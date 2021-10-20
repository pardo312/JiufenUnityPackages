using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{

    public class AudioJobStart : AudioJob
    {
        public AudioJobStart(AudioType audioType, AudioJobOptions audioJobExtras = null) : base(audioType, audioJobExtras)
        {
            action = AudioAction.START;
        }

        public override IEnumerator RunAudioJob(AudioTrack track, AudioClip clip)
        {
            yield return base.RunAudioJob(track, clip);

            track._audioSource.volume = 0;
            track._audioSource.Play();
            yield return FadeAudio(track, options.fadeIn.fadeDuration, 0, 1);
        }
    }

}