using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.Common.Validators.AccessoriesValidators;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using DTOS.AccessoriesDtos;
using Infastructure.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UPG.Core.Filters;

namespace Application.Services;

public class AccessoriesService : IAccessoriesService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUploadImageService _uploadImage;
    private readonly IDistributedCache _distributed;
    private const string CACHE_KEY = "accessory";
    private readonly IRedisService<Accessories> _cache;

    public AccessoriesService(IMapper mapper, 
                              IUnitOfWork unitOfWork, 
                              IUploadImageService uploadImage, 
                              IDistributedCache distributed)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _uploadImage = uploadImage;
        _distributed = distributed;
        _cache = new RedisService<Accessories>(distributed);
}

    public async Task AddAccessoriesAsync(AddAccessoriesDto addAccessoriesDto)
    {
        if (addAccessoriesDto == null) throw new ArgumentException("Accessoru was null!");

        var validator = new AccessoriesValidator();
        var validatorResult = validator.Validate(addAccessoriesDto);

        if (!validatorResult.IsValid)
        {
            throw new ResponseErrors() { Errors = validatorResult.Errors.ToList() };
        }
        var config = _mapper.Map<Accessories>(addAccessoriesDto);
        _unitOfWork.Accessories.Add(config);
        await _unitOfWork.SaveAsync();
        _distributed.Remove(CACHE_KEY);
    }

    public async Task DeleteAccessoriesAsync(int id)
    {
        var accessory = await _unitOfWork.Accessories.GetByIdAsync(id);
        if (accessory == null) throw new NotFoundException("Accessory not found!");

        var images = accessory.ImageUrls;
        await _uploadImage.DeleteAsync(images);
        _unitOfWork.Accessories.Delete(id);
        await _unitOfWork.SaveAsync();
        _distributed.Remove(CACHE_KEY);
    
    }

    public async Task<IEnumerable<AccessoriesDto>> GetAccessoriesAsync()
    {
        var accessories = await _cache.GetFromCacheAsync(CACHE_KEY);
        if (accessories == null)
        {
            accessories = await _unitOfWork.Accessories.GetAllAsync();
            await _cache.SaveToCacheAsync(JsonConvert.SerializeObject(accessories, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }), CACHE_KEY);
        }
        return accessories.Select(accessory => _mapper.Map<AccessoriesDto>(accessory));
    }

    public async Task<AccessoriesDto> GetAccessoriesByIdAsync(int id)
    {
        var accessories = await _cache.GetFromCacheAsync(CACHE_KEY);
        if(accessories == null)
        {
            accessories = await _unitOfWork.Accessories.GetAllAsync();
            await _cache.SaveToCacheAsync(JsonConvert.SerializeObject(accessories, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }), CACHE_KEY);
        }
        var accessory = accessories.FirstOrDefault(i => i.Id == id);
        if (accessory == null) throw new NotFoundException("Accessory not found!");
        return _mapper.Map<AccessoriesDto>(accessory);
    }

    public async Task UpdateAccessoriesAsync(UpdateAccessoriesDto updateAccessoriesDto)
    {
        var accessory = await _unitOfWork.Accessories.GetByIdAsync(updateAccessoriesDto.ID);
        if (accessory == null) throw new NotFoundException("Accessory not found");


        var validator = new UpdateAccessoriesDtoValidator();
        var validatorResult = validator.Validate(updateAccessoriesDto);

        if (!validatorResult.IsValid)
        {
            throw new ResponseErrors() { Errors = validatorResult.Errors.ToList() };
        }

        var images = accessory.ImageUrls.Except(updateAccessoriesDto.ImageUrls).ToList();

        await _uploadImage.DeleteAsync(images);
        var upaccessuary = _mapper.Map<Accessories>(updateAccessoriesDto);
        _unitOfWork.Accessories.Update(upaccessuary);
        await _unitOfWork.SaveAsync();
        _distributed.Remove(CACHE_KEY);
    }

    public async Task<IEnumerable<AccessoriesDto>> GetAllAccessoriesByCategoryIdAsync(int categoryId)
    {
        var models = await _unitOfWork.Accessories.GetAccessoriesByCategoryIdAsync(categoryId);
        return _mapper.Map<IEnumerable<AccessoriesDto>>(models);   
    }

    public async Task<IEnumerable<AccessoriesDto>> GetAllAccessoriesByCategoryNameAsync(string categoryName)
    {
        var models = await _unitOfWork.Accessories.GetAccessoriesByCategoryNameAsync(categoryName);
        return _mapper.Map<IEnumerable<AccessoriesDto>>(models);
    }

    public async Task<List<AccessoriesDto>> FilterByCategoryNameAsync(string categoryName, AccessoriesFilter accessoriesFilter)
    {
        var models = await _unitOfWork.Accessories.GetFilteredAccessoriesByCategoryNameAsync(categoryName, accessoriesFilter);
        return _mapper.Map<List<AccessoriesDto>>(models);
    }

    public async Task<List<AccessoriesDto>> FilterByCategoryIdAsync(int id, AccessoriesFilter accessoriesFilter)
    {
        var accessories = await _unitOfWork.Accessories.GetFilteredAccessoriesByCategoryIdAsync(id, accessoriesFilter);
        return _mapper.Map<List<AccessoriesDto>>(accessories);
    }
}
