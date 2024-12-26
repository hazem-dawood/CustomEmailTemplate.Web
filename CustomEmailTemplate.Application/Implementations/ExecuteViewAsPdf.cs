namespace CustomEmailTemplate.Application.Implementations;

internal class ExecuteViewAsPdfService(IHttpContextAccessor httpContextAccessor, ICompositeViewEngine viewEngine,
    ITempDataProvider tempDataProvider, IStringLocalizer<ExecuteViewAsPdfService> localizer, ILogger<ExecuteViewAsPdfService> logger,
    CancellationToken cancellationToken) : IExecuteViewAsPdfService
{
    public async Task<ResultDto<byte[]>> AsPdf<T>(ExecuteViewAsPdfDto<T> viewAsPdfDto)
    {
        try
        {
            #region Validation

            if (httpContextAccessor.HttpContext == null || string.IsNullOrWhiteSpace(viewAsPdfDto.FullViewPath))
                return new ResultDto<byte[]> { Messages = [localizer["Invalid Data"]] };

            #endregion

            var controller = new TempController
            {
                ControllerContext = new ControllerContext
                {
                    ActionDescriptor = new ControllerActionDescriptor
                    {
                        ControllerName = nameof(TempController).Replace(nameof(Controller), ""),
                        ActionName = nameof(TempController.ExecutePdf),
                    },
                    RouteData = new RouteData(),
                    HttpContext = httpContextAccessor.HttpContext!
                },
            };

            if (controller.ExecutePdf(viewAsPdfDto) is not ViewAsPdf result)
                return new ResultDto<byte[]> { Messages = [localizer["ViewIsNotPdfResult"]] };

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException(cancellationToken);


            var file = await result.BuildFile(controller.ControllerContext);

            return new ResultDto<byte[]>
            {
                IsSuccess = true,
                Data = file
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(AsPdf));
            return new ResultDto<byte[]> { Messages = [ex.Message] };
        }
    }

    public async Task<ResultDto<string>> AsHtml<T>(ExecuteViewAsPdfDto<T> viewAsPdfDto)
    {
        try
        {
            #region Validation

            if (httpContextAccessor.HttpContext == null || string.IsNullOrWhiteSpace(viewAsPdfDto.FullViewPath))
                return new ResultDto<string> { Messages = [localizer["Invalid Data"]] };

            #endregion

            var actionContext = new ActionContext(
                httpContextAccessor.HttpContext,
                httpContextAccessor.HttpContext.GetRouteData(),
                new ActionDescriptor());

            await using var sw = new StringWriter();
            var viewResult = viewEngine.GetView(null, viewAsPdfDto.FullViewPath, isMainPage: true);

            if (!viewResult.Success)
                return new ResultDto<string> { Messages = [localizer["Failed To Execute View"]] };

            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = viewAsPdfDto.Model
            };

            var tempData = new TempDataDictionary(actionContext.HttpContext, tempDataProvider);

            var viewContext = new ViewContext(actionContext, viewResult.View,
                viewDictionary, tempData, sw, new HtmlHelperOptions());

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException(cancellationToken);

            await viewResult.View.RenderAsync(viewContext);

            return new ResultDto<string>
            {
                IsSuccess = true,
                Data = sw.ToString(),
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(AsPdf));
            return new ResultDto<string> { Messages = [ex.Message] };
        }
    }
}
