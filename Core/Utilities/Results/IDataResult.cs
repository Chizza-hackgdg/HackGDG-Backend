﻿namespace Core.Utilities.Results;

public interface IDataResult<T>
{
    public T Data { get; }
    public bool Success { get; }
    public string Message { get; }
    public List<string> Messages { get; }
}

public interface IAsyncDataResult<T>
{
    public Task<T> Data { get; }
    public Task<bool> Success { get; }
    public Task<string> Message { get; }
    public Task<List<string>> Messages { get; }
}