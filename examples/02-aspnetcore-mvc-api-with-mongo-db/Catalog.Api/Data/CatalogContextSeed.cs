using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public static class CatalogContextSeed
{
    public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
    {
        var existProduct = await productCollection.Find(p => true).AnyAsync();

        if (!existProduct)
        {
            await productCollection.InsertManyAsync(GetPreconfiguredProducts());
        }
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        var smartPhone = new Category { CategoryId = (CategoryId)CategoryId.Example, Name = "Smart Phone" };
        var whiteApplicance = new Category { CategoryId = CategoryId.New(), Name = "White Appliances" };
        var homeKitchen = new Category { CategoryId = CategoryId.New(), Name = "Home Kitchen" };

        yield return new()
        {
            Id = "602d2149e773f2a3990b47f5",
            Sku = (Sku)Sku.Example, // Sku property type stored in default Bson.Binary format
            Name = "IPhone X",
            Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
            ImageFile = "product-1.png",
            Price = 950.00M,
            Category = smartPhone
        };

        yield return new()
        {
            Id = "602d2149e773f2a3990b47f6",
            Sku = Sku.New(),
            Name = "Samsung 10",
            Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
            ImageFile = "product-2.png",
            Price = 840.00M,
            Category = smartPhone
        };

        yield return new()
        {
            Id = "602d2149e773f2a3990b47f7",
            Sku = Sku.New(),
            Name = "Huawei Plus",
            Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
            ImageFile = "product-3.png",
            Price = 650.00M,
            Category = smartPhone
        };

        yield return new()
        {
            Id = "602d2149e773f2a3990b47f8",
            Sku = Sku.New(),
            Name = "Xiaomi Mi 9",
            Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
            ImageFile = "product-4.png",
            Price = 470.00M,
            Category = whiteApplicance
        };

        yield return new()
        {
            Id = "602d2149e773f2a3990b47f9",
            Sku = Sku.New(),
            Name = "HTC U11+ Plus",
            Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
            ImageFile = "product-5.png",
            Price = 380.00M,
            Category = smartPhone
        };

        yield return new()
        {
            Id = "602d2149e773f2a3990b47fa",
            Sku = Sku.New(),
            Name = "LG G7 ThinQ",
            Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
            ImageFile = "product-6.png",
            Price = 240.00M,
            Category = homeKitchen
        };
    }
}
