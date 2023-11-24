using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static AnswerManager;
using System.Linq;
using UnityEngine.UI;

public class AnswerCard : MonoBehaviour, IPointerClickHandler
{
    AnswerManager.Answer answer;
    [SerializeField]
    public Image frontImage;
    public List<Card> answerResult = new List<Card>();

    public void SetAnswer(AnswerManager.Answer answer)
    {
        this.answer = answer;
    }

    public void SetResult(List<Card> result)
    {
        answerResult = result;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (particle.activeSelf)
        {
            GameManager.Instance.EndGamePlayerWin(this, answerResult);
            return;
        }

        PlayerHand playerHand = GameManager.Instance.nowPlayerHand;
        List<Card> playerCards = new List<Card>();

        playerCards.Add(playerHand.GetRepCard());
        for (int i = 0; i < playerHand.transform.childCount; i++)
        {
            playerCards.Add(playerHand.transform.GetChild(i).GetComponent<Card>());
        }

        List<Card> sortCards = (from cards in playerCards
                                orderby cards.cardInfo.num
                                select cards).ToList();

        sortCards = (from cards in sortCards
                        orderby cards.cardInfo.univerSity
                        select cards).ToList();

        List<Card> result = new List<Card>();
        if (answer.university.Count > 0)
        {
            for (int i = 0; i < answer.university.Count; i++)
            {
                for (int j = 0; j < sortCards.Count; j++)
                {
                    if (sortCards[j].cardInfo.univerSity == answer.university[i])
                    {
                        result.Add(sortCards[j]);
                        sortCards.RemoveAt(j);
                        break;
                    }
                }
            }
        }

        if (answer.grades.Count > 0)
        {
            List<Grades> sortGrades = (from grade in answer.grades
                                        orderby grade.grade
                                        select grade).ToList();


            for (int i = 0; i < sortGrades.Count; i++)
            {
                if (sortGrades[i].compare == Grades.Compare.Equal)
                {
                    for (int j = 0; j < sortCards.Count; j++)
                    {
                        if (sortCards[j].cardInfo.num == sortGrades[i].grade)
                        {
                            result.Add(sortCards[j]);
                            sortCards.RemoveAt(j);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < sortGrades.Count; i++)
            {
                if (sortGrades[i].compare == Grades.Compare.Up)
                {
                    for (int j = 0; j < sortCards.Count; j++)
                    {
                        if (sortCards[j].cardInfo.num >= sortGrades[i].grade)
                        {
                            result.Add(sortCards[j]);
                            sortCards.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
        }
        GameManager.Instance.OpenAnswerCardPopup(this, result);
    }

    [SerializeField]
    GameObject particle;

    public void SetParticle(bool isActive)
    {
        particle.SetActive(isActive);
    }
}
