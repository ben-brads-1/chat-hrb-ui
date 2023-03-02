#region Using Directives

using Chat.HRB.Common.CosmosDB;
using Chat.HRB.Common.Interfaces;
using Chat.HRB.Interface;
using Chat.HRB.Repository;
using Microsoft.Extensions.DependencyInjection;
using Whetstone.ChatGPT;
using Whetstone.ChatGPT.Models; 

#endregion

namespace Chat.HRB.Infrastructure.Registration
{
    #region Methods
   
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //register the repository
            services.AddTransient<IChatHRBRepository, ChatHRBRepository>();
            services.AddTransient<IDocumentDbRepositoryFactory, DocumentDbRepositoryFactory>();
            services.AddTransient<IDocumentDbRepository, AzureDocumentDbRepository>();


            //configure chatGPTCredentials
            services.Configure<ChatGPTCredentials>(options =>
            {
                options.ApiKey = "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";
            });
            services.AddHttpClient<IChatGPTClient, ChatGPTClient>();
        }
    } 
   
    #endregion
}

