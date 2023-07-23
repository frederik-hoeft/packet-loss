namespace PostOffice.Audit.Rules;

internal abstract class RuleProvider
{
    protected static bool True(PostOfficeSettings _) => true;

    protected static bool False(PostOfficeSettings _) => false;
}
