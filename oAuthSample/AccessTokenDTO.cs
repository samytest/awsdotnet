using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralUtilities.oAuth
{
    public class AccessTokenDTO
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string code { get; set; }
        public string access_token { get; set; }
        public string livemode { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string stripe_publishable_key { get; set; }
        public string stripe_user_id { get; set; }
        public string scope { get; set; }
        public string context { get; set; }
        public string expires_at { get; set; }
    }
}
