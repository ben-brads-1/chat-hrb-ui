using System;
using Chat.HRB.Interface;
using Whetstone.ChatGPT;
using Whetstone.ChatGPT.Models;

namespace Chat.HRB.Repository
{
    public class ChatHRBRepository : BaseRepository, IChatHRBRepository
    {
        protected IServiceProvider _serviceProvider;
        protected IChatGPTClient _chatHRB;

        public ChatHRBRepository(IServiceProvider serviceProvider) : base()
        {
            _serviceProvider = serviceProvider;
            _chatHRB = _serviceProvider.GetService(typeof(IChatGPTClient)) as ChatGPTClient;
        }

        public async Task<string> Chat(string input)
        {
            // TODO first, persist the question

            //TODO take app id, search cosmos DB for baseline prompt/intent by app id


            string userInput = $"You: {input}\nMax Refund: ";
            string userPrompt = string.Concat(this._baseLineIntent, userInput);
            var chatHRBRequest = new ChatGPTCompletionRequest
            {
                Model = ChatGPTCompletionModels.Davinci,
                Prompt = userPrompt,
                MaxTokens = 2000
            };

            var result = await _chatHRB.CreateCompletionAsync(chatHRBRequest);
            var response = result.GetCompletionText();

            if (response != null)
            {
                return response;
            }
            return "";
        }

        //public async Task<string> GetChatHistroy(int taxyear) { }
    }
}

