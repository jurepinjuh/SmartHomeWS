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
        private readonly IMongoCollection<SmartHomeSettings> _settings;
        public SmartHomeDataService(ISmartHomeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DbName);
            _data = database.GetCollection<SmartHomeData>(settings.SmartHomeCollectionName);
            _settings = database.GetCollection<SmartHomeSettings>(settings.SmartHomeSettingsCollectionName);

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
        public async Task<SmartHomeSettings> GetSettings()
        {
            return await _settings.Find(s => true).Sort(new SortDefinitionBuilder<SmartHomeSettings>().Descending("$natural")).FirstOrDefaultAsync();
        }
        public async Task<SmartHomeSettings> Update(SmartHomeSettings settings)
        {
            await _settings.ReplaceOneAsync(item => item.Id == settings.Id, settings);
            return settings;
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
