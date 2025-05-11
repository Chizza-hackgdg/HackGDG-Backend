using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Abstracts
{
    public interface IGeminiService
    {
         Task<IAsyncDataResult<string>> CreateJSONMilestones(Guid id);
        Task<IAsyncDataResult<string>> AnswerForumQuestion(Guid id);
        Task<IAsyncDataResult<string>> ChatBotResponse(string prompt);
    }
}
