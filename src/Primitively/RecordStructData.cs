namespace Primitively;

internal record RecordStructData(DataType DataType, string Name, string NameSpace, ParentData? ParentData)
{
    public int Length { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public string? Pattern { get; set; }
    public string? Example { get; set; }
    public string? Format { get; set; }
    public Specifier? Specifier { get; set; }
}
