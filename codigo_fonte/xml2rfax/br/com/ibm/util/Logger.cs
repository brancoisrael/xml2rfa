using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace xml2rfax.br.com.ibm.util
{
    public class Logger
    {

        public static void LOGGER(String classe, String message) {

            DateTime now = DateTime.Now;

            StringBuilder fileName = new StringBuilder();
            fileName.Append(now.Year).Append(now.Month).Append(now.Day);
            string file = Environment.GetEnvironmentVariable("XML2RFAX_LOG").Replace("{0}", fileName.ToString());// Constantes.LOG.Replace("{ 0}", fileName.ToString());

            try
            {
                if (!File.Exists(file))
                {
                    FileStream arquivo = File.Create(file);
                    arquivo.Close();
                }
                using (StreamWriter txtWriter = File.AppendText(file))
                {
                    StringBuilder log = new StringBuilder();
                    log.Append(now.ToLongTimeString());
                    log.Append(" ").Append(classe);
                    log.Append(" - ").Append(message);
                    txtWriter.WriteLine(log.ToString());
                }               
            }
            catch (Exception)
            {
            }

        }

    }
}
