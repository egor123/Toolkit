using UnityEngine;

namespace Lostbyte.Toolkit.Tween
{
    public class TransformTween : Tween
    {
        internal TransformTween(MonoBehaviour initiator, Transform transform) : base(initiator) { _transform = transform; }
        internal Vector3? TargetPosition { private get; set; }
        internal Quaternion? TargetRotation { private get; set; }
        internal Vector3? TargetScale { private get; set; }
        private Transform _transform;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Vector3 _startScale;
        protected override void Init()
        {
            _startPosition = _transform.position;
            _startRotation = _transform.rotation;
            _startScale = _transform.localScale;
        }
        protected override void DoTween(float delta)
        {
            if (TargetPosition != null) _transform.position = Vector3.LerpUnclamped(_startPosition, TargetPosition.Value, delta);
            if (TargetRotation != null) _transform.rotation = Quaternion.LerpUnclamped(_startRotation, TargetRotation.Value, delta);
            if (TargetScale != null) _transform.localScale = Vector3.LerpUnclamped(_startScale, TargetScale.Value, delta);
        }
    }
}
