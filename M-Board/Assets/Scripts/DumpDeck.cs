using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpDeck : MonoBehaviour
{
    public Card DumpCard;
    int DumpDeckCount = 0;
    [SerializeField]
    UI_DumpCardList ui_DumpCardList;


    public void CardDump(CardStruct discardCard)
    {
        if (DumpCard == null)
            DumpCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform).GetComponent<Card>();
        DumpCard.SetCard(discardCard);
        DumpCard.ReverseCard(false);
        DumpDeckCount++;
        ui_DumpCardList.SetDumpCardList(discardCard);
    }

    public void ResetDumpDeck()
    {
        ui_DumpCardList.ResetList();
        DumpDeckCount = 0;
    }
}
