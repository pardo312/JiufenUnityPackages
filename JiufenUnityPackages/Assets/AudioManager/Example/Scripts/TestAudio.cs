using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio.Test
{
    public class TestAudio : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AudioJobOptions audioJobExtras = new AudioJobOptions(new AudioFadeInfo(true, 1));
                AudioManager.Instance.PlayAudio(AudioType.OST_Main_Theme, audioJobExtras);
                Debug.Log("Pressed q");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.Instance.PlayAudio(AudioType.SFX_1);
                Debug.Log("Pressed e");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                AudioManager.Instance.PlayAudio(AudioType.SFX_2);
                Debug.Log("Pressed r");
            }

            //STOPS
            if (Input.GetKeyDown(KeyCode.A))
            {
                AudioJobOptions audioJobExtras = new AudioJobOptions(null, new AudioFadeInfo(true, 1));
                AudioManager.Instance.StopAudio(AudioType.OST_Main_Theme, audioJobExtras);
                Debug.Log("Pressed a");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                AudioManager.Instance.StopAudio(AudioType.SFX_1);
                Debug.Log("Pressed d");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioManager.Instance.StopAudio(AudioType.SFX_2);
                Debug.Log("Pressed f");
            }
        }
#endif
    }
}
