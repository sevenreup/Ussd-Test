using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.Core.State;

namespace TestUssd.States.General
{
    public class WrongInputState : UssdState
    {
        public override Task<UssdResponse> HandleChoice()
        {
            throw new System.NotImplementedException();
        }

        public async override Task<UssdResponse> RenderForm()
        {
            await SaveSession();
            return new UssdResponse()
            {
                Response = "Sorry "
            };
        }
    }
}
