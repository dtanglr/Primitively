using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;
using Primitively.MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization.Options;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveBsonSerializerTests
{
    [Fact(Skip = "Still in developement")]
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
            .AddBson(options =>
            {
                options.BsonIByteSerializer(option => option.RepresentAs(BsonType.Int32));
                options.BsonIGuidSerializer(option => option
                    .RepresentAs(GuidRepresentation.Standard)
                    .SerializeWith(typeof(BsonIGuidSerializer<>))
                    .CreateInstanceWith((options, primitiveType) =>
                    {
                        // Check that the application is using the correct GuidRepresentationMode
                        if (options.GuidRepresentation != BsonDefaults.GuidRepresentation && BsonDefaults.GuidRepresentationMode == GuidRepresentationMode.V2)
                        {
                            // V3 mode permits a mixture of searchable GUID representations to exist side by side
                            // TODO: Consider throwing an exception instead rather than forcing the mode to V3
                            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
                        }

                        // Construct a Bson serializer for the given Primitively type using the options
                        var serializerType = BsonOptions.GetSerializerType(primitiveType, options.SerializerType);

                        // Create an instance of the serializer
                        object argument = options.Representation == BsonType.String ? BsonType.String : options.GuidRepresentation;
                        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, argument)!;

                        return serializerInstance;
                    }));
                options.BsonIIntSerializer(options => options.AllowOverflow = true);

                //configure.RegisterSerializers(register =>
                //{
                //    // Create an instance of a Bson Serializer for the given Primitively types using the configured
                //    // Bson Serializers held in the BsonSerializerCache
                //    register.AddSerializerForType<BirthDate>();
                //    register.AddSerializerForType<ByteId>();
                //    register.AddSerializerForType<IntId>();
                //    register.AddSerializerForType<LongId>();
                //    register.AddSerializerForType<SByteId>();
                //    register.AddSerializerForType<ShortId>();
                //    register.AddSerializerForType<UIntId>();
                //    register.AddSerializerForType<ULongId>();
                //    register.AddSerializerForType<UShortId>();
                //    register.AddSerializerForType<EightDigits>();

                //    // User serializer defined in the IBsonSerializer type parameter
                //    register.AddSerializerForType<MinimumOf100, BsonIIntSerializer<MinimumOf100>>();

                //    // Create an instance of a Bson Serializer for each Primitively type in the given Primitively repo
                //    register.AddSerializerForEachTypeIn<PrimitiveRepository>();
                //});
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
