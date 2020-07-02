using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using xml2rfax.br.com.ibm.dto;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    public class XMLService
    {
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

        public void moveXMLProcessed(String sourceFile) 
        {
            Console.WriteLine(PropertiesUtil.getInstance().getProperties("dir_xml_processados"));
            Console.WriteLine(Path.GetFileName(sourceFile));
            Console.WriteLine(Path.Combine(PropertiesUtil.getInstance().getProperties("dir_xml_processados"), System.IO.Path.GetFileName(sourceFile)));

            if (Directory.Exists(PropertiesUtil.getInstance().getProperties("dir_xml_processados")))
            {
                try
                {
                    String destFile = Path.Combine(PropertiesUtil.getInstance().getProperties("dir_xml_processados"), System.IO.Path.GetFileName(sourceFile));
                    File.Delete(destFile);
                    File.Move(sourceFile, destFile);
                }
                catch (IOException e)
                {
                    Logger.LOGGER_ERROR("Erro ao tentar mover arquivo {0} para o diretório XML_Processados.".Replace("{0}",sourceFile));
                }
            }
            else
            {
                Logger.LOGGER_ERROR("Não é possível mover arquivos XML, propriedade dir_xml_processados não configurado.");
            }
        }

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
                Logger.LOGGER(MethodBase.GetCurrentMethod().DeclaringType.Name, "ERROR: Propriedade xml_move_age não configurada com valor numérico.");
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
                            Logger.LOGGER(MethodBase.GetCurrentMethod().DeclaringType.Name, "ERROR: Erro ao tentar mover arquivo {0} para o diretório XML_Descartados.".Replace("{0}", fileSource));
                        }
                    }
                }
            }
            else
            {
                Logger.LOGGER(MethodBase.GetCurrentMethod().DeclaringType.Name, "ERROR: Não é possível mover arquivos XML, propriedade dir_xml_analisar não configurado.");
            }           
        }
    }
}
