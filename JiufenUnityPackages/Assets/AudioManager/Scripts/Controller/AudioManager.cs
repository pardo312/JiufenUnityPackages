using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioManager : MonoBehaviour
    {

        #region 1.Fields
        public Hashtable m_audioTable;
        [SerializeField] private AudioTrack[] m_audioTracks;
        [SerializeField] private bool debug;

        public static AudioManager Instance;
        private AudioJobsController m_audioJobsController;
        #endregion 1.Fields

        #region 2.Methods
        #region 2.1.UnityEvents
        public void Awake()
        {
            if (Instance != null) return;
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
        public void PlayAudio(string  key, AudioJobOptions options = null)
        {
            m_audioJobsController.AddJob(new AudioJobStart(key, options));
        }
        public void StopAudio(string key, AudioJobOptions options = null)
        {
            m_audioJobsController.AddJob(new AudioJobStop(key, options));
        }
        public void RestartAudio(string key, AudioJobOptions options = null)
        {
            m_audioJobsController.AddJob(new AudioJobRestart(key, options));
        }
        #endregion 2.2.Audio Behaviours

        #region 2.3.Helpers
        private void Configure()
        {
            Instance = this;
            AudioLogger.m_debug = debug;

            m_audioTable = new Hashtable();
            GenerateAudioTable();

            m_audioJobsController = gameObject.AddComponent<AudioJobsController>();
            m_audioJobsController.Init();
        }

        private void GenerateAudioTable()
        {
            foreach (AudioTrack tracks in m_audioTracks)
            {
                foreach (AudioObject audioObj in tracks.audioObjects)
                {
                    if (m_audioTable.Contains(audioObj.key))
                    {
                        AudioLogger.LogError($"AudioTable is registering an already registered audio type: {audioObj.key}");
                    }
                    else
                    {
                        m_audioTable.Add(audioObj.key, tracks);
                        AudioLogger.Log($"Audio type {audioObj.key} has been registered in the audioTable");
                    }
                }
            }

        }

        private void Dispose()
        {
            m_audioJobsController.Dispose();
        }
        #endregion 2.3.Helpers
        #endregion 2.Methods
    }
}

