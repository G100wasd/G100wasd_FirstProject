using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{

    public float speed;
    public Vector2 dir;
    public float liveTime;
    public float hurt;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("Destroy", liveTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (dir * speed));
    }

    private void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
