namespace Primitively;

internal record RecordStructData(DataType DataType, string Name, string NameSpace, ParentData? ParentData)
{
    public int Length { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public string? Pattern { get; set; }
    public string? Example { get; set; }
    public string? Format { get; set; }
    public string? Interface { get; set; }
    public string? Type { get; set; }
    public string? JsonReaderMethod { get; set; }
    public Specifier? Specifier { get; set; }
    public bool ImplementIValidatableObject { get; set; }
    public decimal? Minimum { get; set; }
    public decimal? Maximum { get; set; }
}
