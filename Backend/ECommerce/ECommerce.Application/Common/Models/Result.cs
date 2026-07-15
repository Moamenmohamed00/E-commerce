namespace ECommerce.Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("A successful result cannot have an error message.");
        
        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("A failure result must have an error message.");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);
}

public class Result<T> : Result
{
    private readonly T? _data;

    public T Data => IsSuccess 
        ? _data! 
        : throw new InvalidOperationException("Cannot access the value of a failure result.");

    protected Result(bool isSuccess, string error, T? data) 
        : base(isSuccess, error)
    {
        _data = data;
    }

    public static Result<T> Success(T data) => new(true, string.Empty, data);
    public static new Result<T> Failure(string error) => new(false, error, default);
}