using System.Net;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _repository.GetProducts();
        return Ok(products);
    }

    [HttpGet("id/{id:length(24)}", Name = "GetProductById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        var product = await _repository.GetProductById(id);

        if (product == null)
        {
            _logger.LogError("Product with id: {id}, not found.", id);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("guid/{guid}", Name = "GetProductByGuid")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductByGuid(Guid guid)
    {
        var product = await _repository.GetProductByGuid(guid);

        if (product == null)
        {
            _logger.LogError("Product with Guid: {guid}, not found.", guid);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("sku/{sku}", Name = "GetProductBySku")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductBySku(Sku sku)
    {
        var product = await _repository.GetProductBySku(sku);

        if (product == null)
        {
            _logger.LogError("Product with Sku: {sku}, not found.", sku);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("productId/{productId}", Name = "GetProductByProductId")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductByProductId(ProductId productId)
    {
        var product = await _repository.GetProductByProductId(productId);

        if (product == null)
        {
            _logger.LogError("Product with ProductId: {productId}, not found.", productId);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("category/{categoryId}", Name = "GetProductByCategoryId")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<Product>>> GetProductByCategoryId(CategoryId categoryId)
    {
        var products = await _repository.GetProducts(categoryId);

        if (products == null)
        {
            _logger.LogError("Products with CategoryId: {categoryId}, not found.", categoryId);
            return NotFound();
        }

        return Ok(products);
    }
}
