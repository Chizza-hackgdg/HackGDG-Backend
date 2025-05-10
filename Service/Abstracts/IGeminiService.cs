using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstracts
{
    public interface IGeminiService
    {
        public Task<IAsyncDataResult<string>> GetResponseFromGeminiAsync(string prompt);
    }
}
