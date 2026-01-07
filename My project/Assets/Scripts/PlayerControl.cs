using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerControl : MonoBehaviour
{

    Vector2 dir = Vector2.zero;
    Rigidbody2D rb;

    [Tooltip("移动速度")]
    public float moveSpeed = 0.5f;
    [Tooltip("子弹")]
    public GameObject bullet;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        BaseSetting.attackMethod = 1;
    }

    void Update()
    {
        Key();
        Attack();
    }
    private void FixedUpdate()
    {
        float fix_t = Time.fixedDeltaTime; 
        if (dir != Vector2.zero)
        {
            rb.MovePosition(rb.position + dir*moveSpeed);
        }
    }

    private void Key()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");
    }

    private void Attack()
    {
        // 这里需要根据近战或者远程修改攻击
        if(Input.GetMouseButtonDown(0))
        {
            if (BaseSetting.attackMethod == 0)
            {
                // 这里需要播放近战动画
            }
            else
            {
                // 这里为远程攻击
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mouseDir = (mousePos - rb.position).normalized;
                GameObject obj = GameObject.Instantiate(bullet, this.transform);
                BulletControl src = obj.GetComponent<BulletControl>();
                src.dir = mouseDir;
                src.speed = 0.5f;
                src.liveTime = 1;


            }
        }
    }

}
