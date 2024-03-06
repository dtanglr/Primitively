namespace Primitively;

/// <summary>
/// Represents a record for storing parent data information.
/// </summary>
/// <param name="Keyword">The keyword associated with the parent data.</param>
/// <param name="Name">The name of the parent data.</param>
/// <param name="Constraints">The constraints associated with the parent data.</param>
/// <param name="Child">The child of the parent data, if any.</param>
internal record ParentData(string Keyword, string Name, string Constraints, ParentData? Child);
