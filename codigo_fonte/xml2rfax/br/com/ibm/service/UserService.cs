using RFCOMAPILib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    public class UserService
    {
        private Dictionary<String, User> mapUsers;
        private User origemFaxBox;
        private String[] routingCode;


        public UserService() 
        {            
            Users users = FaxServerService.getFaxServerIntance().Users;
            routingCode = new String[users.Count];
            mapUsers = new Dictionary<string, User>();

            int i = 0;

            foreach (User us in users)
            {
                if (us.ID.ToUpper().Equals(PropertiesUtil.getInstance().getProperties("rf_user_id").ToUpper()))
                {
                    origemFaxBox = us;                        
                }
                routingCode[i] = us.RoutingCode;
                mapUsers.Add(us.ID,us);
                i++;
            }
            
        }
        
        public User findUser(String routingCode) 
        {
            mapUsers.TryGetValue(routingCode, out User us);

            return us;
        }

        public User OrigemFaxBox { get => origemFaxBox; }
    }
}
