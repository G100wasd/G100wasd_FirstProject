
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
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


    private void Creat()
    {
        Vector2 playerPos = player.transform.position;
        int creatCount = Random.Range(3, 5);
        for (int i = 0; i < creatCount && count<MaxCount; i++)
        {
            float ang = Random.Range(0, 2.0f);
            creatDir = (new Vector2(Mathf.Cos(ang*pi), Mathf.Sin(ang*pi)));
            Vector2 creatPos = playerPos + creatDir * radius;

            // 这里需要根据概率生成不同的敌人
            System.Func<int> GetIndex = () =>
            {
                float creatProbability = Random.Range(0f, 1f);
                for (int j = 0; j < monsterList.Count(); j++)

                {
                    if (creatProbability < monsterList[j].probability)
                    {
                        return j;
                    }
                }
                return 0;
            };
            int index = GetIndex();

            GameObject obj = GameObject.Instantiate(monsterList[index].monsterPerhab, this.transform);
            MonsterScript src = obj.GetComponent<MonsterScript>();
            src.player = player.GetComponent<Rigidbody2D>();

            obj.transform.position = creatPos;
            src.moveSpeed = monsterList[index].moveSpeed;
            src.life = monsterList[index].life;

            count++;
        }
    }

    public void ChangeProbability(int level)
    {
        float x = (Mathf.Atan(level)) / (Mathf.PI / 2);
        x = (x + 1) / 2;
        float total = 0;

        for (int i = 0; i < monsterList.Count(); i++)
        {
            monsterList[i].weight = Mathf.Exp((i+1)*x);
            total += monsterList[i].weight;
        }
        foreach (var item in monsterList)
        {
            item.probability = item.weight / total;
            Debug.Log(item.probability);
        }
        for (int i = 0; i < monsterList.Count()-1; i++)
        {
            monsterList[i + 1].probability += monsterList[0].probability;
        }

    }
}

[System.Serializable]
public class Monster
{
    public GameObject monsterPerhab;
    public float moveSpeed;
    public float life;
    public float probability;
    public float weight;
}
