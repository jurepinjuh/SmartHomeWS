using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SmartHome.Hubs;
using SmartHome.Models;
using SmartHome.Services;

namespace SmartHome.Controllers
{
    [Route("smartHome/[action]")]
    [ApiController]
    public class SmartHomeController : ControllerBase
    {
        private IHubContext<NotifyHub> _hub;
        private IHubContext<SettingsHub> _settingsHub;
        private readonly SmartHomeDataService _dataService;

        public SmartHomeController(SmartHomeDataService dataService,IHubContext<NotifyHub> hub,IHubContext<SettingsHub> settingsHub)
        {
            _dataService = dataService;
            _hub = hub;
            _settingsHub = settingsHub;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartHomeData>>> GetAll()
        {
            var data = await _dataService.GetAllData();
            return Ok(data);
        }
      

        [HttpPost]
        public async Task<IActionResult> Insert(SmartHomeData data)
        {
            await _dataService.Insert(data);
            await _hub.Clients.All.SendAsync("notif", "inserted");
            return Ok(data);
        }

        [HttpGet("{date}")]
        public ActionResult<IEnumerable<SmartHomeData>> GetAllByDate(DateTime date)
        {
            var data = _dataService.GetAllDataForDay(date);
            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult<SmartHomeData>> GetCurrentData()
        {
            var data = await _dataService.GetCurrentData();
            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult<SmartHomeSettings>> GetSettings()
        {
            var data = await _dataService.GetSettings();
            return Ok(data);
        }

        [HttpPut]
        public async Task<ActionResult<SmartHomeSettings>> UpdateSettings(SmartHomeSettings settings)
        {
            var data = await _dataService.Update(settings);
            await _settingsHub.Clients.All.SendAsync("updated", settings);
            return Ok(data);
        }

        [HttpGet("{from}/{to}")]
        public ActionResult<IEnumerable<SmartHomeData>> GetAllByPeriod(DateTime from,DateTime to)
        {
            var data = _dataService.GetDataByPeriod(from,to);
            return Ok(data);
        }



    }
}
