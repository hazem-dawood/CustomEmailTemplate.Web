namespace CustomEmailTemplate.Application.Models;
public class ExecuteViewAsPdfDto<T>
{
    public T? Model { get; set; }

    public string FullViewPath { get; set; } = string.Empty;

    public ViewDataDictionary? ViewDataDictionary { get; set; }

    public bool IsPartial { get; set; }
}