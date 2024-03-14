 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed = 2f;
    public Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        
        if (CompareTag("bullet"))
            _rb.velocity = new Vector2(bulletSpeed, 0f);
        //if (CompareTag("EnemyBullet"))
            //_rb.velocity = new Vector2(bulletSpeed, 0f);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
    */
}