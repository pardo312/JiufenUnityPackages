using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio.Test
{
    public class TestAudio : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AudioJobExtras audioJobExtras= new AudioJobExtras(new AudioFadeInfo(true,1));
                AudioController.Instance.PlayAudio(AudioType.OST_Main_Theme,audioJobExtras);
                Debug.Log("Pressed q");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioController.Instance.PlayAudio(AudioType.SFX_1);
                Debug.Log("Pressed e");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                AudioController.Instance.PlayAudio(AudioType.SFX_2);
                Debug.Log("Pressed r");
            }

            //STOPS
            if (Input.GetKeyDown(KeyCode.A))
            {
                AudioJobExtras audioJobExtras= new AudioJobExtras(new AudioFadeInfo(true,1));
                AudioController.Instance.StopAudio(AudioType.OST_Main_Theme,audioJobExtras);
                Debug.Log("Pressed a");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                AudioController.Instance.StopAudio(AudioType.SFX_1);
                Debug.Log("Pressed d");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioController.Instance.StopAudio(AudioType.SFX_2);
                Debug.Log("Pressed f");
            }
        }
    }
}