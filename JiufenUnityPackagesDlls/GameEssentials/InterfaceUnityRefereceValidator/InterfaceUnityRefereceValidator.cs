using System.Collections.Generic;
using UnityEngine;

namespace JiufenModules.ScoreModule.Example
{
    public static class InterfaceUnityRefereceValidator
    {
        public static T ValidateIfUnityObjectIsOfType<T>(UnityEngine.Object unityObject)
        {
            if (unityObject == null)
                return default(T);

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

        public static List<T> ValidateIfUnityObjectArrayIsOfType<T>(UnityEngine.Object[] unityObjects)
        {
            List<T> returnList = new List<T>();
            for (int i = 0; i < unityObjects.Length; i++)
            {
                UnityEngine.Object item = unityObjects[i];

                if (item == null)
                    continue;

                if (item is T)
                {
                    returnList.Add((T)(object)item);
                }
                else if (((GameObject)item).GetComponent<T>() != null)
                {
                    returnList.Add((item as GameObject).GetComponent<T>());
                }
                else
                {
                    Debug.LogError($"<color=red>ValidateInterface:</color>  The item: [{item.name}] is not a {typeof(T).Name} subclass. Please check the reference");
                }
            }

            //Return List
            return returnList;
        }
    }
}
