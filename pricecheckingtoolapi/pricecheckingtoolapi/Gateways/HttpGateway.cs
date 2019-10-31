using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Gateways
{
    public class HttpGateway
    {
        public async Task Get(string link)
        {
            HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(link);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                httpClient.Dispose();
            }
        }
    }
}
