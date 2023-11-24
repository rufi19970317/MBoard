using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpDeck : MonoBehaviour
{
    public Card DumpCard;
    int DumpDeckCount = 0;

    public void CardDump(CardStruct discardCard)
    {
        if (DumpCard == null)
            DumpCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform).GetComponent<Card>();
        DumpCard.SetCard(discardCard);
        DumpCard.ReverseCard(false);
        DumpDeckCount++;
    }

    public void ResetDumpDeck()
    {
        DumpDeckCount = 0;
    }
}
