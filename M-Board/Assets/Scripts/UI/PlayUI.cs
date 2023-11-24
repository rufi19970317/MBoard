using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    [SerializeField]
    GameObject playerHand;

    [SerializeField]
    GameObject[] otherHands;

    private List<PlayerHand> playerHands = new List<PlayerHand>();

    private void OnDisable()
    {
        for(int i = 0; i < otherHands.Length; i++)
        {
            otherHands[i].SetActive(false);
        }
    }

    // 플레이어들 핸드 세팅
    public void SetHands(int nowPlayerNum, List<Hand> hands)
    {
        playerHands = new List<PlayerHand>();

        // 자신의 핸드 세팅
        playerHand.GetComponent<PlayerHand>().SetHand(hands[nowPlayerNum], nowPlayerNum, false);

        // 상대 핸드 세팅
        // 2인 플레이 시
        if (hands.Count == 2)
        {
            otherHands[1].SetActive(true);

            if (nowPlayerNum == 1)
            {
                otherHands[1].GetComponent<PlayerHand>().SetHand(hands[0], 0, true);
                
                playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
                playerHands.Add(playerHand.GetComponent<PlayerHand>());
            }
            else
            {
                otherHands[1].GetComponent<PlayerHand>().SetHand(hands[1], 1, true);

                playerHands.Add(playerHand.GetComponent<PlayerHand>());
                playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
            }
        }
        // 3인 이상 플레이 시
        else
        {
            for(int i = 0; i < hands.Count - 1; i++)
            {
                otherHands[i].SetActive(true);
                int playerNum;
                if (nowPlayerNum + i + 1 >= hands.Count) playerNum = nowPlayerNum + i + 1 - hands.Count;
                else playerNum = nowPlayerNum + i + 1;

                otherHands[i].GetComponent<PlayerHand>().SetHand(hands[playerNum], playerNum, true);
            }

            if(hands.Count == 3)
            {
                if(nowPlayerNum == 0)
                {
                    playerHands.Add(playerHand.GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[0].GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
                }
                else if (nowPlayerNum == 1)
                {
                    playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
                    playerHands.Add(playerHand.GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[0].GetComponent<PlayerHand>());
                }
                else if (nowPlayerNum == 2)
                {
                    playerHands.Add(otherHands[0].GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
                    playerHands.Add(playerHand.GetComponent<PlayerHand>());
                }
            }
            else if(hands.Count == 4)
            {
                if (nowPlayerNum == 0)
                {
                    playerHands.Add(playerHand.GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[0].GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[2].GetComponent<PlayerHand>());
                }
                else if (nowPlayerNum == 1)
                {
                    playerHands.Add(otherHands[2].GetComponent<PlayerHand>());
                    playerHands.Add(playerHand.GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[0].GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
                }
                else if (nowPlayerNum == 2)
                {
                    playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[2].GetComponent<PlayerHand>());
                    playerHands.Add(playerHand.GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[0].GetComponent<PlayerHand>());
                }
                else if (nowPlayerNum == 3)
                {
                    playerHands.Add(otherHands[0].GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[1].GetComponent<PlayerHand>());
                    playerHands.Add(otherHands[2].GetComponent<PlayerHand>());
                    playerHands.Add(playerHand.GetComponent<PlayerHand>());
                }
            }
        }
    }

    public void SetRepCard(List<CardStruct> repCards)
    {
        for(int i = 0; i < playerHands.Count; i++)
        {
            playerHands[i].SetRepCard(repCards[i]);
        }
    }

    public void SetRepCard(CardStruct card, int playerNum)
    {
        playerHands[playerNum].SetRepCard(card);
    }

    // 덱 카드 설정
    [SerializeField]
    Deck deck;


    public void SetDeckCard(CardStruct card)
    {
        deck.SetDeckCard(card);
    }

    public void DrawCard(CardStruct card, int playerNum)
    {
        playerHands[playerNum].DrawCard(card);
    }

    public GameObject GetEnemyHand(int playerNum)
    {
        return playerHands[playerNum].gameObject;
    }
}
