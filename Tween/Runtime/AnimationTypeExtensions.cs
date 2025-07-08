using UnityEngine;
using System;

namespace Lostbyte.Toolkit.Tween
{
    public static class AnimationTypeExtentions
    {
        public static AnimationCurve ToCurve(this AnimationType type)
        {
            return type switch
            {
                AnimationType.Linear => AnimationCurve.Linear(0, 0, 1, 1),
                AnimationType.EaseInOut => AnimationCurve.EaseInOut(0, 0, 1, 1),
                _ => throw new NotImplementedException(),
            };
        }
    }
}