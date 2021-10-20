using System;
using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public abstract class AudioJob
    {
        public AudioType type;
        public AudioAction action;
        public AudioJobOptions options;

        public AudioJob(AudioType audioType, AudioJobOptions audioJobExtras = null)
        {
            this.type = audioType;
            this.options = audioJobExtras != null ? audioJobExtras : new AudioJobOptions();
        }

        public virtual IEnumerator RunAudioJob(AudioTrack track, AudioClip clip)
        {
            if (options.delay != null) yield return options.delay;
            track._audioSource.clip = clip;
        }

        private protected virtual IEnumerator FadeAudio(AudioTrack track, float durationFade, float initialVolume, float targetVolume, Action onFinishFadeCallback = null)
        {
            float timerFade = 0.0f;

            while (timerFade <= durationFade)
            {
                track._audioSource.volume = Mathf.Lerp(initialVolume, targetVolume, timerFade / durationFade);
                timerFade += Time.deltaTime;
                yield return null;
            }

            track._audioSource.volume = targetVolume;
            onFinishFadeCallback?.Invoke();
        }
    }

}