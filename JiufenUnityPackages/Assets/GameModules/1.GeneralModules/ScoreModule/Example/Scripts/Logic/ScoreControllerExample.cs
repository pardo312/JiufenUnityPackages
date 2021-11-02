using JiufenModules.ScoreModule.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JiufenModules.ScoreModule.Example
{
    public class ScoreControllerExample : ScoreControllerBase
    {
        private int highScore = 0;
        public override void Init(object _initialData)
        {
            base.Init(_initialData);
            ScoreExampleInitialData scoreData = _initialData as ScoreExampleInitialData;
            highScore = scoreData.highScore;
            ChangeScore(scoreData.initScore);
        }
    }
}
