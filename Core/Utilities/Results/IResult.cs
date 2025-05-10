namespace Core.Utilities.Results;
public interface IResult 
{ 
    public bool Success {get;} 
    public string Message { get; }
    public List<string> Messages { get; }
}

public interface IAsyncResult
{
    public Task<bool> Success { get; }
    public Task<string> Message { get; }
    public Task<List<string>> Messages { get; } 
}