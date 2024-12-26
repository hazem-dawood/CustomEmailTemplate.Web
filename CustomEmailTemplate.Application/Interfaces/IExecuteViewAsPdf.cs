namespace CustomEmailTemplate.Application.Interfaces;

/// <summary>
/// Execute a view as pdf or html
/// </summary>
public interface IExecuteViewAsPdfService
{
    /// <summary>
    ///    Execute a view as a pdf, so you can it send it as attachment in email,...
    /// </summary>
    /// <typeparam name="T">T is model that pass to the view</typeparam>
    /// <param name="model">contains information about the required view such as <see cref="ExecuteViewAsPdfDto{T}.FullViewPath"/>,...</param>
    /// <returns></returns>
    public Task<ResultDto<byte[]>> AsPdf<T>(ExecuteViewAsPdfDto<T> model);

    /// <summary>
    ///    Execute a view as a html, so you can it send it as attachment as email
    /// </summary>
    /// <typeparam name="T">T is model that pass to the view</typeparam>
    /// <param name="model">contains information about the required view such as <see cref="ExecuteViewAsPdfDto{T}.FullViewPath"/>,...</param>
    /// <returns></returns>
    public Task<ResultDto<string>> AsHtml<T>(ExecuteViewAsPdfDto<T> model);
}