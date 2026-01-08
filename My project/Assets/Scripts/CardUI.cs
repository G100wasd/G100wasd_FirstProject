
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public RectTransform mainCanva;
    public GameObject player;
    public List<GameObject> buttons;

    RectTransform tran;
    Vector2 normalPos;
    Vector2 showPos = Vector2.zero;
    Vector2 targetPos;

    void Start()
    {
        tran = this.GetComponent<RectTransform>();
        normalPos = new Vector2(0, mainCanva.rect.height);
        targetPos = normalPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { ChangeMenuShow(); }
        MenuMove();
        if (Input.GetKeyDown(KeyCode.O)) { CardsInit(); }
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
        foreach(var item in buttons)
        {
            int index = Random.Range(0, Skill.Length);
            Button btn = item.GetComponent<Button>();
            TextMeshProUGUI text = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>();


            btn.onClick.AddListener(ChangeMenuShow);
            btn.onClick.AddListener(Skill[index]);
            text.text = SkillDescribe[index];
        }
    }

    public UnityAction[] Skill =
    {
        () =>
        {
            Debug.Log("wasd");
        },
    };

    public string[] SkillDescribe =
    {
        "wasd",
    };
}
