using System.ComponentModel;

namespace G4S.Entities.Enums
{
    public enum DeleteOption
    {
        [Description("Not deleted")]
        NotDeleted,
        [Description("Deleted")]
        OnlyDeleted,
        [Description("Both")]
        Both
    }
}
