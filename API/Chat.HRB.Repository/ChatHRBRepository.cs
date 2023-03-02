#region Using Directives

using Chat.HRB.Common.Interfaces;
using Chat.HRB.Interface;
using Chat.HRB.Models;
using Whetstone.ChatGPT;
using Whetstone.ChatGPT.Models; 

#endregion

namespace Chat.HRB.Repository
{
    public class ChatHRBRepository : BaseRepository, IChatHRBRepository
    {
        #region Fields
        
        private IServiceProvider _serviceProvider;
        private IChatGPTClient _chatHRB;

        #endregion

        #region Constructor
       
        public ChatHRBRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _chatHRB = _serviceProvider.GetService(typeof(IChatGPTClient)) as ChatGPTClient;
        }

        #endregion

        #region Properties
       
        private IDocumentDbRepository ChatHistoryDocumentDbRepository => this.DocumentDbRepositoryFactory.GetDocumentDbRepository(DocumentDbType.ChatHistory);
        private IDocumentDbRepository PromtDocumentDbRepository => this.DocumentDbRepositoryFactory.GetDocumentDbRepository(DocumentDbType.Prompt);

        #endregion

        #region Methods
        
        public async Task<string> Chat(string input)
        {
            // TODO first, persist the question

            //TODO take app id, search cosmos DB for baseline prompt/intent by app id


            string userInput = $"You: {input}\nMax Refund: ";
            string userPrompt = string.Concat(this.BaseLineIntent, userInput);
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

        public async Task<PromptModel> GetPromptAsync(string appId)
        {
            var result = await this.PromtDocumentDbRepository.GetItemAsync<PromptModel>(appId, appId);
            return result;

        }

        public async Task<List<string>> GetChatHistoryAsync(string userId, string appId, int year)
        {
            var chatData = await this.ChatHistoryDocumentDbRepository.GetItemAsync<ChatHistoryModel>(userId, userId);
            if (chatData != null)
            {
                var filteredChatHistory = chatData.Messages?.Where(m => m.AppId == appId && m.Year == year).ToList();
                if (filteredChatHistory != null && filteredChatHistory.Any())
                {
                    return filteredChatHistory[0].Messages;
                }
            }
            return new List<string>();
        }
        public async Task<ChatHistoryModel> UpdateOrInsertChatHistory(string userId, string appId, int year, List<string> messages)
        {
            var chatDataModel = await this.ChatHistoryDocumentDbRepository.GetItemAsync<ChatHistoryModel>(userId, userId);
            if (chatDataModel == null)
            {
                chatDataModel = new ChatHistoryModel() { UserId = userId };
                chatDataModel.Messages = new List<ChatMessageData>();
                var chatMessage = new ChatMessageData() { AppId = appId, UserId = userId, Year = year };
                chatMessage.Messages.AddRange(messages);
                chatDataModel.Messages.Add(chatMessage);

                chatDataModel = await ChatHistoryDocumentDbRepository.CreateItemAsync<ChatHistoryModel>(chatDataModel).ConfigureAwait(false);
            }
            else
            {
                var chatMessage = chatDataModel.Messages?.FirstOrDefault<ChatMessageData>(m => m.AppId == appId && m.Year == year);
                if (chatMessage == null)
                {
                    chatDataModel.Messages = new List<ChatMessageData>();
                    var newChatMessage = new ChatMessageData() { AppId = appId, UserId = userId, Year = year };
                    newChatMessage.Messages.AddRange(messages);
                    chatDataModel.Messages.Add(newChatMessage);
                }
                else
                {
                    chatMessage.Messages.AddRange(messages);
                }
                chatDataModel = await ChatHistoryDocumentDbRepository.UpdateItemAsync<ChatHistoryModel>(userId, chatDataModel).ConfigureAwait(false);
            }
            return chatDataModel;

        }

        public async Task<PromptModel> UpdateOrInsertPrompt(string appId, PromptModel model)
        {
            var prompt = await PromtDocumentDbRepository.GetItemAsync<PromptModel>(appId, appId).ConfigureAwait(false) == null
                           ? await PromtDocumentDbRepository.CreateItemAsync<PromptModel>(model).ConfigureAwait(false)
                           : await PromtDocumentDbRepository.UpdateItemAsync<PromptModel>(appId, model).ConfigureAwait(false);

            return prompt;
        } 
       
        #endregion
        //public async Task<string> GetChatHistroy(int taxyear) { }
    }

}

