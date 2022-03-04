using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.Core.State;

namespace TestUssd.States.Mpamba
{
    public class MbBalanceState : UssdState
    {
        public MbBalanceState()
        {
            Form = new UssdForm("Mpamba My Balance\nYour Balance is 500", "").Add("Main Menu");
        }
        public override Task<UssdResponse> HandleChoice()
        {
            return Redirect(nameof(MpambaStartState));
        }
    }
}
