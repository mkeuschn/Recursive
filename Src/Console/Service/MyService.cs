using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace ConsoleApplication.Service
{
    public class MyService
    {
        public async Task<bool> DoAsync(int retry = 1)
        {
            IFlurlResponse response;
            try
            {
                response = await "http://www.google.com".GetAsync();
            }
            catch (FlurlHttpException e)
            {
                if (retry <= 3)
                {
                    Thread.Sleep(new TimeSpan(0, 0, 1 * retry));
                    return await DoAsync(++retry);
                }
                Console.WriteLine(e);
                throw;
            }

            return response.ResponseMessage.IsSuccessStatusCode;
        }
    }
}
