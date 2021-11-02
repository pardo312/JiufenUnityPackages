using JiufenModules.ScoreModule.Logic;
using JiufenModules.ScoreModule.View;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JiufenModules.ScoreModule.Example
{
    public class ScoreViewExample : ScoreViewBase
    {
        [Header("High Score")]
        [SerializeField] private Transform m_rectParentHighScore;

        [SerializeField] private TMP_Text m_scoreValueHighText;

        public override void Init(object _initialData)
        {
            base.Init(_initialData);
            //Highscore
            m_rectParentHighScore.gameObject.SetActive(true);
            ScoreExampleInitialData scoreData = _initialData as ScoreExampleInitialData;
            m_scoreValueText.text = scoreData.highScore.ToString();
            m_scoreValueHighText.text = scoreData.highScore.ToString();
        }
    }
}
