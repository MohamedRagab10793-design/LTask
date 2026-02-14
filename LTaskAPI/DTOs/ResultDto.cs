namespace LTaskAPI.DTOs;

public class ResultDto<T>
{
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; }
    public T Data { get; set; }
}
