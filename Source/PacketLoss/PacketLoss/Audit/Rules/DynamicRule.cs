using System;
using Verse;

namespace PacketLoss.Audit.Rules;

internal class DynamicRule : OptionalRule
{
    // true => Forward
    private readonly Func<Letter, bool> _predicate;

    internal DynamicRule(Func<Letter, bool> predicate, Func<PacketLossSettings, bool> isEnabled, string? debugName = null) : base(isEnabled, debugName) => 
        _predicate = predicate;

    public override MessageAction Audit(Letter letter) => 
        _predicate(letter) ? MessageAction.Forward : MessageAction.Drop;
}
