using System.Collections;
using UnityEngine;

namespace Ketsu.Utils
{
    public class DestroyAfterDelay : MonoBehaviour
    {
        public float Seconds;

        void Start()
        {
            StartCoroutine(DestroyAfterSeconds(Seconds));
        }

        IEnumerator DestroyAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}