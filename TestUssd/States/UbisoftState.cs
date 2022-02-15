using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.Core.State;

namespace TestUssd.State
{
    public class UbisoftState : UssdState
    {
        public override Task<UssdResponse> HandleChoice()
        {

            return Redirect(nameof(MicrosoftState));
        }

        public override Task<UssdResponse> RenderForm()
        {
            return Task.Run(() =>
            {
                return new UssdResponse()
                {
                    Response = "Info about Ubi"
                };
            });
        }
    }
}
