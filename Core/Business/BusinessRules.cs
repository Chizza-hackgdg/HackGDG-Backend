using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;

namespace Core.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }

            }
            return null;
        }

        public async static Task<IAsyncResult> RunAsync(params IAsyncResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!await logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
