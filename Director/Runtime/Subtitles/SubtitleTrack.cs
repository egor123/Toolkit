using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Lostbyte.Toolkit.Director
{
    [TrackBindingType(typeof(SubtitlesManager))]
    [TrackClipType(typeof(SubtitleClip))]
    public class SubtitleTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<SubtitleTrackMixer>.Create(graph, inputCount);
        }
    }
}
