using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using xml2rfax.br.com.ibm.dto;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    public class XMLService
    {
        public DadosFaxDTO lerArquivo(String nomeArquivo) 
        {
            DadosFaxDTO dadosFaxDTO = null;
            StringBuilder arquivo = new StringBuilder();
            arquivo.Append(PropertiesUtil.getInstance().getProperties("dir_xml_analisar"));
            arquivo.Append("\\");
            arquivo.Append(nomeArquivo);
            arquivo.Append(".xml");

            if (!File.Exists(arquivo.ToString())) 
            {
                Logger.LOGGER(MethodBase.GetCurrentMethod().DeclaringType.Name, "Arquivo {0}.xml não encontrado.".Replace("{0}", nomeArquivo));
                return null;
            }               

            XmlSerializer xmlSerializer = new XmlSerializer(typeof (DadosFaxDTO));

            using (Stream stream = new FileStream(arquivo.ToString(),FileMode.Open))
            {               
                dadosFaxDTO = (DadosFaxDTO)xmlSerializer.Deserialize(stream);               
            }

            return dadosFaxDTO;
        }
    }
}
