using Microsoft.AspNetCore.Identity;
using Core.Utilities.Localization;

namespace Core.Utilities.Results;

public class Result : IResult
{
    public Result(bool success, string key, params object[] parameters) : this(success)
    {

    }
    public Result(bool success, List<string> keys, params object[] parameters) : this(success)
    {

    }
    public Result(bool success)
    {
        Success = success;
    }
    public bool Success { get; }

    public string Message { get; }

    public List<string> Messages { get; set; }
}

public class AsyncResult : IAsyncResult
{
    public AsyncResult(Task<bool> success, Task<string> key, params Task<object>[] parameters) : this(success)
    {
        Message = key;
    }
    public AsyncResult(Task<bool> success, params Task<object>[] parameters) : this(success)
    {

    }
    public AsyncResult(Task<bool> success,Task<List<string>> keys, params Task<object>[] parameters) : this(success)
    {
        Messages = keys;
    }

    public AsyncResult(Task<bool> success)
    {
        Success = success;
    }

    public Task<bool> Success { get; }

    public Task<string> Message { get; }

    public Task<List<string>> Messages { get; set; }
}
