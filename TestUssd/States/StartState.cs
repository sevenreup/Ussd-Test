using System;
using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.Core.State;

namespace TestUssd.State
{
    public class StartState : UssdState
    {
        public StartState()
        {

        }

        public override Task<UssdResponse> HandleChoice()
        {
            var choice = Convert.ToInt16(Request.Message.Trim());

            return choice switch
            {
                1 => Redirect(nameof(UbisoftState)),
                _ => Redirect(nameof(MicrosoftState)),
            };
        }

        public async override Task<UssdResponse> RenderForm()
        {
            await SaveSession();
            return new UssdResponse()
            {
                Response = "Hello!!\nJeff\n1: Ubi\n2: Ms"
            };
        }
    }
}
