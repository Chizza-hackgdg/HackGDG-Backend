namespace Core.Utilities.Results;

public class SuccessResult : Result
{
    public SuccessResult(string key) : base(true, key)
    {

    }
    public SuccessResult(List<string> keys) : base(true, keys)
    {

    }
    public SuccessResult() : base(true)
    {

    }

}
public class AsyncSuccessResult : AsyncResult
{
    public AsyncSuccessResult() : base(Task.FromResult(true))
    {

    }
    public AsyncSuccessResult(Task<string> key, params Task<object>[] parameters) : base(Task.FromResult(true), key, parameters)
    {

    }
    public AsyncSuccessResult(Task<List<string>> keys, params Task<object>[] parameters) : base(Task.FromResult(true), keys, parameters)
    {

    }
}
