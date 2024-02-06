using System.Net;
using Acme.Catalog.Api.Entities;
using Acme.Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Catalog.Api.Controllers;

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

    [HttpGet("products", Name = $"{nameof(GetProducts)}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _repository.GetProducts();
        return Ok(products);
    }

    [HttpGet("products/{productId}", Name = $"{nameof(GetProductById)}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(ProductId productId)
    {
        var product = await _repository.GetProduct(productId);

        if (product == null)
        {
            _logger.LogError("Product with ProductId: {ProductId}, not found.", productId);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("products/sku/{sku}", Name = $"{nameof(GetProductBySku)}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductBySku(Sku sku)
    {
        var product = await _repository.GetProduct(sku);

        if (product == null)
        {
            _logger.LogError("Product with Sku: {Sku}, not found.", sku);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("products/category/{categoryId}", Name = $"{nameof(GetProductsByCategoryId)}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(CategoryId categoryId)
    {
        var products = await _repository.GetProducts(categoryId);

        if (products == null)
        {
            _logger.LogError("Products with CategoryId: {CategoryId}, not found.", categoryId);
            return NotFound();
        }

        return Ok(products);
    }
}
