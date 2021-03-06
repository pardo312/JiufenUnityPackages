using UnityEngine;

namespace JiufenModules.ScoreModule.Logic
{
    public interface IScoreController
    {
        float m_currentScore { get; }
        IScoreView m_scoreView { get; }

        void Init(object _initialData);
        void AddScore(float _extraValue, object[] _extraParams = null);
        void RemoveScore(float _valueToRemove, object[] _extraParams = null);
        void ChangeScore(float _newScore);
    }
}