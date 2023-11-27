using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;
using Primitively.MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveBsonSerializerTests
{
    [Fact]
    public void BsonSerializers_Types_Are_Registered_Correctly_Using_IPrimitive_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();
        var expectedTypes = new Type[]
        {
            typeof(BirthDate), typeof(ByteId), typeof(IntId), typeof(LongId), typeof(SByteId), typeof(ShortId),
            typeof(UIntId), typeof(ULongId), typeof(UShortId), typeof(EightDigits), typeof(MinimumOf100)
        };

        // Act
        services.AddPrimitively()
            .AddBson(configure =>
            {
                // Add MongoDB support and register BSON serializers for the given source generated Primitive types
                configure.DefaultSerializers(cache =>
                {
                    // Override default serializers by replacing the item in the cache (Nb. Re-adding the default rather
                    // than creating and assigning a new type). This method does not need to be called unless a custom serializer is preferred 
                    cache.SetSerializer(DataType.DateOnly, typeof(BsonIDateOnlySerializer<>));
                    cache.SetSerializer(DataType.Guid, typeof(BsonIGuidSerializer<>));
                    cache.SetSerializer(DataType.Int, typeof(BsonIIntSerializer<>));
                    cache.SetSerializer(DataType.Long, typeof(BsonILongSerializer<>));
                    cache.SetSerializer(DataType.SByte, typeof(BsonISByteSerializer<>));
                    cache.SetSerializer(DataType.Short, typeof(BsonIShortSerializer<>));
                    cache.SetSerializer(DataType.String, typeof(BsonIStringSerializer<>));
                    cache.SetSerializer(DataType.UInt, typeof(BsonIUIntSerializer<>));
                    cache.SetSerializer(DataType.ULong, typeof(BsonIULongSerializer<>));
                    cache.SetSerializer(DataType.UShort, typeof(BsonIUShortSerializer<>));
                });

                configure.RegisterSerializers(register =>
                {
                    // Create an instance of a Bson Serializer for the given Primitively types using the configured
                    // Bson Serializers held in the BsonSerializerCache
                    register.AddSerializerForType<BirthDate>();
                    register.AddSerializerForType<ByteId>();
                    register.AddSerializerForType<IntId>();
                    register.AddSerializerForType<LongId>();
                    register.AddSerializerForType<SByteId>();
                    register.AddSerializerForType<ShortId>();
                    register.AddSerializerForType<UIntId>();
                    register.AddSerializerForType<ULongId>();
                    register.AddSerializerForType<UShortId>();
                    register.AddSerializerForType<EightDigits>();

                    // User serializer defined in the IBsonSerializer type parameter
                    register.AddSerializerForType<MinimumOf100, BsonIIntSerializer<MinimumOf100>>();

                    // Create an instance of a Bson Serializer for each Primitively type in the given Primitively repo
                    register.AddSerializerForEachTypeIn<PrimitiveRepository>();
                });
            });

        // Assert
        AssertThatSerializerIsRegistered(expectedTypes[0], typeof(BsonIDateOnlySerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[1], typeof(BsonIByteSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[2], typeof(BsonIIntSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[3], typeof(BsonILongSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[4], typeof(BsonISByteSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[5], typeof(BsonIShortSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[6], typeof(BsonIUIntSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[7], typeof(BsonIULongSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[8], typeof(BsonIUShortSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[9], typeof(BsonIStringSerializer<>));
        AssertThatSerializerIsRegistered(expectedTypes[10], typeof(BsonIIntSerializer<>));
    }

    private static void AssertThatSerializerIsRegistered(Type primitivelyType, Type serializerType)
    {
        // Bson Serializers
        var nullablePrimitivelyType = typeof(Nullable<>).MakeGenericType(primitivelyType);
        var primitivelySerializerType = serializerType.IsGenericTypeDefinition ? serializerType.MakeGenericType(primitivelyType) : serializerType;
        var nullablePrimitivelySerializerType = typeof(NullableSerializer<>).MakeGenericType(primitivelyType);

        // Assert non-nullable serializer created
        BsonSerializer.LookupSerializer(primitivelyType).Should().BeOfType(primitivelySerializerType);

        // Assert nullable serializer created
        BsonSerializer.LookupSerializer(nullablePrimitivelyType).Should().BeOfType(nullablePrimitivelySerializerType);
    }
}
