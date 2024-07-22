namespace Ticketing.Dtos.BaseDtos;

public class BaseResponseDto<T>
{
    public bool IsSuccess { get; set; }
    public string StatusCode { get; set; }
    public bool Status { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}