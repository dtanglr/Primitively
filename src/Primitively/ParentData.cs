namespace Primitively;

/// <summary>
/// Represents a record for storing parent data information.
/// </summary>
internal record ParentData(
    /// <summary>
    /// Gets the keyword associated with the parent data.
    /// </summary>
    string Keyword,

    /// <summary>
    /// Gets the name of the parent data.
    /// </summary>
    string Name,

    /// <summary>
    /// Gets the constraints associated with the parent data.
    /// </summary>
    string Constraints,

    /// <summary>
    /// Gets the child of the parent data.
    /// </summary>
    ParentData? Child);
