using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestUssd.Core.State
{
    public abstract class UssdState
    {
        public UssdForm Form { get; set; }
        public UssdRequest Request { get; set; }
        public UssdSessionData Data { get; set; }
        public UssdContext Context { get; set; }

        public UssdState() { }
        public UssdState(UssdRequest request, UssdSessionData data, UssdContext context)
        {
            Request = request;
            Data = data;
            Context = context;
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
            Request.SessionType = 2;
            await Context.SaveValue(Request.NextRouteKey, state);
            await SaveSession();
            return await Context.Redirect(Request);
        }
        public virtual async Task<UssdResponse> InputError()
        {
            await SaveSession();
            Form.Header = "Invalid input, Try again";
            return new UssdResponse()
            {
                Response = Form.Render()
            };
        }
        public async virtual Task<UssdResponse> RenderForm()
        {
            await SaveSession();
            return new UssdResponse()
            {
                Response = Form.Render()
            };
        }
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
