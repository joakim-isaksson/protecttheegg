using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Ketsu
{
    public class AudioManager : MonoBehaviour
    {
        public AudioMixer Mixer;

        [HideInInspector]
        public static AudioManager instance = null;

        public static void DestroySingleton()
        {
            Destroy(instance.gameObject);
            instance = null;
        }

        void Awake()
        {
            // Singleton
            if (instance == null) instance = this;
            else if (!instance.Equals(this)) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        public void TurnOn(string groupName)
        {
            Mixer.SetFloat(groupName, 0.0f);
        }

        public void TurnDown(string groupName)
        {
            Mixer.SetFloat(groupName, -80.0f);
        }

        public void Fade(string groupName, float toVolume, float seconds)
        {
            float toDesibel = Mathf.Lerp(-80.0f, 0.0f, toVolume);
            StartCoroutine(StartFade(groupName, toDesibel, seconds, 0));
        }

        public void Fade(string groupName, float toVolume, float seconds, float delay)
        {
            float toDesibel = Mathf.Lerp(-80.0f, 0.0f, toVolume);
            StartCoroutine(StartFade(groupName, toDesibel, seconds, delay));
        }

        IEnumerator StartFade(string groupName, float toDesibel, float seconds, float delay)
        {
            yield return new WaitForSeconds(delay);

            float volume;
            Mixer.GetFloat(groupName, out volume);

            float progress = 0;
            while (progress < 1.0f)
            {
                progress += (Time.deltaTime / seconds);
                Mixer.SetFloat(groupName, Mathf.Lerp(volume, toDesibel, progress));

                yield return null;
            }

            Mixer.SetFloat(groupName, toDesibel);
        }
    }
}