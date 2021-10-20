using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJobsController : MonoBehaviour
    {

        #region 1.Fields
        public Hashtable m_jobsTable;
        #endregion 1.Fields

        #region 2.Methods
        #region 2.0.Init
        public void Init()
        {
            m_jobsTable = new Hashtable();
        }
        #endregion 2.0.Init

        #region 2.1.Add & Execute Jobs
        public void AddJob(AudioJob _audioJob)
        {
            RemoveConflictingJobs(_audioJob.type);

            //Add Job
            Coroutine jobRunner = StartCoroutine(RunAudioJob(_audioJob));
            m_jobsTable.Add(_audioJob.type, jobRunner);
            AudioLogger.Log($"Starting Job {_audioJob.type} with action: {_audioJob.action}");
            AudioLogger.Log($"Job count: {m_jobsTable.Count}");
        }

        private IEnumerator RunAudioJob(AudioJob _audioJob)
        {
            AudioTrack track = (AudioTrack)AudioController.Instance.m_audioTable[_audioJob.type];
            AudioClip clip = GetAudioClipFromAudioTrack(_audioJob.type, track);

            yield return _audioJob.RunAudioJob(track,clip);

            //To Ensuser that the job was added first
            yield return new WaitForFixedUpdate();
            m_jobsTable.Remove(_audioJob.type);
        }

        private AudioClip GetAudioClipFromAudioTrack(AudioType type, AudioTrack track)
        {
            foreach (AudioObject obj in track._audioObject)
            {
                if (obj._audioType == type)
                {
                    return obj._audioClip;
                }
            }
            return null;
        }
        #endregion 2.1.Add & Execute Jobs

        #region 2.2.Remove and Dispose Jobs
        private void RemoveConflictingJobs(AudioType _type)
        {
            if (m_jobsTable.Contains(_type))
            {
                RemoveJob(_type);
            }

            AudioType audioTypeConflict = AudioType.None;

            foreach (DictionaryEntry job in m_jobsTable)
            {
                AudioTrack currentTrack = (AudioTrack)AudioController.Instance.m_audioTable[job.Key];
                AudioTrack newTrack = (AudioTrack)AudioController.Instance.m_audioTable[_type];
                if (currentTrack._audioSource == newTrack._audioSource)
                {
                    AudioLogger.LogError($"You have the same audio source for different audioTypes. Please check audioType [{job.Key}] and [{_type}] ");
                    audioTypeConflict = _type;
                }
            }
            if (audioTypeConflict != AudioType.None)
            {
                RemoveJob(audioTypeConflict);
            }
        }

        private void RemoveJob(AudioType _type)
        {
            if (!m_jobsTable.ContainsKey(_type))
            {
                AudioLogger.LogError($"You are trying to stop a job that doesn't exist in the jobsTable: {_type}");
                return;
            }
            Coroutine runningJob = (Coroutine)m_jobsTable[_type];
            StopCoroutine(runningJob);
            m_jobsTable.Remove(_type);
        }
        public void Dispose()
        {
            foreach (DictionaryEntry entry in m_jobsTable)
            {
                Coroutine job = (Coroutine)entry.Value;
                StopCoroutine(job);
            }
        }

        #endregion 2.2.Remove and Dispose Jobs
        #endregion 2.Methods
    }
}
