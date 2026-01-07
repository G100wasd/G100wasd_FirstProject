using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float fix_t = Time.fixedDeltaTime;
        MoveToPlayer(fix_t);
    }

    private void MoveToPlayer(float t)
    {
        Vector2 targetPos = Vector2.MoveTowards(rb.position, player.position, moveSpeed);
        rb.MovePosition(targetPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Bullet"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }

}
