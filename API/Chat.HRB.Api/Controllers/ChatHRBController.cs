using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.HRB.Interface;
using Microsoft.AspNetCore.Mvc;

//ChatGPT nuget
using Whetstone.ChatGPT;
using Whetstone.ChatGPT.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.HRB.Api.Controllers
{
    public class ChatHRBController : Controller
    {
        protected IChatHRBRepository _chatHRBRepository;
        public ChatHRBController(IChatHRBRepository chatHRBRepository)
        {
            _chatHRBRepository = chatHRBRepository;
        }

        [HttpPost("chat")]
        public async Task<ActionResult<string>> ChatWithBot(string input, string appId) //MYB, WC, AM, 
        {
            try
            {
                var response = await _chatHRBRepository.Chat(input);
                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {

            }
            return NoContent();
        }
    }
}

