using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayers.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UploadImagesController(IUploadImageService imageService)
    : ControllerBase
{
    private readonly IUploadImageService _imageService = imageService;


    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        try
        {
            var result = await _imageService.UploadAsync(file);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] string url)
    {
        try
        {
            
            await _imageService.DeleteAsync(url);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("multiple")]
    public async Task<IActionResult> UploadImage(List<IFormFile> files)
    {
        try
        {
            

            var result = await _imageService.UploadAsync(files);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("multiple")]
    public async Task<IActionResult> DeleteImage(List<string> urls)
    {
        try
        {
            await _imageService.DeleteAsync(urls);
            return Ok();
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}