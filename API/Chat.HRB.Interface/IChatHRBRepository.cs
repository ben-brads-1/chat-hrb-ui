#region Using Directives

using Chat.HRB.Models; 

#endregion
namespace Chat.HRB.Interface
{
    public interface IChatHRBRepository
    {
        #region Methods

        Task<string> Chat(string input);
        Task<PromptModel> GetPromptAsync(string appId);
        Task<PromptModel> UpdateOrInsertPrompt(string appId, PromptModel model);
        Task<List<string>> GetChatHistoryAsync(string userId, string appId, int year);
        Task<ChatHistoryModel> UpdateOrInsertChatHistory(string userId, string appId, int year, List<string> messages); 

        #endregion


    }
}

