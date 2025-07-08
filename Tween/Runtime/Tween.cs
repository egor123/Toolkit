using System.Collections;
using UnityEngine;
using System;

namespace Lostbyte.Toolkit.Tween
{
    public abstract class Tween
    {
        protected Tween(MonoBehaviour initiator) { Initiator = initiator; }
        protected internal AnimationCurve AnimationCurve { private get; set; }
        protected internal float? Duration { private get; set; }
        protected internal Action Callback { private get; set; }
        private MonoBehaviour Initiator { get; set; }
        private Coroutine _runningCorutine;
        public bool IsRunning { get; private set; }
        public void Start()
        {
            Stop();
            if(Initiator == null && !Initiator.enabled) return;
            IsRunning = true;
            _runningCorutine = Initiator.StartCoroutine(Enumerator());
        }
        public void Stop()
        {
            IsRunning = false;
            if (_runningCorutine != null) Initiator.StopCoroutine(_runningCorutine);
        }
        public void Finish()
        {
            Stop();
            DoTween(AnimationType.Linear.ToCurve().Evaluate(1));
            Callback?.Invoke();
        }
        private IEnumerator Enumerator()
        {
            Init();
            if (Duration > 0)
            {
                float progress = 0f;
                while (progress < 1f)
                {
                    progress += Time.deltaTime / (Duration ?? 1);
                    var curve = AnimationCurve ?? AnimationType.Linear.ToCurve();
                    DoTween(curve.Evaluate(progress));
                    yield return null;
                }
            }
            Finish();
        }
        protected abstract void Init();
        protected abstract void DoTween(float delta);
    }
}
