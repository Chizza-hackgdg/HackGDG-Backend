using Microsoft.AspNetCore.Identity;

namespace Core.Utilities.Results;

public class ErrorResult : Result
{
    public ErrorResult(string key) : base(false, key)
    {

    }

    public ErrorResult(List<string> keys) : base(false, keys)
    {

    }
    public ErrorResult() : base(false)
    {

    }
}
public class AsyncErrorResult : AsyncResult
{
    public AsyncErrorResult() : base(Task.FromResult(false))
    {

    }
    public AsyncErrorResult(Task<string> key) : base(Task.FromResult(false), key)
    {

    }
    public AsyncErrorResult(Task<List<string>> keys) : base(Task.FromResult(false), keys)
    {

    }
}
