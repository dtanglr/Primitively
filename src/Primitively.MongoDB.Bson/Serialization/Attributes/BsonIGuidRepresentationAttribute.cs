﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Attributes;

/// <summary>
/// Specifies the Guid representation to use with the GuidSerializer for this member.
/// </summary>
/// <remarks>
/// Initializes a new instance of the BsonGuidRepresentationAttribute class.
/// </remarks>
/// <param name="guidRepresentation">The Guid representation.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class BsonIGuidRepresentationAttribute(GuidRepresentation guidRepresentation) : Attribute, IBsonMemberMapAttribute
{
    /// <summary>
    /// Gets the Guid representation.
    /// </summary>
    public GuidRepresentation GuidRepresentation { get; } = guidRepresentation;

    /// <inheritdoc/>
    public void Apply(BsonMemberMap memberMap)
    {
        if (memberMap.GetSerializer() is not IGuidRepresentationConfigurable guidSerializer)
        {
            throw new InvalidOperationException("[BsonIGuidRepresentationAttribute] can only be used when the serializer implements IGuidRepresentationConfigurable.");
        }

        // Check that the application is using the correct GuidRepresentationMode
        if (BsonDefaults.GuidRepresentationMode == GuidRepresentationMode.V2 && GuidRepresentation != BsonDefaults.GuidRepresentation)
        {
            // V3 mode permits a mixture of searchable GUID representations to exist side by side
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
        }

        var reconfiguredGuidSerializer = guidSerializer.WithGuidRepresentation(GuidRepresentation);
        memberMap.SetSerializer(reconfiguredGuidSerializer);
    }
}
