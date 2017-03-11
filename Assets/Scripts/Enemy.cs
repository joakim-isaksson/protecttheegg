using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class Enemy : MonoBehaviour
    {
        public float StartingForce;
        public float SpeedUpDelay;
        public float SpeedUpForce;

        public AudioClip CollisionSound;

        AudioSource asrc;

        Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            asrc = GetComponent<AudioSource>();
        }

        void Start()
        {
            Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized;
            rb.AddForce(direction * StartingForce);

            StartCoroutine(SpeedUp());
        }

        public void Freeze()
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            asrc.PlayOneShot(CollisionSound);
        }

        IEnumerator SpeedUp()
        {
            while(true)
            {
                yield return new WaitForSeconds(SpeedUpDelay);
                rb.AddForce(rb.velocity.normalized * SpeedUpForce);
            }
        }
    }
}