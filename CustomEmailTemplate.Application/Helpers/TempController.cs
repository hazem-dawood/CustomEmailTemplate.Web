namespace CustomEmailTemplate.Application.Helpers;

/// <summary>
/// this controller is used only as temp, so we can render view as pdf
/// </summary>
public class TempController : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"><see cref="T"/> is Model </typeparam>
    /// <param name="model"></param>
    /// <returns></returns>
    public IActionResult ExecutePdf<T>(ExecuteViewAsPdfDto<T> model)
    {
        return new ViewAsPdf(model.FullViewPath, model.Model, model.ViewDataDictionary, model.IsPartial);
    }
}
