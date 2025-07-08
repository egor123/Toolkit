using System;
using UnityEngine;

namespace Lostbyte.Toolkit.Tween
{
    public class StringTween : Tween
    {
        internal string TargetText { private get; set; }
        internal Action<string> OnChange;
        internal StringTween(MonoBehaviour initiator, Action<string> onChange) : base(initiator) { OnChange = onChange; }
        protected override void Init() => OnChange.Invoke("");
        protected override void DoTween(float delta) => OnChange.Invoke(TargetText.Substring(0, Mathf.RoundToInt(delta * TargetText.Length)));
    }
}
