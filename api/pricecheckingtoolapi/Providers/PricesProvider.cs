using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using pricecheckingtoolapi.Db;
using pricecheckingtoolapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Providers
{
    public class PricesProvider
    {
        private readonly HttpClient _httpClient;
        private readonly List<string> urlsItems = new List<string>{$"https://poe.ninja/api/data/itemoverview?league=Blight&type=Oil", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Incubator",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=Scarab", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Fossil",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=Resonator", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Essence",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=DivinationCard", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Prophecy",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueJewel", "	https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueFlask",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueWeapon","	https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueArmour",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueAccessory","https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueMap",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=SkillGem", "https://poe.ninja/api/data/itemoverview?league=Blight&type=BaseType",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=Map","https://poe.ninja/api/data/itemoverview?league=Blight&type=Beast"

        };
        private readonly List<string> urlsCurrency = new List<string> { $"https://poe.ninja/api/data/itemoverview?league=Blight&type=Currency", "https://poe.ninja/api/data/currencyoverview?league=Blight&type=Fragment" };

        public PricesProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task ExecuteFetch(DatabaseContext databaseContext, CancellationToken cancellationToken)
        {
            foreach (var url in urlsCurrency)
            {
                await GetCurrenies(url, databaseContext, cancellationToken);
            }
            foreach (var url in urlsItems)
            {
                await GetItems(url, databaseContext, cancellationToken);
            }
        }

        private async Task GetCurrenies(string link, DatabaseContext databaseContext, CancellationToken cancellationToken)
        {
            Currencies objects = new Currencies();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(link, cancellationToken);
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
                var saved = false;
                while (!saved)
                {
                    try
                    {
                        // Attempt to save changes to the database
                        await databaseContext.SaveChangesAsync();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        foreach (var entry in ex.Entries)
                        {
                            if (entry.Entity is Currency)
                            {
                                var proposedValues = entry.CurrentValues;
                                var databaseValues = entry.GetDatabaseValues();

                                foreach (var property in proposedValues.Properties)
                                {
                                    var proposedValue = proposedValues[property];
                                    var databaseValue = databaseValues[property];

                                }
                                entry.OriginalValues.SetValues(proposedValues);
                            }
                            else
                            {
                                throw new NotSupportedException(
                                    "Don't know how to handle concurrency conflicts for "
                                    + entry.Metadata.Name);
                            }
                        }
                    }
                }
            }
        }

        private async Task GetItems(string link, DatabaseContext databaseContext, CancellationToken cancellationToken)
        {
            Items objects = new Items();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(link, cancellationToken);
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
                var saved = false;
                while (!saved)
                {
                    try
                    {
                        // Attempt to save changes to the database
                        await databaseContext.SaveChangesAsync();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        foreach (var entry in ex.Entries)
                        {
                            if (entry.Entity is Item)
                            {
                                var proposedValues = entry.CurrentValues;
                                var databaseValues = entry.GetDatabaseValues();

                                foreach (var property in proposedValues.Properties)
                                {
                                    var proposedValue = proposedValues[property];
                                    var databaseValue = databaseValues[property];

                                }
                                entry.OriginalValues.SetValues(proposedValues);
                            }
                            else
                            {
                                throw new NotSupportedException(
                                    "Don't know how to handle concurrency conflicts for "
                                    + entry.Metadata.Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
