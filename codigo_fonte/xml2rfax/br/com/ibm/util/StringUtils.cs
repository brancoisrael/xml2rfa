using System;
using System.Text;

namespace xml2rfax.br.com.ibm.util
{
    public class StringUtils
    {

        public static String pathXMLSouce(String fileName) 
        {
            StringBuilder arquivo = new StringBuilder();
            arquivo.Append(PropertiesUtil.getInstance().getProperties("dir_xml_analisar"));
            arquivo.Append("\\");
            arquivo.Append(fileName);
            arquivo.Append(".xml");

            return arquivo.ToString();
        }

    }
}
