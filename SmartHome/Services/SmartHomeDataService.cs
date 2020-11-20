using MongoDB.Bson;
using MongoDB.Driver;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public class SmartHomeDataService
    {
        private readonly IMongoCollection<SmartHomeData> _data;
        public SmartHomeDataService(ISmartHomeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DbName);
            _data = database.GetCollection<SmartHomeData>(settings.SmartHomeCollectionName);
        }

        public List<SmartHomeData> GetAllDataForDay(DateTime date)
        {
            List<SmartHomeData> data = new List<SmartHomeData>();
            foreach (var item in _data.Find(s => true).ToList())
            {
                if (item.dateTime.Date == date.Date)
                {
                    data.Add(item);
                }
            }

            return data;

        }
        public async Task<List<SmartHomeData>> GetAllData()
        {
            return await _data.Find(s => true).ToListAsync();
        }


        public async Task<SmartHomeData> GetCurrentData()
        {
            return await _data.Find(s => true).Sort(new SortDefinitionBuilder<SmartHomeData>().Descending("$natural")).FirstOrDefaultAsync();
        }

        public async Task<SmartHomeData> Insert(SmartHomeData data)
        {
            data.dateTime = DateTime.Now;
            await _data.InsertOneAsync(data);
            return data;
        }


    }
}
