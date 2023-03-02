using System;
namespace Chat.HRB.Interface
{
    public interface IChatHRBRepository
    {
        Task<string> Chat(string input);
    }
}

