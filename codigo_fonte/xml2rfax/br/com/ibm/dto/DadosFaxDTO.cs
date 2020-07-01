using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace xml2rfax.br.com.ibm.dto
{
    [XmlRoot("fax")]
    public class DadosFaxDTO
    {
        [XmlElement("codigoFax")]
        private String _codigoFax;

        [XmlElement("Billinfo1")]
        private String _billinfo1;

        [XmlElement("Billinfo2")]
        private String _billinfo2;

        [XmlElement("RoutingCode")]
        private String _routingCode;

        [XmlElement("FromName")]
        private String _fromName;

        [XmlElement("Comments")]
        private String _comments;

        public DadosFaxDTO()
        { 
        }

        public String codigoFax { get => _codigoFax; set => _codigoFax = value; }
        public String Billinfo2 { get => _billinfo2; set => _billinfo2 = value; }
        public String RoutingCode { get => _routingCode; set => _routingCode = value; }
        public string FromName { get => _fromName; set => _fromName = value; }
        public string Comments { get => _comments; set => _comments = value; }
        public string Billinfo1 { get => _billinfo1; set => _billinfo1 = value; }

        public bool validateFile() 
        {
            foreach (var item in this.GetType().GetProperties())
            {
                if (item.GetValue(this, null) == null || String.IsNullOrEmpty(item.GetValue(this, null).ToString()))
                    return false;                
            }        

            return true;
        }

    }
}
