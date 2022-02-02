using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JiufenPackages.SceneFlow.Logic
{
    public class SceneFlowManager : MonoBehaviour
    {
        #region ----Fields----
        [SerializeField] private string loadingSceneName = "Loading";

        private Dictionary<string, IInitializable> initilizables = new Dictionary<string, IInitializable>();
        private string previousScene = "";
        #endregion ----Fields----

        #region ----Methods----
        #region Init
        public void Init()
        {
            IInitializable[] initializableList = GetComponents<IInitializable>();

            for (int i = 0; i < initializableList.Length; i++)
                initilizables.Add(initializableList[i].m_sceneName, initializableList[i]);
        }
        #endregion Init

        #region Change Scene
        public void ChangeSceneTo(string nameOfScene)
        {
            previousScene = SceneManager.GetActiveScene().name;
            ShowLoadingScene();
            LoadScene(nameOfScene);
        }

        private void LoadScene(string nameOfScene)
        {
            if (CheckIfSceneExist(nameOfScene))
            {
                SceneManager.LoadSceneAsync(nameOfScene);
            }
            else
            {
                if (nameOfScene.CompareTo(previousScene) != 0)
                {
                    SceneManager.LoadScene(previousScene);
                    Debug.Log($"Scene {nameOfScene} doesn't exist in build. Going back to previous scene: {previousScene}");
                }
                else
                {
                    Debug.LogError($"SceneFlow Fatal: Scene {nameOfScene} doesn't exist in build. Previous Scene is equal to current scene so couldn't go back.");
                }
            }
        }
        #endregion Change Scene

        #region LoadingScene
        private void ShowLoadingScene()
        {
            SceneManager.LoadSceneAsync(loadingSceneName);
        }

        private void HideLoadingScene(bool loadingSuccess)
        {
            if (loadingSuccess)
                SceneManager.UnloadSceneAsync(loadingSceneName);
        }
        #endregion LoadingScene

        #region Init Scene
        public void InitScene(string sceneName)
        {
            if (sceneName != loadingSceneName)
                initilizables[sceneName].GetData(InitializeSceneController);
        }

        private void InitializeSceneController(object data)
        {
            SceneController sceneController = FindObjectOfType<SceneController>();
            sceneController.Init(data, (successLoadingScene) =>
            {
                HideLoadingScene(successLoadingScene);
            });
        }

        private bool CheckIfSceneExist(string nameOfScene)
        {
            List<string> sceneLoadedNames = new List<string>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                sceneLoadedNames.Add(SceneManager.GetSceneAt(i).name);
            }

            if (sceneLoadedNames.Contains(nameOfScene))
            {
                return true;
            }
            return false;
        }
        #endregion Init Scene
        #endregion ----Methods----
    }
}