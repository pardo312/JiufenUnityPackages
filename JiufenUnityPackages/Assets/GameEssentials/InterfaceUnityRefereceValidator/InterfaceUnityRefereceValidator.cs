using UnityEngine;

namespace JiufenModules.ScoreModule.Example
{
    public static class InterfaceUnityRefereceValidator
    {
        public static T ValidateIfUnityObjectIsOfType<T>(UnityEngine.Object unityObject)
        {
            if (unityObject is T)
            {
                return (T)(object)unityObject;
            }
            else if (((GameObject)unityObject).GetComponent<T>() != null)
            {
                return ((GameObject)unityObject).GetComponent<T>();
            }
            else
            {
                Debug.Log($"");
                return default(T);
            }
        }
    }
}
