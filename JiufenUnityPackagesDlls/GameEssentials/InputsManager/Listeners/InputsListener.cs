using System.Collections.Generic;

namespace JiufenGames.InputManager.Logic
{
    public interface InputsListener<T>
    {
        List<T> GetCurrentInputsPressed();
    }
}