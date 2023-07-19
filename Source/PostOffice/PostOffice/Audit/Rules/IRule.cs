using Verse;

namespace PostOffice.Audit.Rules;

public interface IRule
{
    bool CanHandle(Letter letter);

    MessageAction Audit(Letter letter);
}
