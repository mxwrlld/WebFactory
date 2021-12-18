using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrentDateAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurrentDateController : ControllerBase
    {
        [HttpGet]
        public ActionResult<DateTime> Get()
        {
            return DateTime.Now;
        }

        [Route("date")]
        [HttpGet]
        public ActionResult<string> GetDateWithFormat(string format = "D")
        {
            try
            {
                string date = DateTime.Now.Date.ToString(format);
                return date;
            }
            catch(FormatException ex)
            {
                return String.Format("Введён неверный формат даты");
            }
        }

        
        [Route("time")]
        [HttpGet]
        public ActionResult<string> GetTime()
        {
            return DateTime.Now.ToString("T");
        }
    }
}
