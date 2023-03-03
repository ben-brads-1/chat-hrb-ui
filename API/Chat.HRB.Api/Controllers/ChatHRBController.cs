using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.HRB.Interface;
using Chat.HRB.Models;
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


        [HttpPost("chathistory")]
        public async Task<ActionResult<List<string>>> GetChatHistoryData(string userId, string appId, int year)
        {
            try
            {
                var chats = await _chatHRBRepository.GetChatHistoryAsync(userId, appId, year);
                if (chats != null)
                {
                    return Ok(chats);
                }

            }
            catch (Exception ex)
            {

                //Log exception
            }
            return NoContent();
        }

        [HttpPost("updatechathistory")]
        public async Task<ActionResult<PromptModel>> UpdateChatHistoryData(string userId, string appId, int year, List<string> messages)
        {
            try
            {
                var updatedData = await _chatHRBRepository.UpdateOrInsertChatHistory(userId, appId, year, messages);
                if (updatedData != null)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

                //Log exception
            }

            return NoContent();
        }

        [HttpPost("prompt")]
        public async Task<ActionResult<PromptModel>> GetPromptData(string appId)
        {
            try
            {
                var prompt = await _chatHRBRepository.GetPromptAsync(appId);
                if (prompt != null)
                {
                    return Ok(prompt);
                }
            }
            catch (Exception ex)
            {
                //Log exception
            }
            return NoContent();
        }

        [HttpPost("updateprompt")]
        public async Task<ActionResult<PromptModel>> UpdatePromptData(string appId, PromptModel prompt)
        {
            try
            {
                var upDatedPrompt = await _chatHRBRepository.UpdateOrInsertPrompt(appId, prompt);
                if (prompt != null)
                {
                    return Ok(upDatedPrompt);
                }

            }
            catch (Exception ex)
            {

                //Log exception
            }
            return NoContent();
        }

        [HttpPost("chat")]
        public async Task<ActionResult<string>> ChatWithBot(string input, string appId, string userId, int year) //MYB, WC, AM, 
        {
            try
            {
                var response = await _chatHRBRepository.Chat(input, appId, userId, year);
                if (response != null)
                {
                    return Ok(response.Trim());
                }
            }
            catch (Exception e)
            {

            }
            return NoContent();
        }
    }
}

