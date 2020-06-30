using RFCOMAPILib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xml2rfax.br.com.ibm.dto;
using xml2rfax.br.com.ibm.util;

namespace xml2rfax.br.com.ibm.service
{
    public class FaxService
    {                
        public void listUserFax() 
        {
            XMLService xmlService = new XMLService();
            User user = new UserService().OrigemFaxBox;

            if (user != null)
            {
                Faxes faxes = user.Faxes;

                SortedList<DateTime, Fax> listaFax = new SortedList<DateTime, Fax>();

                foreach (Fax fax in faxes)
                {
                    listaFax.Add(fax.FaxRecordDateTime, fax);
                }
               

                foreach (Fax fax in listaFax.Values)
                {
                    DadosFaxDTO dadosFaxDTO = xmlService.lerArquivo(fax.ToFaxNumber); 
                   
                }
                
            }
        } 

    }
}
