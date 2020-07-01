using RFCOMAPILib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using xml2rfax.br.com.ibm.dto;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    public class FaxService
    {         
       private UserService userService;

        public FaxService()
        {
            userService = new UserService();
        }

        public void moveFaxes() 
        {
           
            IList<Fax> listaFax = listarFaxesAsc();

            if (listaFax != null)
            {
                XMLService xmlService = new XMLService();

                foreach (Fax fax in listaFax.Reverse())
                {
                    DadosFaxDTO dadosFaxDTO = xmlService.lerArquivo(fax.FromFaxNumber);

                    if (dadosFaxDTO != null)
                    {
                        if (!dadosFaxDTO.validateFile())
                        {
                            Logger.LOGGER(MethodBase.GetCurrentMethod().DeclaringType.Name, "Arquivo {0}.xml com problemas em sua estrutura.".Replace("{0}", fax.FromFaxNumber));
                            continue;
                        }

                        routToUser(fax, dadosFaxDTO);
                    }
                }
            }    
            
        }

        private IList<Fax> listarFaxesAsc()
        {           
            User user = userService.OrigemFaxBox;


            if (user != null)
            {
                Faxes faxes = user.Faxes;

                SortedList<DateTime, Fax> listaFax = new SortedList<DateTime, Fax>();
               
                foreach (Fax fax in faxes)
                {
                    if (fax.IsReceived == BoolType.True && fax.FaxStatus == FaxStatusType.fsDoneOK)
                        listaFax.Add(fax.FaxRecordDateTime, fax);
                }

                return listaFax.Values;
            }
            return null;
        }


        private void routToUser(Fax fax, DadosFaxDTO dadosFaxDTO) 
        {
            fax.BillingCode1 = dadosFaxDTO.Billinfo1;
            fax.BillingCode2 = dadosFaxDTO.Billinfo2;
            fax.FromName = dadosFaxDTO.Comments;
            fax.UserComments = dadosFaxDTO.FromName;

            fax.ToName = dadosFaxDTO.RoutingCode;
            fax.ToFaxNumber = "990000";

            fax.Save(BoolType.True);

            User user = userService.findUser(fax.ToName);
            
            if(user != null)
                fax.RouteToUser(userService.findUser(fax.ToName),"XML2URAFAX");
        }

    }
}
