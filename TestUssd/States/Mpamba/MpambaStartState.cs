using System;
using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.Core.State;

namespace TestUssd.States.Mpamba
{
    public class MpambaStartState : UssdState
    {
        public MpambaStartState()
        {
            Form = new UssdForm("Welcome to Mpamba", "")
                .Add("Check Balance")
                .Add("Send Money")
                .Add("Cashout")
                .Add("Airtime/Bundle");
        }

        public override Task<UssdResponse> HandleChoice()
        {
            try
            {
                var choice = Convert.ToInt16(Request.Message.Trim());

                return choice switch
                {
                    1 => Redirect(nameof(MbBalanceState)),
                    2 => Redirect(nameof(MbSendState)),
                    3 => Redirect(nameof(MbCashOutState)),
                    4 => Redirect(nameof(MbAirtimeState)),
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
