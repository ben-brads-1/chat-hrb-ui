using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//ChatGPT nuget
using Whetstone.ChatGPT;
using Whetstone.ChatGPT.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.HRB.Api.Controllers
{
    public class ChatHRBController : Controller
    {
        protected IServiceProvider _serviceProvider;
        public ChatHRBController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost("chat")]
        public async Task<ActionResult<string>> ChatWithBot(string input)
        {
            try
            {

            }
            catch (Exception e)
            {

            }
            return NoContent();
        }
    }
}

