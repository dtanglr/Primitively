using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;
using Primitively.MongoDb;
using Primitively.MongoDb.Bson.Serialization.Serializers;
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
        services.AddPrimitively(configure =>
        {
            // Add MongoDB support and register BSON serializers for the given source generated Primitive types
            configure.UseMongoDB(builder =>
            {
                // Override default serializers by replacing the item in the cache (Nb. Re-adding the default rather than creating and assigning a new type)
                // Nb. This method does not need to be called unless a custom serializer is preferred 
                builder.UseBsonSerializer()
                    .ForDataType(DataType.DateOnly, typeof(DateOnlyBsonSerializer<>))
                    .ForDataType(DataType.Byte, typeof(ByteBsonSerializer<>))
                    .ForDataType(DataType.Guid, typeof(GuidBsonSerializer<>))
                    .ForDataType(DataType.Int, typeof(IntBsonSerializer<>))
                    .ForDataType(DataType.Long, typeof(LongBsonSerializer<>))
                    .ForDataType(DataType.SByte, typeof(SByteBsonSerializer<>))
                    .ForDataType(DataType.Short, typeof(ShortBsonSerializer<>))
                    .ForDataType(DataType.String, typeof(StringBsonSerializer<>))
                    .ForDataType(DataType.UInt, typeof(UIntBsonSerializer<>))
                    .ForDataType(DataType.ULong, typeof(ULongBsonSerializer<>))
                    .ForDataType(DataType.UShort, typeof(UShortBsonSerializer<>));

                // Use serializer defined in the BsonSerializerCache class
                builder.RegisterBsonSerializer()
                    .ForType<BirthDate>()
                    .ForType<ByteId>()
                    .ForType<IntId>()
                    .ForType<LongId>()
                    .ForType<SByteId>()
                    .ForType<ShortId>()
                    .ForType<UIntId>()
                    .ForType<ULongId>()
                    .ForType<UShortId>()
                    .ForType<EightDigits>();

                // User serializer defined in the IBsonSerializer type parameter
                builder.RegisterBsonSerializer()
                    .ForType<MinimumOf100, IntBsonSerializer<MinimumOf100>>();

                // Use serializer defined in the BsonSerializerOptions class
                builder.RegisterBsonSerializer()
                    .ForEachTypeIn<PrimitiveRepository>();
            });
        });

        // Assert
        AssertThatBsonSerializerIsRegistered(expectedTypes[0], typeof(DateOnlyBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[1], typeof(ByteBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[2], typeof(IntBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[3], typeof(LongBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[4], typeof(SByteBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[5], typeof(ShortBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[6], typeof(UIntBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[7], typeof(ULongBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[8], typeof(UShortBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[9], typeof(StringBsonSerializer<>));
        AssertThatBsonSerializerIsRegistered(expectedTypes[10], typeof(IntBsonSerializer<>));
    }

    private static void AssertThatBsonSerializerIsRegistered(Type primitivelyType, Type serializerType)
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
