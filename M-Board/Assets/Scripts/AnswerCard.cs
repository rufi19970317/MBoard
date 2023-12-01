using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static AnswerManager;
using System.Linq;
using UnityEngine.UI;

public class AnswerCard : MonoBehaviour, IPointerClickHandler
{
    AnswerManager.LeaderAnswerSet leaders;

    [SerializeField]
    public Image frontImage1;
    public Image frontImage2;

    public List<Card> answerResult = new List<Card>();

    public void SetAnswer(AnswerManager.LeaderAnswerSet leaders)
    {
        this.leaders = leaders;
        frontImage1.sprite = Resources.Load<Sprite>("/Images/Icon/" + leaders.university[0]);
        frontImage2.sprite = Resources.Load<Sprite>("/Images/Icon/" + leaders.university[1]);
    }

    public void SetResult(List<Card> result)
    {
        answerResult = result;
    }

    public void OnPointerClick(PointerEventData eventData)
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

    [SerializeField]
    GameObject particle;

    public void SetParticle(bool isActive)
    {
        particle.SetActive(isActive);
    }
}
