using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class MonsterCreat : MonoBehaviour
{
    static public int count = 0;
    int MaxCount = 100;

    public GameObject player;
    public List<string> monsterName;
    public List<Monster> monsterList;

    float radius = 25;
    float pi = Mathf.PI;
    Vector2 creatDir;
    void Start()
    {
        InvokeRepeating("Creat", 2, 5);
    }

    void Update()
    {
        
    }

    private void Creat()
    {
        Vector2 playerPos = player.transform.position;
        int creatCount = Random.Range(3, 5);
        for (int i = 0; i < creatCount && count<MaxCount; i++)
        {
            float ang = Random.Range(0, 2.0f);
            creatDir = (new Vector2(Mathf.Cos(ang*pi), Mathf.Sin(ang*pi)));
            Vector2 creatPos = playerPos + creatDir * radius;

            GameObject obj = GameObject.Instantiate(monsterList[0].monsterPerhab, this.transform);
            MonsterScript src = obj.GetComponent<MonsterScript>();

            // 这里需要根据概率生成不同的敌人

            obj.transform.position = creatPos;
            src.player = player.GetComponent<Rigidbody2D>();
            src.moveSpeed = 0.05f;
            src.life = 3;

            count++;
        }
    }


}

[System.Serializable]
public class Monster
{
    public float moveSpeed;
    public GameObject monsterPerhab;
}
