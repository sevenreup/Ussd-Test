using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestUssd.Core.State
{
    public abstract class UssdState
    {
        public UssdForm Form { get; private set; }
        public UssdRequest Request { get; set; }
        public UssdSessionData Data { get; set; }
        public UssdContext Context { get; set; }

        public UssdState()
        {
        }

        public async Task SaveSession()
        {
            var json = JsonConvert.SerializeObject(Data);
            await Context.SaveValue(Request.SessionId, json);
        }
        public async Task<UssdResponse> Redirect(string state, string input = null)
        {
            Data.NextRoute = state;
            Data.Input = input;
            Data.IsGetting = true;
            Request.SessionType = "response";
            await Context.SaveValue(Request.NextRouteKey, state);
            await SaveSession();
            return await Context.Redirect(Request);
        }
        public abstract Task<UssdResponse> RenderForm();
        public abstract Task<UssdResponse> HandleChoice();
        public async Task<UssdResponse> HandleState()
        {
            Data = await Context.GetSessionData(Request);

            if (Data.IsGetting)
            {
                Data.IsGetting = false;
                return await RenderForm();
            }
            else
            {
                return await HandleChoice();
            }
        }
    }
}
