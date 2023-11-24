using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    bool isAnswer = false;
    GameObject hand;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SetEnemy(GameObject playerHand)
    {
        hand = playerHand;
    }

    public void StartEnemy()
    {
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSecondsRealtime(2f);
        DrawCard();
        yield return new WaitForSecondsRealtime(3f);

        if (isAnswer)
        {
            Finish();
        }
        else
        {
            DiscardCard();
        }
    }

    void DrawCard()
    {
        GameManager.Instance.phase = GameManager.Phase.Draw;
        System.Random ran = new System.Random();
        int a = ran.Next(0, 2);
        if (a == 0)
        {
            Card card = GameManager.Instance.deckCard;
            GameManager.Instance.EnemyDrawCard(card, Card.CardState.DeckCard);
        }
        else
        {
            int b = ran.Next(0, 4);
            Card card = GameObject.Find("PlayerHand").transform.GetChild(b).GetComponent<Card>();
            GameManager.Instance.EnemyDrawCard(card, Card.CardState.MyCard);
        }

        GameManager.Instance.phase = GameManager.Phase.Discard;
    }

    void DiscardCard()
    {
        System.Random ran = new System.Random();
        int b = ran.Next(0, 5);
        GameManager.Instance.DiscardCard(hand.transform.GetChild(b).GetComponent<Card>());
    }

    void Finish()
    {

    }
}
