namespace Core.Utilities.Results;

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult(T data, string key) : base(data, false, key, new List<string>())
    {

    }

    public ErrorDataResult(T data, List<string> keys) : base(data, false, keys, new List<string>())
    {

    }
    public ErrorDataResult(T data) : base(data, false, string.Empty, new List<string>())
    {

    }
    public ErrorDataResult(string key) : base(default, false, key, new List<string>())
    {

    }

    public ErrorDataResult(List<string> keys) : base(default, false, keys, new List<string>())
    {

    }
    public ErrorDataResult() : base(default, false, string.Empty, new List<string>())
    {

    }
}
public class AsyncErrorDataResult<T> : AsyncDataResult<T>
{
    public AsyncErrorDataResult(Task<T> data, Task<string> key) : base(data, Task.FromResult(false), key, Task.FromResult(new object()))
    {

    }
    public AsyncErrorDataResult(Task<T> data, Task<List<string>> keys) : base(data, Task.FromResult(false), keys, Task.FromResult(new object()))
    {

    }
    public AsyncErrorDataResult(Task<T> data) : base(data, Task.FromResult(false), Task.FromResult(string.Empty), Task.FromResult(new object()))
    {

    }
    public AsyncErrorDataResult(Task<string> key) : base(Task.FromResult(default(T)), Task.FromResult(false), key, Task.FromResult(new object()))
    {

    }
    public AsyncErrorDataResult(Task<List<string>> keys) : base(Task.FromResult(default(T)), Task.FromResult(false), keys, Task.FromResult(new object()))
    {

    }
    public AsyncErrorDataResult() : base(Task.FromResult(default(T)), Task.FromResult(false), Task.FromResult(string.Empty), Task.FromResult(new object()))
    {

    }
}
