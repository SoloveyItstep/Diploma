using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.Services.MessageSender
{
    public class AuthMessageSMSSenderOptions
    {
        public string SID
        {
            get
            {
                return "AC32047867810256229f004d0e227058ac";
            }
            set { }
        }
        public string AuthToken
        {
            get
            {
                return "7fcaec42f322f6205ae2de0066f42ae0";
            }
            set { }
        }
        public string SendNumber { get { return "+12015946033"; } set { } }
    }
}
