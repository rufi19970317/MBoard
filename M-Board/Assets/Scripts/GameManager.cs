using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AnswerManager;


// 개인 손패
public struct Hand
{
    public List<CardStruct> Card;

    public void Add(CardStruct card)
    {
        if (Card == null) Card = new List<CardStruct>();
        Card.Add(card);
    }

    public CardStruct Remove(int num)
    {
        CardStruct card = Card[num];
        Card.RemoveAt(num);
        return card;
    }
}


public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private CardDeckManager cardDeckManager;
    [SerializeField]
    private UIManager uiManager;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        cardDeckManager = GetComponent<CardDeckManager>();
        uiManager.StartGame();

        Screen.SetResolution(2560, 1600, true);
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public List<Hand> hands = new List<Hand>();
    private int playerNum = 0; // 플레이어 숫자
    private int nowPlayerIndex = 0; // 현재 플레이어 순서

    // 현재 플레이어
    // 네트워크마다 달라야 함
    private int nowPlayerNum = 0;

    public List<CardStruct> RepCards = new List<CardStruct> ();

    [SerializeField]
    AnswerManager answerManager;

    // 게임 시작
    public void OnPlay(int num)
    {
        if (num == 0) return;

        isEnd = false;
        // 플레이어 숫자
        playerNum = num;
        nowPlayerIndex = 0;

        // 네트워크 연결 시
        // nowPlayerNum = Network Num;

        // 버림패 리셋
        dumpDeck.ResetDumpDeck();
        // 덱 세팅
        cardDeckManager.SetDeck();

        // 플레이어들 핸드 세팅
        SetHands();
        uiManager.OnPlay(nowPlayerNum, hands);
        uiManager.SetRepCard(RepCards);

        // 덱 카드 세팅
        uiManager.SetDeckCard(cardDeckManager.DrawCard());

        answerManager.SetAnswer();

        nowPlayerHand = uiManager.GetPlayerHand(0).GetComponent<PlayerHand>();
        enemyManager.SetEnemy(uiManager.GetPlayerHand(1));

        // 첫 번째 플레이어부터 게임 시작
        if (nowPlayerNum == nowPlayerIndex)
        {
            phase = Phase.Draw;
            isMyTurn = true;
        }
    }

    // 플레이어들 핸드 세팅
    private void SetHands()
    {
        hands = new List<Hand>();
        for (int i = 0; i < playerNum; i++)
        {
            Hand newHand = new Hand();
            for (int j = 0; j < 4; j++)
            {
                CardStruct drawCard = cardDeckManager.DrawCard();
                newHand.Add(drawCard);

            }
            hands.Add(newHand);
        }

        RepCards = new List<CardStruct>();
        for(int i = 0; i < playerNum; i++)
        {
            CardStruct drawCard = cardDeckManager.DrawCard();
            RepCards.Add(drawCard);
        }
    }

    public enum Phase
    {
        Draw,
        Discard,
        TurnEnd,
        GameEnd
    }

    public Phase phase = Phase.TurnEnd;

    bool isOppoCard = false;
    int drawCardPlayerNum = 9999;

    [SerializeField]
    EnemyManager enemyManager;
    public Card deckCard;

    // 내 턴에 카드 뽑기
    public void DrawCard(Card drawCard, Card.CardState cardState)
    {
        if(isMyTurn && phase == Phase.Draw)
        {
            CardStruct cardInfo = drawCard.cardInfo;
            hands[nowPlayerIndex].Card.Add(cardInfo);
            uiManager.OnDraw(cardInfo, nowPlayerIndex);
            Destroy(drawCard.gameObject);
            phase = Phase.Discard;

            if (cardState == Card.CardState.DeckCard)
            {
                uiManager.SetDeckCard(cardDeckManager.DrawCard());
                isOppoCard = false;
            }
            else if(cardState != Card.CardState.DeckCard)
            {
                isOppoCard = true;
                drawCardPlayerNum = drawCard.hand.playerNum;
            }
        }
    }
    public void EnemyDrawCard(Card drawCard, Card.CardState cardState)
    {
        if (!isMyTurn && phase == Phase.Draw)
        {
            CardStruct cardInfo = drawCard.cardInfo;
            hands[nowPlayerIndex].Card.Add(cardInfo);
            uiManager.OnDraw(cardInfo, nowPlayerIndex);
            Destroy(drawCard.gameObject);
            phase = Phase.Discard;

            if (cardState == Card.CardState.DeckCard)
            {
                uiManager.SetDeckCard(cardDeckManager.DrawCard());
                isOppoCard = false;
            }
            else if (cardState != Card.CardState.DeckCard)
            {
                isOppoCard = true;
                drawCardPlayerNum = drawCard.hand.playerNum;
            }
        }
    }


    [SerializeField]
    DumpDeck dumpDeck;
    public bool isMyTurn = true;

    public void DiscardCard(Card selectCard)
    {
        if (phase == Phase.Discard)
        {
            SetActiveDiscardZone(false);
            if (!isOppoCard)
            {
                dumpDeck.CardDump(selectCard.cardInfo);
                if(!isMyTurn)
                {
                    List<Card> cards = new List<Card>();
                    cards.Add(nowPlayerHand.GetRepCard());

                    for (int i = 0; i < nowPlayerHand.transform.childCount; i++)
                    {
                        cards.Add(nowPlayerHand.transform.GetChild(i).GetComponent<Card>());
                    }
                    cards.Add(dumpDeck.DumpCard);

                    answerManager.FindAnswer(cards);
                }
            }
            else
            {
                if(drawCardPlayerNum <= playerNum)
                {
                    hands[drawCardPlayerNum].Card.Add(selectCard.cardInfo);
                    uiManager.OnDraw(selectCard.cardInfo, drawCardPlayerNum);
                    drawCardPlayerNum = 9999;
                }
            }
            hands[nowPlayerIndex].Card.Remove(selectCard.cardInfo);
            Destroy(selectCard.gameObject);
            phase = Phase.TurnEnd;

            nowPlayerIndex++;
            if (nowPlayerIndex >= playerNum) nowPlayerIndex -= playerNum;

            if (isMyTurn)
            {
                isMyTurn = false;
                phase = Phase.Draw;
                enemyManager.StartEnemy();
            }
            else
            {
                isMyTurn = true;
                phase = Phase.Draw;

            }
        }
    }

    public void PassHand()
    {
        nowPlayerIndex++;
        if (nowPlayerIndex == playerNum) nowPlayerIndex = 0;
    }

    public void ResetRound()
    {
        nowPlayerIndex = 0;

        dumpDeck.ResetDumpDeck();
        cardDeckManager.SetDeck();
        SetHands();
    }

    public void SetActiveDiscardZone(bool isActive)
    {
        if (nowPlayerIndex == nowPlayerNum && phase == Phase.Discard)
        {
            uiManager.DiscardZone.SetActive(isActive);
            uiManager.DiscardZone.GetComponent<Image>().color = new Color(255f, 0f, 0f, 0.1f);
        }
    }

    public void FindAnswer(PlayerHand playerHand)
    {
        List<Card> cards = new List<Card>();

        cards.Add(playerHand.GetRepCard());
        for (int i = 0; i < playerHand.transform.childCount; i++)
        {
            cards.Add(playerHand.transform.GetChild(i).GetComponent<Card>());
        }

        answerManager.FindAnswer(cards);

        if(playerHand.playerNum == nowPlayerNum)
        {

        }
    }


    public GameObject selectCard = null;
    public void SetSelectCard(GameObject card)
    {
        selectCard = card;
    }

    [SerializeField]
    GameObject AnswerCardPopUp;
    
    public void OpenAnswerCardPopup(AnswerCard answerCard, List<Card> result)
    {
        if (selectCard != null) return;
        if (!AnswerCardPopUp.activeSelf)
        {
            AnswerCardPopUp.SetActive(true);
            GameObject grid = AnswerCardPopUp.transform.GetChild(2).gameObject;
            grid.SetActive(true);
            foreach(Transform child in grid.transform)
            {
                child.gameObject.SetActive(false);
            }

            for(int i = 0; i < result.Count; i++)
            {
                CardStruct cardStruct = result[i].cardInfo;
                grid.transform.GetChild(i).gameObject.SetActive(true);
                grid.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Students/Student_" + cardStruct.univerSity.ToString() + "_" + cardStruct.num.ToString());
            }
        }
    }

    public void EndGamePlayerWin(AnswerCard answer, List<Card> result)
    {
        if(isMyTurn)
        {
            EndGamePopUp.SetActive(true);
            EndGamePopUp.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = "You Win!";


            GameObject grid = EndGamePopUp.transform.GetChild(3).gameObject;
            foreach (Transform child in grid.transform)
            {
                child.gameObject.SetActive(false);
            }

            grid.transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 0; i < result.Count; i++)
            {
                CardStruct cardStruct = result[i].cardInfo;
                grid.transform.GetChild(i + 1).gameObject.SetActive(true);
                grid.transform.GetChild(i + 1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Students/Student_" + cardStruct.univerSity.ToString() + "_" + cardStruct.num.ToString());
            }
        }
    }
    public void EndGame(AnswerCard answer, List<Card> result)
    {
        if (!isMyTurn && phase == Phase.Draw)
        {
            enemyManager.StopAllCoroutines();
            EndGamePopUp.SetActive(true);
            EndGamePopUp.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = "Enemy Win...";

            GameObject grid = EndGamePopUp.transform.GetChild(3).gameObject;
            foreach (Transform child in grid.transform)
            {
                child.gameObject.SetActive(false);
            }

            grid.transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 0; i < result.Count; i++)
            {
                CardStruct cardStruct = result[i].cardInfo;
                grid.transform.GetChild(i + 1).gameObject.SetActive(true);
                grid.transform.GetChild(i + 1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Students/Student_" + cardStruct.univerSity.ToString() + "_" + cardStruct.num.ToString());
            }
        }
        else
        {
            isEnd = true;
            foreach(Card card in result)
            {
                card.SetParticle(true);
            }
            answer.SetParticle(true);
        }
    }

    public PlayerHand nowPlayerHand;
    public bool isFinish = false;
    public GameObject EndGamePopUp;
    public bool isEnd = false;
}
