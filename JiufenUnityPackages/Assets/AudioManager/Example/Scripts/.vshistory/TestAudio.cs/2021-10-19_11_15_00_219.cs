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
                AudioController.Instance.PlayAudio(AudioType.SFX_1);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                AudioController.Instance.PlayAudio(AudioType.SFX_2);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioController.Instance.PlayAudio(AudioType.SFX_2);
            }
        }
    }
}
