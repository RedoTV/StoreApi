using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;
using ProductsApi.Services;

namespace ProductsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IRepository _repository;

    public ProductController(ILogger<ProductController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Product> products = await _repository.GetAllProductsAsync();
        if(products.Count() < 1)
        {
            Exception exception = new Exception("Db is empty");
            _logger.LogError(exception.Message);
            return NotFound(exception.Message);
        }
        return Ok(products);
    }

    [HttpGet]
    [Route("get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Product? product = null;
        try
        {
            _logger.LogInformation("Get product...");
            product = await _repository.GetProductAsync(id);
        }
        catch (Exception exc)
        {
            _logger.LogError(exc.Message);
            return NotFound(exc.Message);
        }

        return Ok(product);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add(AddProductModel product)
    {
        Product? addedProduct = null;
        try
        {
            _logger.LogInformation("Adding product...");
            addedProduct = await _repository.AddProductAsync(product);
        }
        catch (Exception exc)
        {
            _logger.LogError(exc.Message);
            return BadRequest(exc.Message);
        }
        return Ok(addedProduct);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update(UpdateProductModel product)
    {
        Product? updatedProduct = null;
        try
        {
            _logger.LogInformation("Update product info...");
            updatedProduct = await _repository.UpdateProductInfoAsync(product);
        }
        catch (Exception exc)
        {
            return NotFound(exc.Message);
        }
        if(updatedProduct is null)
        {
            Exception exception = new Exception("Db don't exsist product with this id");
            _logger.LogError(exception.Message);
            return NotFound(null);
        }
        return Ok(updatedProduct);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Product? currentProduct = null;
        try
        {
            currentProduct =  await _repository.DeleteProductAsync(id);
        }
        catch (Exception exc)
        {
            _logger.LogError(exc.Message);
            return NotFound(exc.Message);
        }
        return Ok(currentProduct);
    }
}
