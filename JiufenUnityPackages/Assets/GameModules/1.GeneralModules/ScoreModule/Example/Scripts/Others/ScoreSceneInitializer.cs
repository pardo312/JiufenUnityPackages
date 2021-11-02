using JiufenModules.ScoreModule.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JiufenModules.ScoreModule.Example
{
    public class ScoreSceneInitializer : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Object m_scoreControllerField;
        public IScoreController m_scoreController => InterfaceUnityRefereceValidator.ValidateIfUnityObjectIsOfType<IScoreController>(m_scoreControllerField);


        [SerializeField] private int m_initialScore = 0;
        [SerializeField] private int m_highScore = 100;
        public void Start()
        {
            m_scoreController.Init(new ScoreExampleInitialData() { initScore = m_initialScore, highScore = m_highScore });
        }
    }
}
