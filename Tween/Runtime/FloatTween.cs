using System;
using UnityEngine;

namespace Lostbyte.Toolkit.Tween
{
    public class FloatTween : Tween
    {
        internal float StartFloat { private get; set; } = 0f;
        internal float TargetFloat { private get; set; } = 1f;
        internal Action<float> OnChange;
        internal FloatTween(MonoBehaviour initiator, Action<float> onChange) : base(initiator) { OnChange = onChange; }
        protected override void Init() => OnChange.Invoke(StartFloat);
        protected override void DoTween(float delta) => OnChange.Invoke(StartFloat + delta *(TargetFloat - StartFloat));
    }
}
