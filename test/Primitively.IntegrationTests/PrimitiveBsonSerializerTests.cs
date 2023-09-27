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
                // Override default serializers (Nb. Re-adding the default rather than create a new type
                builder.SetBsonSerializerForDataType(DataType.DateOnly, typeof(DateOnlyBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.Byte, typeof(ByteBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.Guid, typeof(GuidBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.Int, typeof(IntBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.Long, typeof(LongBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.SByte, typeof(SByteBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.Short, typeof(ShortBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.String, typeof(StringBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.UInt, typeof(UIntBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.ULong, typeof(ULongBsonSerializer<>));
                builder.SetBsonSerializerForDataType(DataType.UShort, typeof(UShortBsonSerializer<>));

                // Use serializer defined in the BsonSerializerOptions class
                builder.RegisterBsonSerializerForType<BirthDate>();
                builder.RegisterBsonSerializerForType<ByteId>();
                builder.RegisterBsonSerializerForType<IntId>();
                builder.RegisterBsonSerializerForType<LongId>();
                builder.RegisterBsonSerializerForType<SByteId>();
                builder.RegisterBsonSerializerForType<ShortId>();
                builder.RegisterBsonSerializerForType<UIntId>();
                builder.RegisterBsonSerializerForType<ULongId>();
                builder.RegisterBsonSerializerForType<UShortId>();
                builder.RegisterBsonSerializerForType<EightDigits>();

                // User serializer defined in the IBsonSerializer type parameter
                builder.RegisterBsonSerializerForType<MinimumOf100, IntBsonSerializer<MinimumOf100>>();

                // Use serializer defined in the BsonSerializerOptions class
                builder.RegisterBsonSerializerForEachTypeIn<PrimitiveRepository>();
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
