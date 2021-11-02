using JiufenModules.ScoreModule.Logic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JiufenModules.ScoreModule.View
{
    public abstract class ScoreViewBase : MonoBehaviour, IScoreView
    {
        [Header("Current Score")]
        [SerializeField] private protected Transform m_rectParentScore;

        [SerializeField] private protected TMP_Text m_scoreValueText;

        public virtual void Init(object _initialData)
        {
            m_rectParentScore.gameObject.SetActive(true);
        }

        /// <summary>
        /// ChangeScore of the UI
        /// </summary>
        /// <param name="_finalScore"></param>
        public void ChangeScore(float _finalScore)
        {
            m_scoreValueText.text = _finalScore.ToString();
        }

        /// <summary>
        /// Add score to the ui, also
        /// can do animation and things with the _extraValue
        /// </summary>
        /// <param name="_finalScore"></param>
        /// <param name="_extraValue"></param>
        /// <param name="_extraParams"></param>
        public virtual void AddScore(float _finalScore, float _extraValue, object[] _extraParams = null)
        {
            ChangeScore(_finalScore);
        }

        /// <summary>
        /// remove score to the ui, also
        /// can do animation and things with the _removeValue
        /// </summary>
        /// <param name="_finalScore"></param>
        /// <param name="_removeValue"></param>
        /// <param name="_extraParams"></param>
        public virtual void RemoveScore(float _finalScore, float _removeValue, object[] _extraParams = null)
        {
            ChangeScore(_finalScore);
        }

    }
}
