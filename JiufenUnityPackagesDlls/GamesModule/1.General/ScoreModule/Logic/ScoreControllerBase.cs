using JiufenModules.InterfaceReferenceValidator;
using UnityEngine;
using UnityEngine.Scripting;

namespace JiufenModules.ScoreModule.Logic
{
    public abstract class ScoreControllerBase : MonoBehaviour, IScoreController
    {
        #region Fields
        #region Backing Fields
        [SerializeField] private UnityEngine.Object m_scoreViewField;
        private float m_currentScoreField;
        #endregion Backing Fields

        #region Properties
        public float m_currentScore { get => m_currentScoreField; private set => m_currentScoreField = value; }
        public IScoreView m_scoreView => InterfaceUnityRefereceValidator.ValidateIfUnityObjectIsOfType<IScoreView>(m_scoreViewField);
        #endregion Properties
        #endregion Fields

        #region Methods
        public virtual void Init(object _initialData)
        {
            m_scoreView.Init(_initialData);
            ChangeScore(0);
        }

        public virtual void AddScore(float _extraValue, object[] _extraParams = null)
        {
            m_currentScore += (int)_extraValue;
            m_scoreView.AddScore(m_currentScore, _extraValue, _extraParams);
        }

        public void RemoveScore(float _removeValue, object[] _extraParams = null)
        {
            m_currentScore -= (int)_removeValue;
            m_scoreView.RemoveScore(m_currentScore, _removeValue, _extraParams);
        }

        public void ChangeScore(float _newScore)
        {
            m_currentScore = _newScore;
            m_scoreView.ChangeScore(_newScore);
        }
        #endregion Methods
    }
}