using RFCOMAPILib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    /// <summary>
    /// Classe de servico para usuario
    /// </summary>
    public class UserService
    {
        private IDictionary<String, User> mapUsers;
        private User origemFaxBox;
       
        /// <summary>
        /// Construtor padrao
        /// </summary>
        public UserService() 
        {            
            Users users = FaxServerService.getFaxServerIntance().Users;
            mapUsers = new Dictionary<string, User>();
            
            foreach (User us in users)
            {
                if (us.ID.ToUpper().Equals(PropertiesUtil.getInstance().getProperties("rf_user_id").ToUpper()))
                {
                    origemFaxBox = us;                        
                }
                
                if(!mapUsers.ContainsKey(us.RoutingCode))
                    mapUsers.Add(us.RoutingCode,us);
            }
            
        }
        
        /// <summary>
        /// Busca usuario logado no sistema
        /// </summary>
        /// <param name="routingCode"></param>
        /// <returns>User</returns>
        public User findUser(String routingCode) 
        {
            mapUsers.TryGetValue(routingCode, out User us);

            return us;
        }

        public User OrigemFaxBox { get => origemFaxBox; }
    }
}
