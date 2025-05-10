namespace Core.Utilities.Results;

public class SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(T data, string key) : base(data, true, key, new List<string>())
    {

    }
    public SuccessDataResult(T data, List<string> keys) : base(data, true, keys, new List<string>())
    {

    }
    public SuccessDataResult(T data) : base(data, true)
    {

    }
    public SuccessDataResult(string key) : base(default, true, key, new List<string>())
    {

    }
    public SuccessDataResult(List<string> keys) : base(default, true, keys, new List<string>())
    {

    }
    public SuccessDataResult() : base(default, true)
    {

    }
}

public class AsyncSuccessDataResult<T> : AsyncDataResult<T>
{

    public AsyncSuccessDataResult(Task<T> data, Task<string> key) : base(data, Task.FromResult(true), key, Task.FromResult(new object()))
    {

    }
    public AsyncSuccessDataResult(Task<T> data, Task<List<string>> keys) : base(data, Task.FromResult(true), keys, Task.FromResult(new object()))
    {

    }
    public AsyncSuccessDataResult(Task<T> data) : base(data, Task.FromResult(true), Task.FromResult(string.Empty), Task.FromResult(new object()))
    {

    }
    public AsyncSuccessDataResult(Task<List<string>> keys) : base(Task.FromResult(default(T)), Task.FromResult(true), keys, Task.FromResult(new object()))
    {

    }
    public AsyncSuccessDataResult(Task<string> key) : base(Task.FromResult(default(T)), Task.FromResult(true), key, Task.FromResult(new object()))
    {

    }
    public AsyncSuccessDataResult() : base(Task.FromResult(default(T)), Task.FromResult(true), Task.FromResult(string.Empty), Task.FromResult(new object()))
    {

    }
}
