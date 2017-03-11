using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class Egg : MonoBehaviour
    {
        public GameObject BreakPrefab;

        Rigidbody2D rb;

        public float Speed;

        Vector3 offset;
        
        float distanceToCamera;
        bool move;

        GameManager gameManager;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        void OnMouseDown()
        {
            move = true;
            distanceToCamera = transform.position.z - Camera.main.transform.position.z;
            offset = gameObject.transform.position -
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera));
        }

        void OnMouseUp()
        {
            move = false;
        }

        void Update()
        {
            if (move)
            {
                Vector3 target = Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera)
                ) + offset;

                rb.MovePosition(Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime));
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Instantiate(BreakPrefab, transform.position, transform.rotation);
            gameManager.OnGameOver();
            Destroy(gameObject);
        }
    }
}