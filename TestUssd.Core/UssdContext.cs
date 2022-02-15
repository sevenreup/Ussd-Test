using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestUssd.Core.State;

namespace TestUssd.Core
{
    public class UssdContext
    {
        private readonly RedisStore _store;
        private string StartState { get; set; }
        public UssdContext(RedisStore store, string startState)
        {
            _store = store;
            StartState = startState;
        }

        public async Task<UssdResponse> HandleRequest(UssdRequest request)
        {
            try
            {
                switch (request.RequestType)
                {
                    case UssdRequestType.Init:
                        await CloseSession(request);
                        await _store.Set(request.NextRouteKey, StartState);
                        return await OnResponse(request);
                    case UssdRequestType.Response:
                        return await OnResponse(request);
                    default:
                        throw new Exception("Weird");
                }
            }
            catch (Exception ex)
            {
                return UssdResponse.Error(ex.Message);
            }
        }

        private async Task<UssdResponse> OnResponse(UssdRequest request)
        {
            var exists = await SessionExists(request.NextRouteKey);
            if (!exists)
            {
                throw new Exception("SessionId does not exist");
            }
            return await SessionAction(request);
        }
        public async Task CloseSession(UssdRequest request)
        {
            await _store.Delete(request.NextRouteKey);
        }
        public Task<bool> SessionExists(string id)
        {
            return _store.ValueExists(id);
        }

        public async Task<UssdResponse> SessionAction(UssdRequest request)
        {
            var nextRoute = await _store.GetValue(request.NextRouteKey);
            UssdState state = null;
            try
            {
                foreach (var domainAssembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var listOfStates = (
                       from assemblyType in domainAssembly.GetTypes()
                       where typeof(UssdState).IsAssignableFrom(assemblyType)
                       select assemblyType).ToArray();
                    if (listOfStates != null && listOfStates.Length > 0)
                    {
                        var type = listOfStates.First(x => x.Name == nextRoute);
                        state = (UssdState)domainAssembly.CreateInstance(type.FullName);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                state = null;
            }
            if (state == null)
            {
                throw new Exception("Cannot find the state");
            }
            state.Request = request;
            state.Context = this;
            return await state.HandleState();
        }
        public async Task<UssdResponse> Redirect(UssdRequest request)
        {
            return await OnResponse(request);
        }
        public async Task<UssdSessionData> GetSessionData(UssdRequest request)
        {
            var json = await _store.GetValue(request.SessionId);
            if (json == null)
            {
                return new UssdSessionData();
            }
            var data = JsonConvert.DeserializeObject<UssdSessionData>(json);
            return data;
        }

        public async Task SaveValue(string key, string value)
        {
            await _store.Set(key, value);
        }
    }

    public class UssdConstants
    {
        public static string SessionId = "SessionId";
    }
}
