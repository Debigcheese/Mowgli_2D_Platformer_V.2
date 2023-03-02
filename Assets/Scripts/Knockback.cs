using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 10f;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}