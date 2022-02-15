using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUssd.Core
{
    public class UssdResponse
    {
        public string Response { get; set; }
        public string SessionType { get; set; }

        public static UssdResponse Error(string message) {
            return new UssdResponse()
            {
                Response = message
            };
        }
    }
}
