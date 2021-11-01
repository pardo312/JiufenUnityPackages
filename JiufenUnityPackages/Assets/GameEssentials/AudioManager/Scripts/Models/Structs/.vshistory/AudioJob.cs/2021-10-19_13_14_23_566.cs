﻿using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioType type;
        public AudioAction action;

        public AudioJob(AudioType audioType, AudioAction audioAction)
        {
            this.type = audioType;
            this.action = audioAction;
        }
    }
    public class AudioJobExtras
    {
        public AudioFadeInfo fadeIn;
        public AudioFadeInfo fadeOut;
        public WaitForSeconds delay;

        public AudioJobExtras(AudioFadeInfo fadeIn, AudioFadeInfo fadeOut, float delay)
        {

            this.fadeIn = fadeIn;
            this.fadeOut = fadeOut;
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
        }
    }

    public class AudioFadeInfo
    {
        public static AudioFadeInfo defaultfadeInfo = new AudioFadeInfo(false, 0);

        public bool fade;
        public float fadeDuration;

        public AudioFadeInfo(bool fade, float fadeDuration)
        {
            this.fade = fade;
            this.fadeDuration = fadeDuration;
        }
    }
}