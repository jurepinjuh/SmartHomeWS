using MongoDB.Driver;
using SmartHomeDataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHomeDataService
{
    public class SmartHomeDataService
    {
        private readonly IMongoCollection<SmartHomeData> _courses;
        public SmarthomeDataService(SmartHomeDataSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _courses = database.GetCollection<Course>(settings.CoursesCollectionName);
        }
    }
}
