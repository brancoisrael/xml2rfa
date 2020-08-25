using System;
using System.Text;

namespace xml2rfax.br.com.ibm.util
{
    /// <summary>
    /// Classe utilitaria para strings
    /// </summary>
    public class StringUtils
    {
        /// <summary>
        /// Metodo para construir a formacao do PATH do diretorio de XML analisados
        /// </summary>
        /// <param name="fileName">String</param>
        /// <returns>String</returns>
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
