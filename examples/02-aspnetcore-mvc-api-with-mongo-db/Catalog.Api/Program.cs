﻿using Catalog.Abstractions;
using Catalog.Api.Data;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;
using MongoDB.Bson;
using Primitively.AspNetCore.Mvc;
using Primitively.AspNetCore.SwaggerGen;
using Primitively.Configuration;
using Primitively.MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddControllers(options =>
{
    // Removed text/plain output support
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add primitively configuration
builder.Services.AddPrimitively(options =>
{
    // Register the location of source generated Primitively types within the application
    // NB. No need to use reflection to scan assemblies! Each class library that contains source
    // generated Primitively types also has a PrimitiveLibrary static helper class. Meta data such
    // as type name, underlying data type, example value, min / max Length etc can then be obtained
    // from the PrimitiveRepository instance within each class library
    options.Register(PrimitiveLibrary.Respository);
})
// Add AspNetCore Mvc model binding support for Primitively types used in APIs.  This means strongly typed
// Primitively types can easily be used as querystring and route params too etc
.AddMvc()
// Add Swashbuckle OpenApi SchemaFilter so Primitively types are fully supported in the API Swagger documentation 
.AddSwaggerGen()
// Add MongoDB BsonSerializer configuration
// During initialization each type will have a Primitiely BSON serializer registered on it's behalf.
// This method also supports registering types individually with a custom serializer. A custom serialiser
// can also be registered to replace the default serializer for any given Primitively DataType.
.AddBson();

// Override MongoDB.Driver v2 warnings: 
#pragma warning disable CS0618
BsonDefaults.GuidRepresentation = GuidRepresentation.Standard; // GuidRepresentation.CSharpLegacy: default
BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3; // V3 = Apply setting to collection | V2 = Apply setting to property
#pragma warning restore CS0618

// Now seed the database
builder.Services.AddSingleton<ICatalogContext>(new CatalogContext(builder.Configuration));

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
