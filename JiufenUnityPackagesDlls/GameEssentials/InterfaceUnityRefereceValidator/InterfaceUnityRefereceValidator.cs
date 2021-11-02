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
                Debug.LogError($"<color=red>ValidateInterface:</color>  The item: [{unityObject.name}] is not a {typeof(T).Name} subclass. Please check the reference");
                return default(T);
            }
        }
    }
}
