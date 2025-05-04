namespace apbd_tutorial8.Service;

public class ServiceResult<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }

    public static ServiceResult<T> Ok(T data)=> new ServiceResult<T> { Success = true,Data = data };
    public static ServiceResult<T> Error(string message)=> new ServiceResult<T> { Success = false, Message = message };
}