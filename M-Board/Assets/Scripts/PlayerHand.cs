using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public int playerNum = 0;
    public bool isOppo = false;

    [SerializeField]
    Card RepCard;

    private void OnDisable()
    {
        RepCard.gameObject.SetActive(false);
    }

    // 게임 시작 시, 핸드 세팅
    public void SetHand(Hand hand, int num, bool isOppo)
    {
        // 전부 리셋
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // 나인지 상대인지 확인
        this.isOppo = isOppo;


        // 카드 세팅
        for (int i = 0; i < hand.Card.Count; i++)
        {
            Card newCard;
            if (!isOppo)
            {
                newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform).GetComponent<Card>();
                newCard.SetCard(this, hand.Card[i], Card.CardState.MyCard);
            }
            else
            {
                newCard = Instantiate(Resources.Load<GameObject>("Prefabs/OppoCard"), transform).GetComponent<Card>();
                newCard.SetCard(this, hand.Card[i], Card.CardState.OppoCard);
            }
        }

        playerNum = num;
    }

    public void SetRepCard(CardStruct cardInfo)
    {
        RepCard.gameObject.SetActive(true);
        RepCard.SetCard(this, cardInfo, Card.CardState.RepCard);
    }

    public Card GetRepCard()
    {
        return RepCard;
    }

    public void DrawCard(CardStruct card)
    {
        if (!isOppo)
        {
            Card newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform).GetComponent<Card>();
            newCard.SetCard(this, card, Card.CardState.MyCard);
            GameManager.Instance.FindAnswer(this);
        }
        else
        {
            Card newCard = Instantiate(Resources.Load<GameObject>("Prefabs/OppoCard"), transform).GetComponent<Card>();
            newCard.SetCard(this, card, Card.CardState.OppoCard);
            GameManager.Instance.FindAnswer(this);
        }
    }
}
