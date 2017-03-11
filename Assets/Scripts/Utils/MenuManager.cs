using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ketsu.Utils
{
    public class MenuManager : ExtendedMonoBehaviour
    {
        public float FadeSpeed;
        public string NextScene;
        public string ExitScene;
        public bool QuitOnExit;
        public bool AutoLoad;
        public float AutoLoadTime;
        public bool AllowReload;
        public bool LoadNextSceneOnKeyPress;

        ScreenFaider Faider;

        bool loading = true;

        void Start()
        {
            Faider = ScreenFaider.Instance;

            Faider.FadeOut(FadeSpeed, delegate
            {
                loading = false;
                if (AutoLoad)
                {
                    DelayedAction(AutoLoadTime, delegate
                    {
                        LoadScene(ExitScene);
                    });
                }
            });
        }

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (QuitOnExit) Application.Quit();
                else LoadScene(ExitScene);
            }
            else if (AllowReload && Input.GetKeyDown(KeyCode.R)) LoadScene(SceneManager.GetActiveScene().name);
            else if (LoadNextSceneOnKeyPress && Input.anyKeyDown) LoadScene(NextScene);
        }

        public void LoadNextScene()
        {
            LoadScene(NextScene);
        }

        void LoadScene(string name)
        {
            if (loading) return;
            loading = true;

            Faider.FadeIn(FadeSpeed, delegate
            {
                SceneManager.LoadScene(name, LoadSceneMode.Single);
            });
        }
    }
}