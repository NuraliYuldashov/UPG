using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryInterface category) : ControllerBase
{
    private readonly ICategoryInterface _category = category;

    [HttpGet]
    public async Task<IActionResult> GetAll ()
    {
        var categories = await _category.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _category.GetByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public IActionResult Post(Category category)
    {
        _category.Add(category);
        return Ok();
    }

    [HttpPut]
    public IActionResult Put(Category category)
    {
        _category.Update(category);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _category.Delete(id);
        return Ok();
    }
}
