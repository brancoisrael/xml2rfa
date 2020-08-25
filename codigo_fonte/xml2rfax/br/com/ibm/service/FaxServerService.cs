using RFCOMAPILib;
using System;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    /// <summary>
    /// Classe de servico para fax server
    /// </summary>
    public class FaxServerService
    {
        private FaxServer faxServer;
        private static FaxServerService rfLoginService;

        /// <summary>
        /// Construtor padrao
        /// </summary>
        private FaxServerService()
        {
            try
            {
                faxServer = new FaxServer();
                faxServer.ServerName = PropertiesUtil.getInstance().getProperties("rf_host");
                faxServer.UseNTAuthentication = PropertiesUtil.getInstance().getProperties("rf_nt_authentication") == "true" ? BoolType.True : BoolType.False;
                faxServer.AuthorizationUserID = PropertiesUtil.getInstance().getProperties("rf_login");
                faxServer.AuthorizationUserPassword = PropertiesUtil.getInstance().getProperties("rf_passwd");
                faxServer.Protocol = CommunicationProtocolType.cpTCPIP;

                ServerInfo serverInfo = faxServer.ServerInfo;
            }
            catch(Exception e)
            {
                Logger.LOGGER_ERROR("Não foi possível conectar ao servidor RightFax com as configuraçõe informadas: {0}".Replace("{0}",e.Message));                
            }
        }

        /// <summary>
        /// Resgata instancia unica da classe FaxService
        /// </summary>
        /// <returns>FaxServer</returns>
        public static FaxServer getFaxServerIntance() {
            if (rfLoginService == null)
                rfLoginService = new FaxServerService();

            return rfLoginService.getFaxServer();
        }

        /// <summary>
        /// Retorna instancia de fax server
        /// </summary>
        /// <returns>FaxServer</returns>
        private FaxServer getFaxServer() {
            return faxServer;
        }
    }
}
