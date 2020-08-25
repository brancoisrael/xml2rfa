using RFCOMAPILib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using xml2rfax.br.com.ibm.dto;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    /// <summary>
    /// Classe de servico para fax 
    /// </summary>
    public class FaxService
    {
        private UserService userService;
        private XMLService xmlService;

        /// <summary>
        /// Construtor padrao
        /// </summary>
        public FaxService()
        {
            userService = new UserService();
            xmlService = new XMLService();
        }

        /// <summary>
        /// Metodo utilizado para mover faxes em da caixa postal default para
        /// as caixas postais de destino configurada no arquivo XML
        /// </summary>
        public void moveFaxes()
        {
            IList<Fax> listaFax = listarFaxesAsc();

            if (listaFax != null && listaFax.Count > 0)
            {
                foreach (Fax fax in listaFax)
                {                   
                    if (!String.IsNullOrEmpty(fax.FromFaxNumber))
                    {
                        DadosFaxDTO dadosFaxDTO = xmlService.mapperXMLtoObject(fax.FromFaxNumber);

                        if (dadosFaxDTO != null)
                        {
                            if (!dadosFaxDTO.validateFile())
                            {
                                Logger.LOGGER_INF("Arquivo {0}.xml com problemas em sua estrutura.".Replace("{0}", fax.FromFaxNumber));
                                continue;
                            }

                            routToUser(fax, dadosFaxDTO);
                        }
                    }
                    else
                    {
                        Logger.LOGGER_INF("Fax com UniqueID {0} não possui routing code.".Replace("{0}", fax.UniqueID));
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para listar os faxes ordenando do mais antigo ao mais recente
        /// </summary>
        /// <returns>IList</returns>
        private IList<Fax> listarFaxesAsc()
        {
            User user = userService.OrigemFaxBox;


            if (user != null)
            {
                Faxes faxes = user.Faxes;

                SortedList<DateTime, Fax> listaFax = new SortedList<DateTime, Fax>();

                foreach (Fax fax in faxes)
                {                  
                    if (fax.IsReceived == BoolType.True && fax.FaxStatus == FaxStatusType.fsDoneOK && fax.Folder.ID.ToUpper().Equals("MAIN"))
                        listaFax.Add(fax.FaxRecordDateTime, fax);                    
                }

                return listaFax.Values;
            }
            return null;
        }

        /// <summary>
        /// Metodo para rotear faxes para os usuarios de destino
        /// </summary>
        /// <param name="fax"></param>
        /// <param name="dadosFaxDTO"></param>
        private void routToUser(Fax fax, DadosFaxDTO dadosFaxDTO)
        {
            if (Directory.Exists(PropertiesUtil.getInstance().getProperties("dir_xml_processados")))
            {
                User user = userService.findUser(dadosFaxDTO.RoutingCode);

                if (user != null)
                {
                    fax.BillingCode1 = dadosFaxDTO.Billinfo1;
                    fax.BillingCode2 = dadosFaxDTO.Billinfo2;
                    fax.FromName = dadosFaxDTO.Comments;
                    fax.UserComments = dadosFaxDTO.FromName;
                    fax.ToName = dadosFaxDTO.RoutingCode;
                    fax.ToFaxNumber = "990000";

                    fax.Save(BoolType.True);
                    fax.RouteToUser(user, "XML2URAFAX");
                    xmlService.moveXMLProcessed(StringUtils.pathXMLSouce(dadosFaxDTO.codigoFax));
                }
                else
                {
                    Logger.LOGGER_INF("Usuário com routing code {0} não encontrado.".Replace("{0}", dadosFaxDTO.RoutingCode));
                }
            }
            else
            {
                Logger.LOGGER_ERROR("Não é possível mover arquivos XML, propriedade dir_xml_processados não configurado.");
            }

        }
    }
}
