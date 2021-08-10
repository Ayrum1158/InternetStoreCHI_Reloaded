using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ConfigPOCOs
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
