using RimWorld;
using System;
using Verse;

namespace PacketLoss.Audit.Rules;

internal abstract class OptionalRule : BaseRule
{
    private readonly Func<PacketLossSettings, bool> _isEnabled;

    protected OptionalRule(Func<PacketLossSettings, bool> isEnabled, string? debugName = null) : base(debugName)
    {
        _isEnabled = isEnabled;
    }

    public override bool CanHandle(Letter letter) =>
        _isEnabled.Invoke(PacketLossMod.Settings);
}
