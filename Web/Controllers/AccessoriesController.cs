﻿using Application.Common.Exceptions;
using Application.Interfaces;
using DTOS.AccessoriesDtos;
using Microsoft.AspNetCore.Mvc;
using UPG.Core.Filters;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessoriesController : ControllerBase
{
    private readonly IAccessoriesService _accessoriesService;

    public AccessoriesController(IAccessoriesService accessoriesService)
    {
        _accessoriesService = accessoriesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _accessoriesService.GetAccessoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var category = await _accessoriesService.GetAccessoriesByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddAccessoriesDto dto)
    {
        try
        {
            await _accessoriesService.AddAccessoriesAsync(dto);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ResponseErrors ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateAccessoriesDto dto)
    {
        try
        {
            await _accessoriesService.UpdateAccessoriesAsync(dto);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ResponseErrors ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]    
    public async Task<IActionResult> Delete(int id)
    {
        await _accessoriesService.DeleteAccessoriesAsync(id);
        return Ok();
    }

    [HttpGet("FilterByCategoryIdAsync")]
    public async Task<IActionResult> FilterByCategoryIdAsync([FromQuery] int categoryId, AccessoriesFilter filter)
    {
        try
        {
            var accessories = await _accessoriesService.FilterByCategoryIdAsync(categoryId, filter);
            return Ok(accessories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet("FilterByCategoryNameAsync")]
    public async Task<IActionResult> FilterByCategoryNameAsync([FromQuery] string categoryName, AccessoriesFilter filter)
    {
        try
        {
            var accessories = await _accessoriesService.FilterByCategoryNameAsync(categoryName, filter);
            return Ok(accessories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("byCategoryId")]
    public async Task<IActionResult> GetAllAccessoriesByCategoryIdAsync([FromQuery] int categoryId)
    {
        try
        {
            var accessories = await _accessoriesService.GetAllAccessoriesByCategoryIdAsync(categoryId);
            return Ok(accessories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet("byCategoryName")]
    public async Task<IActionResult> GetAllAccessoriesByCategoryNameAsync([FromQuery] string categoryName)
    {
        try
        {
            var accessories = await _accessoriesService.GetAllAccessoriesByCategoryNameAsync(categoryName);
            return Ok(accessories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}
