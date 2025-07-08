using UnityEngine;
using System;

namespace Lostbyte.Toolkit.Tween
{
    public static class TweenExtensions
    {
        public static T SetAnimation<T>(this T t, AnimationCurve curve) where T : Tween => Base(t, () => t.AnimationCurve = curve);
        public static T SetAnimation<T>(this T t, AnimationType type) where T : Tween => Base(t, () => t.AnimationCurve = type.ToCurve());
        public static T SetDuration<T>(this T t, float duration) where T : Tween => Base(t, () => t.Duration = duration);
        public static T SetCallback<T>(this T t, Action callback) where T : Tween => Base(t, () => t.Callback = callback);
        //-----------------------------------------------------------------------------------------
        public static TransformTween Tween(this MonoBehaviour initiator, Transform transform) => new(initiator, transform);
        public static TransformTween SetTargetPosition(this TransformTween t, Vector3 position) => Base(t, () => t.TargetPosition = position);
        public static TransformTween SetTargetRotation(this TransformTween t, Quaternion rotation) => Base(t, () => t.TargetRotation = rotation);
        public static TransformTween SetTargetScale(this TransformTween t, Vector3 scale) => Base(t, () => t.TargetScale = scale);
        //--------------------------------------------------------------------------------------------
        public static StringTween Tween(this MonoBehaviour initiator, Action<string> onChange) => new(initiator, onChange);
        public static StringTween SetTargetString(this StringTween t, string text) => Base(t, () => t.TargetText = text);
        //--------------------------------------------------------------------------------------------
        public static FloatTween Tween(this MonoBehaviour initiator, Action<float> onChange) => new(initiator, onChange);
        public static FloatTween SetStartFloat(this FloatTween t, float value) => Base(t, () => t.StartFloat = value);
        public static FloatTween SetTargetFloat(this FloatTween t, float value) => Base(t, () => t.TargetFloat = value);
        //--------------------------------------------------------------------------------------------
        public static Vector3Tween Tween(this MonoBehaviour initiator, Action<Vector3> onChange) => new(initiator, onChange);
        public static Vector3Tween SetStartVector(this Vector3Tween t, Vector3 value) => Base(t, () => t.StartVector = value);
        public static Vector3Tween SetTargetVector(this Vector3Tween t, Vector3 value) => Base(t, () => t.TargetVector = value);
        //--------------------------------------------------------------------------------------------

        public static T Base<T>(T tween, Action act) where T : Tween
        {
            act.Invoke();
            return tween;
        }
    }
}