
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public RectTransform mainCanva;
    public List<GameObject> buttons;
    public GameObject player;
    PlayerControl playerSrc;

    RectTransform tran;
    Vector2 normalPos;
    Vector2 showPos = Vector2.zero;
    Vector2 targetPos;

    UnityAction[] Skills;
    string[] SkillDescribe;

    void Start()
    {
        tran = this.GetComponent<RectTransform>();
        normalPos = new Vector2(0, mainCanva.rect.height);
        targetPos = normalPos;
        playerSrc = player.GetComponent<PlayerControl>();

        Skills = new UnityAction[]
        {
            () =>
            {
                if (playerSrc.moveSpeed < 1.0f) {  playerSrc.moveSpeed += 0.02f; }
            },
            () =>
            {
                if(playerSrc.bulletSpeed < 5.0f) { playerSrc.bulletSpeed += 0.02f; }
            },
            () =>
            {
               playerSrc.bulletLiveTime += 0.02f;
            }
            

        };
        SkillDescribe = new string[]
        {
            "add move speed",
            "add bullet speed",
            "bullet live longer",
        };

}

    // Update is called once per frame
    void Update()
    {
        MenuMove();
        if (BaseSetting.exp >= 10)
        {
            CardsInit();
            ChangeMenuShow();
            BaseSetting.exp = 0;
        }
    }

    public void ChangeMenuShow()
    {
        targetPos = targetPos == normalPos ? showPos : normalPos;
        if (targetPos == showPos) { Time.timeScale = 0f; }
        else { Time.timeScale = 1.0f; }


    }
    private void MenuMove()
    { 
        if (Vector2.Distance(tran.anchoredPosition, targetPos) < 0.5f)
        {
            tran.anchoredPosition = targetPos;
        }
        else
        {
            tran.anchoredPosition = Vector2.MoveTowards(tran.anchoredPosition, targetPos, 5f);
        }
    }

    public void CardsInit()
    {
        CardClear();
        foreach(var item in buttons)
        {
            int index = Random.Range(0, Skills.Length);
            Button btn = item.GetComponent<Button>();
            TextMeshProUGUI text = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            btn.onClick.AddListener(ChangeMenuShow);
            btn.onClick.AddListener(CardClear);
            btn.onClick.AddListener(Skills[index]);
            text.text = SkillDescribe[index];
        }
    }

    private void CardClear()
    {
        foreach (var item in buttons)
        {
            Button btn = item.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
        }
    }

}
