using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJobOptions
    {
        public AudioFadeInfo fadeIn;
        public AudioFadeInfo fadeOut;
        public WaitForSeconds delay;

        public AudioJobOptions(AudioFadeInfo fadeIn = null, AudioFadeInfo fadeOut = null, float delay = 0f)
        {
            this.fadeIn = fadeIn != null ? fadeIn : new AudioFadeInfo(false, 0);
            this.fadeOut = fadeOut != null ? fadeOut : new AudioFadeInfo(false, 0);
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
        }
    }
}