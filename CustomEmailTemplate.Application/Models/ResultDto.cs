namespace CustomEmailTemplate.Application.Models;

/// <summary>
/// Main Response for all endpoints
/// </summary>
/// <typeparam name="T"><see cref="T"/> is Model</typeparam>
public class ResultDto<T>
{
    /// <summary>
    /// is success or not
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// messages in success or error
    /// </summary>
    public List<string> Messages { get; set; } = [];

    /// <summary>
    /// data
    /// </summary>
    public T? Data { get; set; }
}