using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lostbyte.Toolkit.CustomEditor;
using Lostbyte.Toolkit.FactSystem;

namespace Lostbyte.Toolkit.Audio
{
    public class FactAudio : MonoBehaviour
    {
        [SerializeField, Autowired(Autowired.Type.Parent, isForced: true), Hide] private KeyReference m_key;
        [field: SerializeField] public AudioSource Source { get; private set; }
        [field: SerializeField] public FactAudioSettings Settings { get; private set; }
        private int _priority;
        private List<AudioSettingsRunner> _runners;
        private List<AudioSettingsRunner> Runners => _runners ??= m_key ? Settings.Settings.Select(s => s.Create(m_key.Key, this)).ToList() : null;
        public void Play(AudioSettings settings)
        {
            if (Source.isPlaying && (settings.Priority < _priority || !settings.StopActive)) return;
            _priority = settings.Priority;
            Source.pitch = Mathf.Clamp01(Random.Range((settings.Pitch - settings.PitchRandomization) / 2f, (settings.Pitch + settings.PitchRandomization) / 2f));
            Source.volume = Mathf.Clamp01(Random.Range((settings.Volume - settings.VolumeRandomization) / 2f, (settings.Volume + settings.VolumeRandomization) / 2f));
            Source.clip = settings.Clips[Random.Range(0, settings.Clips.Length)];
            Source.Play();
        }
        private void OnEnable() => Runners?.ForEach(i => i.Enable());
        private void OnDisable() => Runners?.ForEach(i => i.Disable());
    }
}
