
namespace Core.Utilities.Results;

public class DataResult<T> : Result, IDataResult<T>
{
    public DataResult(T data, bool success, string message, params object[] parameters) : base(success, message, parameters)
    {
        Data = data;
    }
    public DataResult(T data, bool success, List<string> messages, params object[] parameters) : base(success, messages, parameters)
    {
        Data = data;
    }
    public DataResult(T data, bool success) : base(success)
    {
        Data = data;
    }

    public T Data { get; }
}

public class AsyncDataResult<T> : AsyncResult, IAsyncDataResult<T>
{
    public AsyncDataResult(Task<T> data, Task<bool> success, Task<string> message, params Task<object>[] parameters)
        : base(success, message, parameters)
    {
        Data = data;
    }
    public AsyncDataResult(Task<T> data, Task<bool> success, Task<List<string>> messages, params Task<object>[] parameters) : base(success,messages, parameters)
    {
        Data = data;
    }
    public AsyncDataResult(Task<T> data, Task<bool> success) : base(success)
    {
        Data = data;
    }

    public Task<T> Data { get; }
}
