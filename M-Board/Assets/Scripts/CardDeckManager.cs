using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CardDeckManager;

// Ä«µå ±¸Á¶Ã¼
public struct CardStruct
{
    public UniverSity univerSity;
    public int num;

    public CardStruct(UniverSity univerSity, int num)
    {
        this.univerSity = univerSity;
        this.num = num;
    }
}


public class CardDeckManager : MonoBehaviour
{
    // ´Ü°ú´ë 10 Á¾·ù
    public enum UniverSity
    {
        Business,
        Engineering,
        Humanities,
        IC,
        Medical,
        NaturalScience,
        Nursing,
        Pharmacy,
        SocialSciences,
        Software,
        Joker
    }

    // Ä«µåµ¦
    List<CardStruct> Deck = new List<CardStruct>();

    public void Start()
    {
        SetDeck();
    }

    // µ¦ ¼³Á¤
    public void SetDeck()
    {
        CreateDeck();
        ShuffleDeck();
    }

    // µ¦ »ý¼º
    private void CreateDeck()
    {
        Deck = new List<CardStruct>();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 1; j <= 5; j++)
            {
                Deck.Add(new CardStruct((UniverSity)i, j));
            }
        }
        Deck.Add(new CardStruct(UniverSity.Joker, 10));
        Deck.Add(new CardStruct(UniverSity.Joker, 11));
        Deck.Add(new CardStruct(UniverSity.Joker, 12));
    }

    // µ¦ ¼ÅÇÃ
    private void ShuffleDeck()
    {
        Deck.Shuffle();
    }

    // Ä«µå ÁÖ±â
    public CardStruct DrawCard()
    {
        if(Deck.Count == 0)
        {
            GameManager.Instance.ResetRound();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return new CardStruct();
        }

        CardStruct nowCard = Deck[0];
        Deck.RemoveAt(0);
        return nowCard;
    }
}
