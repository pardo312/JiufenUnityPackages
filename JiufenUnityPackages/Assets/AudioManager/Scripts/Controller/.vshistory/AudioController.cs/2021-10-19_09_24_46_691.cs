using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioController : MonoBehaviour
    {

        #region 1.Fields
        public bool _debug;
        public AudioTrack[] _audioTracks;
        public Hashtable _audioTable;
        public Hashtable _jobsTable;

        public AudioController Instace;
        #endregion 1.Fields

        #region 2.Methods

        #region 2.1.UnityEvents
        public void Awake()
        {
            if (Instace != null) return;
            else
            {
                Configure();
            }
        }
        public void OnDisable()
        {
            Dispose();
        }

        #endregion 2.1.UnityEvents


        #region 2.2.Audio Behaviours
        public void PlayAudio(AudioType audioType)
        {

        }
        public void StopAudio(AudioType audioType)
        {

        }
        public void RestartAudio(AudioType audioType)
        {

        }
        #endregion 2.2.Audio Behaviours

        #region 2.3.Helpers
        private void Configure()
        {
            Instace = this;
            _audioTable = new Hashtable();
            _jobsTable = new Hashtable();
            ConfigureAudioTable();
        }

        private void ConfigureAudioTable()
        {
            foreach (AudioTrack tracks in _audioTracks)
            {
                foreach (AudioObject audioObj in tracks._audioObject)
                {
                    if (_audioTable.Contains(audioObj._audioType))
                    {
                        LogError($"AudioTable already has this audio type: {audioObj._audioType.ToString()}");
                    }
                    else
                    {

                    }


                }
            }

        }

        private void Dispose()
        {

        }

        private void Log(string message)
        {
            if (!_debug) return;
            Debug.Log($"<color=yellow>Audio Controller message:</color> {message}");
        }
        private void LogError(string message)
        {
            if (!_debug) return;
            Debug.Log($"<color=red>Audio Controller error message:</color> {message}");
        }
        #endregion 2.3.Helpers

        #endregion 2.Methods
    }
}
