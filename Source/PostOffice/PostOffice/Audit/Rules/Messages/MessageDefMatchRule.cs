using Verse;

namespace PostOffice.Audit.Rules.Messages;

internal class MessageDefMatchRule : DefMatchRule<Message, MessageTypeDef>
{
    public MessageDefMatchRule(Func<MessageTypeDef> defSupplier, ChainAction matchAction, Func<PostOfficeSettings, bool> isEnabled, string? debugName = null) 
        : base(defSupplier, matchAction, isEnabled, debugName)
    {
    }

    protected override MessageTypeDef? GetDefOf(Message target) => target.def;
}
