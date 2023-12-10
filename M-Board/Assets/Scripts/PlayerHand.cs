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

    // ���� ���� ��, �ڵ� ����
    public List<Card> SetHand(Hand hand, int num, bool isOppo)
    {
        List<Card> cards = new List<Card>();
        // ���� ����
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // ������ ������� Ȯ��
        this.isOppo = isOppo;


        // ī�� ����
        for (int i = 0; i < hand.Card.Count; i++)
        {
            Card newCard;
            if (!isOppo)
            {
                newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform).GetComponent<Card>();
                newCard.SetCard(this, hand.Card[i], Card.CardState.MyCard);
                cards.Add(newCard);
            }
            else
            {
                newCard = Instantiate(Resources.Load<GameObject>("Prefabs/OppoCard"), transform).GetComponent<Card>();
                newCard.SetCard(this, hand.Card[i], Card.CardState.OppoCard);
            }
        }

        playerNum = num;
        return cards;
    }

    public Card SetRepCard(CardStruct cardInfo)
    {
        RepCard.gameObject.SetActive(true);
        return RepCard.SetCard(this, cardInfo, Card.CardState.RepCard);
    }

    public Card GetRepCard()
    {
        return RepCard;
    }

    public Card DrawCard(CardStruct card)
    {
        if (!isOppo)
        {
            Card newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform).GetComponent<Card>();
            newCard.SetCard(this, card, Card.CardState.MyCard);
            return newCard;
        }
        else
        {
            Card newCard = Instantiate(Resources.Load<GameObject>("Prefabs/OppoCard"), transform).GetComponent<Card>();
            newCard.SetCard(this, card, Card.CardState.OppoCard);
            return newCard;
        }
    }

    public void ChangeCard(CardStruct card)
    {
        Card newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform).GetComponent<Card>();
        newCard.SetCard(this, card, Card.CardState.MyCard);
    }
}
