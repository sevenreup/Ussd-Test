using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.Core.State;

namespace TestUssd.States.Mpamba
{
    public class MbAirtimeState : UssdState
    {
        public MbAirtimeState()
        {
            Form = new UssdForm("Mpamba - Airtime/Bundles", "").Add("Airtime").Add("Bundles").Add("Main Menu");
        }

        public override Task<UssdResponse> HandleChoice()
        {
            try
            {
                var choice = Convert.ToInt16(Request.Message.Trim());

                return choice switch
                {
                    1 => Redirect(nameof(MbCashOutState)),
                    2 => Redirect(nameof(MbCashOutState)),
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
