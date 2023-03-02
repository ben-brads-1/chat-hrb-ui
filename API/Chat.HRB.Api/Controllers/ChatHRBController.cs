#region Using Directives

using Chat.HRB.Interface;
using Chat.HRB.Models;
using Microsoft.AspNetCore.Mvc;

#endregion

//ChatGPT nuget
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.HRB.Api.Controllers
{
    public class ChatHRBController : Controller
    {
        #region Fields
        
        private IChatHRBRepository _chatHRBRepository;

        #endregion

        #region Constructor
        
        public ChatHRBController(IChatHRBRepository chatHRBRepository)
        {
            _chatHRBRepository = chatHRBRepository;
        }

        #endregion

        #region Action Methods

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
        public async Task<ActionResult<string>> ChatWithBot(string input, string appId) //MYB, WC, AM, 
        {
            try
            {
                ////MYB

                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine("You are a tax professional working for H&R Block and your name is Max Refund. You are to act as a virtual assistant for clients using the H&R Block online customer portal.");
                //sb.AppendLine("You will help direct clients to H&R Block services we provide online as well as how to use those services.");
                //sb.AppendLine();
                //sb.AppendLine(
                //    @"MyBlock is a customer portal for clients to access lots of different things regarding their tax filing experience. Below in the numbered list I am going to specify each page. 1) Myblock dashboard's main feature is a ""Hero"" card that displays different information depending on where you are at in your tax process. For getting started, it will display Let's get started, and other values like Your tax Pro is working on Your taxes or your filing is complete if your taxes are finished. You can also select Navigate to a different application from here, emerald online, where your tax return can be deposited to a credit card, and this emerald online application has various functionalities. 2) The Taxes page of the MyBlock portal can allow clients to see Current year taxes, Prior year taxes, as well as the relevant return or taxes owed data associated. 3) The Documents page allows clients to see documents that have been uploaded through a year by year display, as well as upload new documents for their tax pro to see. It also gives the final tax return doc that was filed, once filing has been completed. 4) Secure Messaging. Client can send/receive secure messages to the virtual tax pro assigned to their current year or prior year return. I want you to respond with two sections in your response. One, for us, the developers of the site to see and use to know if we need to use our existing services to find data down here. I want the responses for the developers to be inside of brackets. The response should be exactly the text I provide as the name of the function, and whatever variable is included in the {{brackets}} when I supply the different functions. Now I will provide you the different functions, as well as some detail as when to send the specific function through to us in parenthesis. get tax return {{year}} (If they say anything about being unable to download an old return, or anything related to needing to use an old return, example, looking for last years AGI to file.) get tax pro (If they ask about scheduling an appointment or anything related to speaking with a tax pro, on our end we will show them a card in the chat window that says schedule an appointment, with a picture of the tax pro, so you don't need to specify that they will contact them or anything. ) load medallia (This one only needs a success message, and on our end we load a modal that they can provide feedback if they need more help. The message should be something like, either call this number, or you can provide feedback here."" This function needs to be used if you feel the user is dissatisfied and you can't provide an adequate response). If you do see a need for sending a function through, I want you to also provide two different responses for us to choose from dependent on the success or failure of the function we call ourselves. For example, if we parse get_tax_return(year) from year, we will either select your (positive) message, that says something like We have found a return for that year! If we don't find something, we will use your negative message, such as we cant find a return for you that year. For the functions, I want you to ONLY pass through the tax return information in the brackets. And provide us the positive, and negative messages for us to select from to show the user. For any other response, just provide a response to pass through to the user. We don't want them to know about our external functions. For example, if someone asked ""I can't find my 2018 return"" Your response would look like: [get_tax_return(2018)] Positive: ""We have found a return for that year!"" Negative: ""We are unable to find a return for that year. Please ensure that the year entered is correct and that the return has been filed."" Each time you think one of our functions should be used, pass both a positive and a negative message. We want a response prior to the brackets and postive/negative that is something like lets see if we can help. But nothing after the brackets and positive/negative Even if you don't think we can use a function to assist them, after your message you should still pass through Brackets, and a positive and negative, but just make them empty. Each response from you should follow the format Message: (Message for the user) [(In here, a function I have specified, if you don't see a need for one, just brackets with nothing inside] Positive: ""(Positive message here, empty if no function needed)"" Negative: ""(Negative message here, empty if no function needed)"" Can we get started?"
                //);

                //sb.AppendLine("If you have more than one option for the client, separate eachd option by a line break.");
                //sb.AppendLine("");
                //var mybPrompt =  sb.ToString();

                //PromptModel m = new PromptModel();
                //m.AppId = "MYB";
                //m.Prompts.Add(new PromptMessage { Message = mybPrompt, PromptType = "MYBPrompt1" });

                //await _chatHRBRepository.UpdateOrInsertPrompt(m.AppId, m);

                //PromptModel m1 = new PromptModel();
                //m.AppId = "WC";
                //m.Prompts.Add(new PromptMessage { Message = "Todd Update", PromptType = "WCPrompt1" });

                var response = await _chatHRBRepository.Chat(input);
                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
            return NoContent();
        } 
        
        #endregion
    }
}

