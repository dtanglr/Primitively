﻿using Catalog.Api.Entities;

namespace Catalog.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<Product> GetProduct(Sku sku);
    Task<List<Product>> GetProducts(CategoryId categoryId);
}
