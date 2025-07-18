using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Video;

namespace Lostbyte.Toolkit.Director
{
    public class SubtitlesManager : MonoBehaviour
    {
        public static SubtitlesManager Instance { get; private set; }
        [SerializeField] private TMP_Text m_tmp;
        [SerializeField] private float m_charTypingDuration = 0.1f;
        [SerializeField, Range(0f, 1f)] private float m_maxTypingDuration = 0.8f;
        [SerializeField] private AudioSource m_source;
        [SerializeField] private List<AudioClip> m_clips = new();
        [SerializeField, Range(0, 1f)] private float m_pitchVariation = 0.1f;
        public UnityEvent OnTypeEvent;
        private void Awake()
        {
            Instance = this;
            Clear();
        }
        private LocalizedString _localizedString;
        private string _text = null;
        private float _progress;
        private float _d;
        private float _duration;
        public void Set(LocalizedString text, float duration)
        {
            _progress = 0;
            _localizedString = text;
            _d = _duration = duration;
            _localizedString.StringChanged += UpdateText;
        }
        public void SetFrame(string text, float time, float duration)
        {
            Clear();
            var d = Mathf.Min(duration * m_maxTypingDuration, text.Length * m_charTypingDuration);
            if (time < d)
            {
                OnTypeEvent?.Invoke();
                m_tmp.text = text[..Mathf.CeilToInt(text.Length * Mathf.Clamp01(time / d))];
            }
            else
            {
                m_tmp.text = text;
            }

        }
        private void UpdateText(string value)
        {
            _text = value;
            _d = Mathf.Min(_duration * m_maxTypingDuration, _text.Length * m_charTypingDuration);
        }
        public void Clear()
        {
            if (_localizedString != null)
                _localizedString.StringChanged -= UpdateText;
            _text = null;
            m_tmp.text = "";
        }
        private void Update()
        {
            if (_text != null)
            {
                _progress += Time.deltaTime;
                m_tmp.text = _text[..Mathf.CeilToInt(_text.Length * Mathf.Clamp01(_progress / _d))];
                if (_progress < _d)
                {
                    OnTypeEvent?.Invoke();
                    if (m_source && m_clips.Count > 0 && !m_source.isPlaying)
                    {
                        m_source.clip = m_clips[Random.Range(0, m_clips.Count)];
                        m_source.pitch = Random.Range(1f - m_pitchVariation, 1f + m_pitchVariation);
                        m_source.Play();
                    }
                }

            }
        }
    }
}
