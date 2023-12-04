using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static AnswerManager;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class AnswerCard : MonoBehaviour
{
    AnswerManager.LeaderAnswerSet leaders;

    [SerializeField]
    private Image frontImage1;
    [SerializeField]
    private Image frontImage2;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private TMP_Text point;

    public List<Card> answerResult = new List<Card>();

    public void SetAnswer(AnswerManager.LeaderAnswerSet leaders)
    {
        this.leaders = leaders;
        Debug.Log(leaders.name);
        frontImage1.sprite = Resources.Load<Sprite>("Images/Icon/Icon_" + leaders.university[0].ToString());
        frontImage2.sprite = Resources.Load<Sprite>("Images/Icon/Icon_" + leaders.university[1].ToString());
        text.text = leaders.name;
        if (leaders.point > 1)
            point.text = "x" + leaders.point.ToString();
        else
            point.text = "";
    }

    public void SetResult(List<Card> result)
    {
        answerResult = result;
    }

    public void OnMouseEnter()
    {
        /*
        if (particle.activeSelf)
        {
            GameManager.Instance.EndGamePlayerWin(this, answerResult);
            return;
        }

        GameManager.Instance.OpenAnswerCardPopup(this, result);
        */
    }

    public void OnMouseExit()
    {
        
    }

    [SerializeField]
    GameObject particle;

    public void SetParticle(bool isActive)
    {
        particle.SetActive(isActive);
    }
}
