namespace PostOffice.Patching;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
internal class RequiresModAttribute(string modId) : Attribute
{
    public string ModId { get; } = modId;
}
