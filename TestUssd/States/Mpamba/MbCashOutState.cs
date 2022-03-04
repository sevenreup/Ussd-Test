using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.Core.State;

namespace TestUssd.States.Mpamba
{
    public class MbCashOutState : UssdState
    {
        public MbCashOutState()
        {
            Form = new UssdForm("Mpamba - Cash Out", "").Add("Via Agent").Add("Via ATM").Add("Main Menu");
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
