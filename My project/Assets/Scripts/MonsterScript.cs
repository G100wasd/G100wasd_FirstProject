using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed;
    public float life;

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
        if (collision.CompareTag("Player"))
        {
            GameObject.Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Bullet"))
        {
            life -= collision.gameObject.GetComponent<BulletControl>().hurt;
            if (life <= 0) {
                BaseSetting.exp += 1;
                GameObject.Destroy(this.gameObject); 
            }
            else { StartCoroutine(GetHurt()); }

        }
    }


    IEnumerator GetHurt()
    {
        SpriteRenderer col = this.GetComponent<SpriteRenderer>();
        col.color = Color.red;
        yield return new WaitForSeconds(0.1f); 
        col.color = Color.white;
    }

}
