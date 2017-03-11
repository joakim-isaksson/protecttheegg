using Ketsu.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ketsu
{
    public class WinText : MonoBehaviour
    {

        void Awake()
        {
            GetComponent<Text>().text = PersistentState.Instance.LastTime.ToString("00.000");
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}