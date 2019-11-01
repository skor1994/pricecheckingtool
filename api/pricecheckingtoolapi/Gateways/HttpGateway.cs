using pricecheckingtoolapi.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using pricecheckingtoolapi.Models;

namespace pricecheckingtoolapi.Gateways
{
    public class HttpGateway
    {
        public async Task GetCurrenies(string link, DatabaseContext databaseContext)
        {
            HttpClient httpClient = new HttpClient();
            Currencies objects = new Currencies();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(link);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                objects = JsonConvert.DeserializeObject<Currencies>(responseBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objects.lines.ForEach(n => databaseContext.Currencies.Update(n));
                await databaseContext.SaveChangesAsync();
                httpClient.Dispose();
            }
        }

        public async Task GetItems(string link, DatabaseContext databaseContext)
        {
            HttpClient httpClient = new HttpClient();
            Items objects = new Items();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(link);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                objects = JsonConvert.DeserializeObject<Items>(responseBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objects.lines.ForEach(n => databaseContext.Items.Update(n));
                await databaseContext.SaveChangesAsync();
                httpClient.Dispose();
            }
        }
    }
}
