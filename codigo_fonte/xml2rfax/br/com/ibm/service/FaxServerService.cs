using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RFCOMAPILib;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    public class FaxServerService
    {
        private FaxServer faxServer;
        private static FaxServerService rfLoginService;

        private FaxServerService()
        {
            faxServer = new FaxServer();
            faxServer.ServerName = PropertiesUtil.getInstance().getProperties("rf_host");
            faxServer.UseNTAuthentication = PropertiesUtil.getInstance().getProperties("rf_nt_authentication") == "true" ? BoolType.True : BoolType.False;
            faxServer.AuthorizationUserID = PropertiesUtil.getInstance().getProperties("rf_login");
            faxServer.AuthorizationUserPassword = PropertiesUtil.getInstance().getProperties("rf_passwd");
            faxServer.Protocol = CommunicationProtocolType.cpTCPIP;
        }

        public static FaxServer getFaxServerIntance() {
            if (rfLoginService == null)
                rfLoginService = new FaxServerService();

            return rfLoginService.getFaxServer();
        }

        private FaxServer getFaxServer() {
            return faxServer;
        }
    }
}
