namespace Primitively;

internal class PrimitiveRecordStruct
{
    public PrimitiveRecordStruct(PrimitiveType primitiveType, string name, string nameSpace, ParentClass? parent)
    {
        PrimitiveType = primitiveType;
        Name = name;
        NameSpace = nameSpace;
        Parent = parent;
    }

    public PrimitiveType PrimitiveType { get; }
    public string Name { get; }
    public string NameSpace { get; }
    public ParentClass? Parent { get; }
    public int Length { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public string? Pattern { get; set; }
    public string? Example { get; set; }
    public string? Format { get; set; }
}
