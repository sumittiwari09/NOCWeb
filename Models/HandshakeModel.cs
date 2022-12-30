using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class HandshakeModel
    {
        public string UserName { get; set; }


        public string Password { get; set; }

        public SSOUserDetail SSOUserDetail { get; set; }
    }
}