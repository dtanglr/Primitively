namespace Primitively;

internal record PrimitiveRecordStruct(PrimitiveType PrimitiveType, string Name, string NameSpace, ParentClass? Parent)
{
    public int Length { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public string? Pattern { get; set; }
    public string? Example { get; set; }
    public string? Format { get; set; }
}
