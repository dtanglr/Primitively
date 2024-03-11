using Acme.Catalog.Abstractions;
using Acme.Catalog.Api.Data;
using Acme.Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ICatalogContext, CatalogContext>();
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
    options.Register(PrimitiveLibrary.Repository);
})
// Add AspNetCore Mvc model binding support for Primitively types used in APIs.  This means strongly typed
// Primitively types can easily be used as querystring and route params too etc
.AddMvc()
// Add Swashbuckle OpenApi SchemaFilter so Primitively types are fully supported in the API Swagger documentation 
.AddSwaggerGen()
// Add MongoDB BsonSerializer configuration. This method also supports registering types individually. By default
// it will register a BSON serializer for each Primitively type in the PrimitivelyOptions registry.
// Any Primitively types that are IGuid primitives will be by default stored in Mongo as the default CSharpLegacy Base64
// strings unless overridden using the Bson serializer options (see below)
.AddBson();
// Alternatively, override the IGuid GuidRepresentation to use Standard (UUID) format instead of the default CSharpLegacy (Base64 string)
//.AddBson(builder =>
//    builder.Configure<BsonIGuidSerializerOptions>(options =>
//        options.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// Seed the database with test data
var cataglog = app.Services.GetRequiredService<ICatalogContext>();
await CatalogContextSeed.SeedDataAsync(cataglog.Products);

// Run the examples app
await app.RunAsync();
