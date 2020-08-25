using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using xml2rfax.br.com.ibm.dto;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    /// <summary>
    /// Classe de servico para arquivos XML
    /// </summary>
    public class XMLService
    {
        /// <summary>
        /// Conversor de string XML para objeto de fax
        /// </summary>
        /// <param name="nomeArquivo"></param>
        /// <returns>DadosFaxDTO</returns>
        public DadosFaxDTO mapperXMLtoObject(String nomeArquivo) 
        {
            DadosFaxDTO dadosFaxDTO = null;
            StringBuilder arquivo = new StringBuilder();
            arquivo.Append(PropertiesUtil.getInstance().getProperties("dir_xml_analisar"));
            arquivo.Append("\\");
            arquivo.Append(nomeArquivo);
            arquivo.Append(".xml");

            if (!File.Exists(StringUtils.pathXMLSouce(nomeArquivo))) 
            {
                Logger.LOGGER_INF("Arquivo {0}.xml não encontrado.".Replace("{0}", nomeArquivo));
                return null;
            }               

            XmlSerializer xmlSerializer = new XmlSerializer(typeof (DadosFaxDTO));

            using (Stream stream = new FileStream(arquivo.ToString(),FileMode.Open))
            {               
                dadosFaxDTO = (DadosFaxDTO)xmlSerializer.Deserialize(stream);               
            }

            return dadosFaxDTO;
        }

        /// <summary>
        /// Metodo para mover arquivos XML processados pela rotina
        /// </summary>
        /// <param name="sourceFile"></param>
        public void moveXMLProcessed(String sourceFile)
        {            
            try
            {
                String destFile = Path.Combine(PropertiesUtil.getInstance().getProperties("dir_xml_processados"), System.IO.Path.GetFileName(sourceFile));
                File.Delete(destFile);
                File.Move(sourceFile, destFile);
            }
            catch (IOException e)
            {
                Logger.LOGGER_ERROR("Erro ao tentar mover arquivo {0} para o diretório XML_Processados.".Replace("{0}", sourceFile));
                Logger.LOGGER_ERROR(e.Message);
            }
        }
           
        
        /// <summary>
        /// Metodo para mover XML nao processado pela rotina com base no ciclo de vida
        /// configurado para faxes nao processados
        /// </summary>
        public void moveXMLDiscarded()
        {
            int faxAge = 0;
            if (String.IsNullOrEmpty(PropertiesUtil.getInstance().getProperties("xml_move_age")))
            {
                Logger.LOGGER_ERROR("Propriedade xml_move_age não configurada.");
                return;            
            }

            try 
            {
                faxAge = Int32.Parse(PropertiesUtil.getInstance().getProperties("xml_move_age"));
            }
            catch(Exception e)
            {
                Logger.LOGGER_ERROR("Propriedade xml_move_age não configurada com valor numérico.");
                Logger.LOGGER_ERROR(e.Message);
            }

            if(Directory.Exists(PropertiesUtil.getInstance().getProperties("dir_xml_descartado")))
            {
                string[] files = Directory.GetFiles(PropertiesUtil.getInstance().getProperties("dir_xml_analisar"));

                foreach (string fileSource in files)
                {
                    DateTime createDate = File.GetCreationTime(fileSource);
                    DateTime xmlAge = DateTime.Now;
                    xmlAge = xmlAge.AddDays(faxAge * (-1));

                    if (xmlAge >= createDate)
                    {
                        try
                        {
                            String destFile = Path.Combine(PropertiesUtil.getInstance().getProperties("dir_xml_descartado"), System.IO.Path.GetFileName(fileSource));
                            File.Delete(destFile);
                            File.Move(fileSource, destFile);
                        }
                        catch (IOException e)
                        {
                            Logger.LOGGER_ERROR("Erro ao tentar mover arquivo {0} para o diretório XML_Descartados.".Replace("{0}", fileSource));
                            Logger.LOGGER_ERROR(e.Message);
                        }
                    }
                }
            }
            else
            {
                Logger.LOGGER_ERROR("Não é possível mover arquivos XML, propriedade dir_xml_analisar não está configurada corretamente.");
            }           
        }
    }
}
