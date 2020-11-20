using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Models;
using SmartHome.Services;

namespace SmartHome.Controllers
{
    [Route("smartHome/[action]")]
    [ApiController]
    public class SmartHomeController : ControllerBase
    {
        private readonly SmartHomeDataService _dataService;

        public SmartHomeController(SmartHomeDataService dataService)
        {
            _dataService = dataService;
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

    }
}
