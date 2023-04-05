using dominantColorsUmbraco.Models;
using dominantColorsUmbraco.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace dominantColorsUmbraco.Controllers;

public class HomeController : RenderController
{
    private readonly IColourService _colourService;

    public HomeController(
        ILogger<HomeController> logger, 
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor, 
        IColourService colourService) 
    
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _colourService = colourService;
    }

    [NonAction]
    public override IActionResult Index() => new NotFoundResult();

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var homePage = CurrentPage as Home;

        var images = homePage!.ImagesToSample;

        if (images == null)
        {
            throw new ArgumentException("YOU NEED IMAGES SELECTED");
        }

        var actualImages = images.Select(x => x.Content as Image).ToList();
        var focalPointRectangles = GetFocalPointRectangles(actualImages);

        var imagesWithColour = await _colourService.GetImagesWithColour(focalPointRectangles);

        return View("~/Views/Home.cshtml", imagesWithColour);
    }

    private static List<FocalPointRectangle> GetFocalPointRectangles(List<Image?> actualImages)
    {
        // Yes. This could probably be moved to another file and look better, meh.
        var focalPointRecatangles = new List<FocalPointRectangle>();
        foreach (var image in actualImages)
        {
            if (image?.UmbracoFile == null)
            {
                continue;
            }

            var focalPointRectangle = new FocalPointRectangle();
            if (!image.UmbracoFile.HasFocalPoint())
            {
                // No focal point is set, so default to center of the image.
                // This can have negative consequence if it's not the desired image (reset on the red image)
                focalPointRectangle.Left = 0.5m;
                focalPointRectangle.Top = 0.5m;
            }
            else
            {
                focalPointRectangle.Left = image.UmbracoFile.FocalPoint.Left;
                focalPointRectangle.Top = image.UmbracoFile.FocalPoint.Top;
            }

            focalPointRectangle.Width = image.UmbracoWidth;
            focalPointRectangle.Height = image.UmbracoHeight;

            focalPointRectangle.Image = image.UmbracoFile.Src;
            focalPointRecatangles.Add(focalPointRectangle);
        }

        return focalPointRecatangles;
    }
}