using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJobRestart : AudioJob
    {
        public AudioJobRestart(AudioType audioType, AudioJobOptions audioJobExtras = null) : base(audioType, audioJobExtras)
        {
            action = AudioAction.RESTART;
        }

        public override IEnumerator RunAudioJob(AudioTrack track, AudioClip clip)
        {
            yield return base.RunAudioJob(track, clip);

            track._audioSource.Stop();
            track._audioSource.Play();
        }
    }

}