using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class SmartHomeDatabaseSettings : ISmartHomeDatabaseSettings
    {
        public string SmartHomeCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
        public string SmartHomeSettingsCollectionName { get; set; }
    }
    public interface ISmartHomeDatabaseSettings
    {
        string SmartHomeCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DbName { get; set; }

        string SmartHomeSettingsCollectionName { get; set; }
    }
}
