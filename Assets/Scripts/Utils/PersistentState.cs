using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ketsu.Utils
{
	public class PersistentState : MonoBehaviour
	{
		[HideInInspector]
		public static PersistentState Instance = null;

        [HideInInspector]
        public float LastTime;

		/// <summary>
		/// Destroy this singleton instance
		/// </summary>
		public static void DestroySingleton()
		{
			Destroy(Instance.gameObject);
			Instance = null;
		}

		void Awake()
		{
			// Make this an indestructible singleton
			if (Instance == null) Instance = this;
			else if (!Instance.Equals(this)) Destroy(gameObject);
			DontDestroyOnLoad(gameObject);
		}
	}
}