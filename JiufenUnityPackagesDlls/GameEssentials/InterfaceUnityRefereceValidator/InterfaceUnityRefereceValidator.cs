using System;
using System.Collections.Generic;
using UnityEngine;

namespace JiufenModules.ScoreModule.Example
{
    public static class InterfaceUnityRefereceValidator
    {
        public static T ValidateIfUnityObjectIsOfType<T>(UnityEngine.Object unityObject, string classNamespace = null)
        {
            if (unityObject == null)
                return default(T);

            if (unityObject is T)
            {
                return (T)(object)unityObject;
            }
            else if (unityObject is GameObject && (unityObject  as GameObject).GetComponent<T>() != null)
            {
                return ((GameObject)unityObject).GetComponent<T>();
            }
            else
            {
                Type classType = Type.GetType(classNamespace + unityObject.name);
                if (classType != null)
                    return  (T)Activator.CreateInstance(classType);

                Debug.LogError($"<color=red>ValidateInterface:</color>  The item: [{unityObject.name}] is not a {typeof(T).Name} subclass. Please check the reference");
                return default(T);
            }
        }

        public static List<T> ValidateIfUnityObjectArrayIsOfType<T>(UnityEngine.Object[] unityObjects, string classNamespace = null)
        {
            List<T> returnList = new List<T>();
            for (int i = 0; i < unityObjects.Length; i++)
            {
                UnityEngine.Object item = unityObjects[i];

                if (item == null)
                    continue;


                if ((object)item is T)
                {
                    returnList.Add((T)(object)item);
                }
                else if (item is GameObject && (item as GameObject).GetComponent<T>() != null)
                {
                    returnList.Add((item as GameObject).GetComponent<T>());
                }
                else
                {
                    Type classType = Type.GetType(classNamespace + item.name);
                    if (classType != null)
                    {
                        returnList.Add((T)Activator.CreateInstance(classType));
                        continue;
                    }
                    Debug.LogError($"<color=red>ValidateInterface:</color>  The item: [{item.name}] is not a {typeof(T).Name} subclass. Please check the reference");
                }

            }
            //Return List
            return returnList;
        }
    }
}
