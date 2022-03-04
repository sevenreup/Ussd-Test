using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.Core.State;

namespace TestUssd.States.Mpamba
{
    public class MbSendState : UssdState
    {
        public MbSendState()
        {
            Form = new UssdForm("Mpamba - Send Money", "").Add("TNM Mpamba").Add("Airtel Money").Add("Main Menu");
        }
        public override Task<UssdResponse> HandleChoice()
        {
            try
            {
                var choice = Convert.ToInt16(Request.Message.Trim());

                return choice switch
                {
                    1 => Redirect(nameof(MbSendState)),
                    2 => Redirect(nameof(MbSendState)),
                    3 => Redirect(nameof(MpambaStartState)),
                    _ => InputError(),
                };
            }
            catch (Exception)
            {
                return InputError();
            }
        }
    }
}
