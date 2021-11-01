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
        public Hashtable _JobsTable;
        #endregion 1.Fields

        #region 2.Methods

        #region 2.1.UnityEvents
        public void Awake()
        {
            
        }
        #endregion 2.1.UnityEvents

        #region 2.3.Helpers
        private void Log(string message)
        {
            if (!_debug) return;
            Debug.Log($"<color=yellow>Audio Controller message:</color> {message}");
        }
        #endregion 2.3.Helpers

        #endregion 2.Methods
    }
}
