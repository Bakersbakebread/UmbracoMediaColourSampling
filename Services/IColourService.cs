using dominantColorsUmbraco.Models;

namespace dominantColorsUmbraco.Services;

public interface IColourService
{
    Task<IEnumerable<ImageWithColour>> GetImagesWithColour(IEnumerable<FocalPointRectangle> imageFiles);
}