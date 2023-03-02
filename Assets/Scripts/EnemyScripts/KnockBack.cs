
using UnityEngine;

public class KnockBack : MonoBehaviour
{

        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                 player.Knockback(transform);
            }
        }
   



}