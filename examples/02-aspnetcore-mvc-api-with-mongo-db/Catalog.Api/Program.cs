using Catalog.Api;
using Catalog.Api.Data;
using Catalog.Api.Repositories;
using Primitively.AspNetCore.Mvc;
using Primitively.AspNetCore.SwaggerGen;
using Primitively.Configuration;
using Primitively.MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add primitively configuration
var primitively = builder.Services.AddPrimitively(options =>
{
    // Register the location of the source generated Primitively types
    // NB. No need to use reflection to scan assemblies! Each class library that contains source
    // generated Primitively types also has a PrimitiveLibrary static class. Meta-data such
    // as type name, underlying data type, example value, Min / Max Length etc can then be obtained
    // from the PrimitiveRepository instance
    options.Register(PrimitiveLibrary.Respository);
});

// Add AspNetCore Mvc model binding support for Primitively types used in APIs.  This means strongly typed
// Primitively types can easily be used as querystring and route params too etc
primitively.AddMvc();

// Add Swashbuckle OpenApi SchemaFilter so Primitively types are fully supported in the API Swagger documentation 
primitively.AddSwaggerGen();

// Add MongoDB BsonSerializer configuration
// During initialization each type will have an approipriate Primitiely BSON serializer registered
// This method also support registering types individually with a custom serializer and/or custom
// serialisers can be registered to replace the default serializer for the any given Primitively DataType.
primitively.AddBson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
