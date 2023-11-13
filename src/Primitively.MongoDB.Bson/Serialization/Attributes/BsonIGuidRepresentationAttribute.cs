using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Attributes;

/// <summary>
/// Specifies the Guid representation to use with the GuidSerializer for this member.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class BsonIGuidRepresentationAttribute : Attribute, IBsonMemberMapAttribute
{
    /// <summary>
    /// Initializes a new instance of the BsonGuidRepresentationAttribute class.
    /// </summary>
    /// <param name="guidRepresentation">The Guid representation.</param>
    public BsonIGuidRepresentationAttribute(GuidRepresentation guidRepresentation)
    {
        GuidRepresentation = guidRepresentation;
    }

    /// <summary>
    /// Gets the Guid representation.
    /// </summary>
    public GuidRepresentation GuidRepresentation { get; }

    /// <inheritdoc/>
    public void Apply(BsonMemberMap memberMap)
    {
        var guidSerializer = memberMap.GetSerializer() as IGuidRepresentationConfigurable;

        if (guidSerializer == null)
        {
            throw new InvalidOperationException("[BsonIGuidRepresentationAttribute] can only be used when the serializer implements IGuidRepresentationConfigurable.");
        }

        var reconfiguredGuidSerializer = guidSerializer.WithGuidRepresentation(GuidRepresentation);
        memberMap.SetSerializer(reconfiguredGuidSerializer);
    }
}
