﻿using System.Net;
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

    [HttpGet("{id:length(24)}", Name = "GetProductById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        var product = await _repository.GetProduct(id);

        if (product == null)
        {
            _logger.LogError("Product with id: {id}, not found.", id);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("{sku}", Name = "GetProductBySku")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductBySku(Sku sku)
    {
        var product = await _repository.GetProduct(sku);

        if (product == null)
        {
            _logger.LogError("Product with SKU: {sku}, not found.", sku);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("{sku}", Name = "GetProductBySku2")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductBySku2(Sku sku)
    {
        var product = await _repository.GetProduct2(sku);

        if (product == null)
        {
            _logger.LogError("Product with SKU: {sku}, not found.", sku);
            return NotFound();
        }

        return Ok(product);
    }
}
