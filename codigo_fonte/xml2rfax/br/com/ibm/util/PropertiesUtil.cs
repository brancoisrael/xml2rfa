using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace xml2rfax.br.com.ibm.util
{
    /// <summary>
    /// Classe para representacao do arquivo properties de configuraca
    /// </summary>
    public class PropertiesUtil
    {
       
        private Dictionary<String, String> map;

        private static PropertiesUtil propertiesUtil;

        /// <summary>
        /// Construtor padrao
        /// </summary>
        private PropertiesUtil() {
           map = new Dictionary<string, string>();

            xml2rfaxInit();

            rightFaxInit();
        }

        /// <summary>
        /// Retorna instancia unica da classe
        /// </summary>
        /// <returns>PropertiesUtil</returns>
        public static PropertiesUtil getInstance() {
            try
            {
                if (propertiesUtil == null)
                {
                    propertiesUtil = new PropertiesUtil();
                }                
            }
            catch (Exception e) {
                Logger.LOGGER_ERROR(e.Message);
                throw new NullReferenceException();
            }

            return propertiesUtil;
        }

        /// <summary>
        /// Retorna valor da propriedade informada
        /// </summary>
        /// <param name="key">String</param>
        /// <returns>String</returns>
        public String getProperties(String key)
        { 
            if (!map.TryGetValue(key, out String value))
            {
                Logger.LOGGER_ERROR("Valor não encontrado para a propriedade de configuração {0}.".Replace("{0}",key));
            }

            return value;
        }

        /// <summary>
        /// Metodo que resgata informacao da variavel de ambiente XML2RFAX,
        /// efetua a leitura do arquivo 
        /// </summary>
        private void xml2rfaxInit()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path: Environment.GetEnvironmentVariable("XML2RFAX_INIT"));

                foreach (string line in lines)
                {
                    if (line.StartsWith("#"))
                        continue;

                    String[] values = line.Split('=');
                    map.Add(values[0], values[1]);
                }
            }
            catch (IOException e)
            {
                Logger.LOGGER_ERROR("Erro ao tentar ler arquivo de configuração xml2rfax.");
                Logger.LOGGER_ERROR(e.Message);
            }
        }

        /// <summary>
        /// Metodo para preencher a estrutura de dados com os valores configurados
        /// no arquivo de inicializacao
        /// </summary>
        private void rightFaxInit()
        {
            try
            {
                String[] lines = System.IO.File.ReadAllLines(path: getProperties("rf_conf_file"));
                String[] values = lines[0].ToString().Split(';');

                map.Add("rf_host", values[2]);
                map.Add("rf_login", values[1]);
                map.Add("rf_passwd", values[3]);
                map.Add("rf_nt_authentication", "false");
                map.Add("rf_user_id", values[1]);
                map.Add("dir_xml_analisar", values[7]);
                map.Add("dir_xml_descartado", values[8]);
                map.Add("dir_xml_processados", values[9]);
            }
            catch (IOException e)
            {
                Logger.LOGGER_ERROR("Erro ao tentar ler arquivo de configuração URAFAX.");
                Logger.LOGGER_ERROR(e.Message);
            }
        }
    }
}
