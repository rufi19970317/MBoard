using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public void SetDeckCard(CardStruct card)
    {
        Card newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform).GetComponent<Card>();
        newCard.SetCard(null, card, Card.CardState.DeckCard);
        GameManager.Instance.deckCard = newCard;
    }

    public void SetNullCard(bool isActive)
    {
        transform.GetChild(1).gameObject.SetActive(isActive);
    }
}
