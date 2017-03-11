using Ketsu.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ketsu
{
    public class GameManager : ExtendedMonoBehaviour
    {
        public Text TimeText;

        float passedTime;

        MenuManager menu;

        [HideInInspector]
        public static bool GameOver;

        void Awake()
        {
            GameOver = false;
        }

        void Start()
        {
            menu = FindObjectOfType<MenuManager>();
        }

        void Update()
        {
            if (GameOver) return;

            passedTime += Time.deltaTime;
            TimeText.text = passedTime.ToString("00.000");
        }

        public void OnGameOver()
        {
            GameOver = true;
            PersistentState.Instance.LastTime = passedTime;

            foreach(Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.Freeze();
            }

            DelayedAction(1.0f, delegate
            {
                menu.LoadNextScene();
            });
        }
    }
}