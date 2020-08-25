using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;

namespace xml2rfax.br.com.ibm.util
{
    /// <summary>
    /// Classe de servico para informacoes de log
    /// </summary>
    public class Logger
    {

        /// <summary>
        /// Metodo para efetuar log em arquivo texto
        /// </summary>
        /// <param name="classe"></param>
        /// <param name="message"></param>
        public static void LOGGER(String classe, String message) {

            DateTime now = DateTime.Now;

            StringBuilder fileName = new StringBuilder();
            fileName.Append(now.Year).Append(now.Month).Append(now.Day);
            string file = PropertiesUtil.getInstance().getProperties("xml2rfax_log").Replace("{0}", fileName.ToString());

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

        /// <summary>
        /// Metodo para efetuar log de erro no event viewer
        /// </summary>
        /// <param name="message"></param>
        public static void LOGGER_ERROR(String message)
        {
            
            try
            {               
                if (!EventLog.SourceExists("XML2RFAX"))
                {
                    EventLog.CreateEventSource("XML2RFAX", "XML2RFAX");
                }

                EventLog.WriteEntry("XML2RFAX", message, EventLogEntryType.Error);
            }
            catch(SecurityException e)
            {
                Logger.LOGGER(MethodBase.GetCurrentMethod().DeclaringType.Name, "Erro ao tentar gravar evento {0} no event viewer, mensagem: {1}".Replace("{0}", message).Replace("{1}",e.Message));
            }
            
        }

        /// <summary>
        /// Metodo para efetuar log de informacao no event viewer
        /// </summary>
        /// <param name="message"></param>
        public static void LOGGER_INF(String message)
        {
            try
            {               
                if (!EventLog.SourceExists("XML2RFAX"))
                {
                    EventLog.CreateEventSource("XML2RFAX", "XML2RFAX");
                }

                EventLog.WriteEntry("XML2RFAX", message, EventLogEntryType.Information);
                
            }
            catch(SecurityException e)
            {
                Logger.LOGGER(MethodBase.GetCurrentMethod().DeclaringType.Name, "Erro ao tentar gravar evento {0} no event viewer, mensagem: {1}".Replace("{0}", message).Replace("{1}", e.Message));
            }
        }

        /// <summary>
        /// Metodo para efetuar log de alerta no event viewer
        /// </summary>
        /// <param name="message"></param>
        public static void LOGGER_WARN(String message)
        {
            try
            {
                if (!EventLog.SourceExists("XML2RFAX"))
                {
                    EventLog.CreateEventSource("XML2RFAX", "XML2RFAX");
                }

                EventLog.WriteEntry("XML2RFAX", message, EventLogEntryType.Warning);
            }
            catch(SecurityException e)
            {
                Logger.LOGGER(MethodBase.GetCurrentMethod().DeclaringType.Name, "Erro ao tentar gravar evento {0} no event viewer, mensagem: {1}".Replace("{0}", message).Replace("{1}", e.Message));
            }
        }

    }
}
