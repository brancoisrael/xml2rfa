using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xml2rfax.br.com.ibm.util
{
    public class PropertiesUtil
    {
       
        private Dictionary<String, String> map;

        private static PropertiesUtil propertiesUtil;

        private PropertiesUtil() {
           map = new Dictionary<string, string>();

            string[] lines = System.IO.File.ReadAllLines(path: Constantes.INIT_PROPERTIES);

            foreach (string line in lines)
            {
                String[] values = line.Split('=');
                map.Add(values[0], values[1]);
            }
        }

        public static PropertiesUtil getInstance() {
            try
            {
                if (propertiesUtil == null)
                {
                    propertiesUtil = new PropertiesUtil();
                }                
            }
            catch (Exception e) {
                Logger.LOGGER("Erro: "+e.Message);
                throw new NullReferenceException();
            }

            return propertiesUtil;
        }

        public String getProperties(String key)
        { 
            if (!map.TryGetValue(key, out String value))
            {
                Logger.LOGGER("Erro: Valor não encontrado para a propriedade de configuração {0}.".Replace("{0}",key));
            }

            return value;
        }
    }
}
