namespace Primitively;

internal class PrimitiveRecordStruct
{
    public PrimitiveRecordStruct(string name, string nameSpace, ParentClass? parent)
    {
        Name = name;
        NameSpace = nameSpace;
        Parent = parent;
    }

    public string Name { get; }
    public string NameSpace { get; }
    public ParentClass? Parent { get; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public string? Pattern { get; set; }
    public string? Example { get; set; }
    public string? Format { get; set; }
    public PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Default;
}

internal enum PrimitiveType
{
    Date,
    Guid,
    String,
    Default = Guid
}
