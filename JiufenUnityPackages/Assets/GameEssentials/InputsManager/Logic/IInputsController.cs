using System;
using System.Collections.Generic;
using UnityEngine;

namespace JiufenGames.TetrisAlike.Logic
{
    public interface IInputsController<T>
    {
        #region Fields
        //Inputs Config
        List<InputsListener<T>> m_inputsListener { get; }

        //Actions Dictionary
        Dictionary<string, Action<object[]>> m_actionsDictionary { get; set; }

        //Timers
        float m_initialTimeBetweenInputs { get; set; }
        float m_currentTimeBetweenInputs { get; set; }

        // KeyPressed Record
        List<T> m_currentPressedInputs { get; }
        List<T> m_lastInputPressed { get; }

        //Input timing
        int m_neededTimePresses { get; }
        float m_timesPressed { get; }
        #endregion Fields

        #region Methods
        void HandleInputs();
        void CheckContinuousInput(T input, int initWaitPressedTimesFactor, int onContinousMovementNeededPressesForNextMovement, Action inputAction);
        void CheckSingleInput(T keyCode, Action inputAction);
        void CheckIfIsPressingKey(T inputToCheck, Action<T> inputPressedAction);
        #endregion Methods
    }
}