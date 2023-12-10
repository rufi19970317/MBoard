using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField]
    Button FinishAnswerBtn;
    [SerializeField]
    Button FinishDefaultAnswerBtn;
    int roundNum = 0;

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

        FinishAnswerBtn.onClick.AddListener(OpenFinishAnswerPopup);
        FinishDefaultAnswerBtn.onClick.AddListener(OpenFinishDefaultAnswerPopup);
        Screen.SetResolution(2960, 1440, true);
        /*
#if(UNITY_ANDROID)
        int i_width = Screen.width;
        int i_height = Screen.height;
        Input.multiTouchEnabled = false;
        Screen.SetResolution(i_width, i_height, true);
#elif(UNITY_EDITOR)
        Screen.SetResolution(2960, 1440, true);
#endif
        */
        Application.targetFrameRate = 60;
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

    [SerializeField]
    AnswerManager answerManager;

    int playerPoint = 0;
    int enemyPoint = 0;

    // 게임 시작
    public void OnPlay(int num)
    {
        if (num == 0) return;
        roundNum++;

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

        answerManager.ResetAnswerList();
        answerManager.SetAnswer();

        nowPlayerHand = uiManager.GetPlayerHand(0).GetComponent<PlayerHand>();
        enemyManager.SetEnemy(uiManager.GetPlayerHand(1));

        playerPoint = 0;
        enemyPoint = 0;

        // 첫 번째 플레이어부터 게임 시작
        if (nowPlayerNum == nowPlayerIndex)
        {
            isMyTurn = true;
            GamePhase = Phase.Draw;
        }
    }
    List<CardStruct> RepCards = new List<CardStruct>();

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

    Phase phase = Phase.TurnEnd;

    [SerializeField]
    UI_PhasePopUp phasePopUp;
    public Phase GamePhase
    {
        get { return phase; }
        set
        {
            phase = value;
            GameManager.Instance.PlayerPareticleOff();
            switch (phase)
            {
                case Phase.Draw:
                    if(isMyTurn)
                    {
                        uiManager.SetDrawAnimActive(true);
                    }
                    else
                    {

                    }
                    break;
                case Phase.Discard:
                    if (isMyTurn)
                    {
                        uiManager.SetDrawAnimActive(false);
                    }
                    break;
            }

            if (selectCard != null) selectCard.GetComponent<Card>().OnEndDrag(null);
            if (!isResetRound) FindAnswer(nowPlayerHand);
            phasePopUp.OnPhasePopUp(GamePhase, isMyTurn);

            if (phase == Phase.Discard && isMyTurn)
            {
                if (answerManager.isFinishAnswer) FinishAnswerBtn.gameObject.SetActive(true);
                if (answerManager.isFinishDefaultAnswer) FinishDefaultAnswerBtn.gameObject.SetActive(true);
            }
            else
            {
                FinishAnswerBtn.gameObject.SetActive(false);
                FinishDefaultAnswerBtn.gameObject.SetActive(false);
            }
        }
    }

    bool isOppoCard = false;
    int drawCardPlayerNum = 9999;

    [SerializeField]
    EnemyManager enemyManager;
    public Card deckCard;

    // 내 턴에 카드 뽑기
    public void DrawCard(Card drawCard, Card.CardState cardState)
    {
        if(isMyTurn && GamePhase == Phase.Draw)
        {
            CardStruct cardInfo = drawCard.cardInfo;
            hands[nowPlayerIndex].Card.Add(cardInfo);
            uiManager.OnDraw(cardInfo, nowPlayerIndex);
            Destroy(drawCard.gameObject);
            GamePhase = Phase.Discard;

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
        if (!isMyTurn && GamePhase == Phase.Draw)
        {
            CardStruct cardInfo = drawCard.cardInfo;
            hands[nowPlayerIndex].Card.Add(cardInfo);
            uiManager.OnDraw(cardInfo, nowPlayerIndex);
            drawCard.transform.SetParent(null);
            Destroy(drawCard.gameObject);

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
            GamePhase = Phase.Discard;
        }
    }


    [SerializeField]
    DumpDeck dumpDeck;
    public bool isMyTurn = true;

    public void DiscardCard(Card selectCard)
    {
        if (GamePhase == Phase.Discard)
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
            selectCard.gameObject.transform.SetParent(null);
            hands[nowPlayerIndex].Card.Remove(selectCard.cardInfo);
            Destroy(selectCard.gameObject);
            GamePhase = Phase.TurnEnd;

            nowPlayerIndex++;
            if (nowPlayerIndex >= playerNum) nowPlayerIndex -= playerNum;

            if (isMyTurn)
            {
                isMyTurn = false;
                GamePhase = Phase.Draw;
                enemyManager.StartEnemy();
            }
            else
            {
                isMyTurn = true;
                GamePhase = Phase.Draw;

            }
        }
    }

    public void ChangeCard(Card selectCard)
    {
        if(GamePhase == Phase.Draw)
        {
            Card repCard = nowPlayerHand.GetRepCard();

            hands[nowPlayerIndex].Card.Add(repCard.cardInfo);

            if(isMyTurn)
            {
                nowPlayerHand.ChangeCard(repCard.cardInfo);
                nowPlayerHand.SetRepCard(selectCard.cardInfo);
                uiManager.SetDrawAnimActive(false);
            }
            else
            {

            }

            selectCard.gameObject.transform.SetParent(null);
            hands[nowPlayerIndex].Card.Remove(selectCard.cardInfo);
            Destroy(selectCard.gameObject);
            GamePhase = Phase.TurnEnd;

            nowPlayerIndex++;
            if (nowPlayerIndex >= playerNum) nowPlayerIndex -= playerNum;

            if (isMyTurn)
            {
                isMyTurn = false;
                GamePhase = Phase.Draw;
                enemyManager.StartEnemy();
            }
            else
            {
                isMyTurn = true;
                GamePhase = Phase.Draw;
            }
        }
    }

    public void PassHand()
    {
        nowPlayerIndex++;
        if (nowPlayerIndex == playerNum) nowPlayerIndex = 0;
    }

    bool isResetRound = false;
    public void ResetRound()
    {
        isResetRound = true;
        roundNum++;

        // 버림패 리셋
        dumpDeck.ResetDumpDeck();
        // 덱 세팅
        cardDeckManager.SetDeck();

        // 플레이어들 핸드 세팅
        SetHands();
        List<Card> cards = uiManager.SetHands(nowPlayerNum, hands);
        uiManager.SetRepCard(RepCards);

        // 덱 카드 세팅
        uiManager.SetDeckCard(cardDeckManager.DrawCard());

        answerManager.ResetAnswerList();
        answerManager.SetAnswer();

        enemyManager.SetEnemy(uiManager.GetPlayerHand(1));

        // 첫 번째 플레이어부터 게임 시작
        if (nowPlayerNum == nowPlayerIndex)
        {
            isMyTurn = true;
            GamePhase = Phase.Draw;
            answerManager.FindAnswer(cards, true);
        }
        isResetRound = false;
    }

    public void SetActiveDiscardZone(bool isActive)
    {
        if (nowPlayerIndex == nowPlayerNum && GamePhase == Phase.Discard)
        {
            if (isOppoCard)
            {
                uiManager.DiscardZone_Oppocard.SetActive(isActive);
                uiManager.DiscardZone_Oppocard.GetComponent<Image>().color = new Color(0f, 0f, 255f, 0.5f);
            }
            else
            {
                uiManager.DiscardZone_Discard.SetActive(isActive);
                uiManager.DiscardZone_Discard.GetComponent<Image>().color = new Color(0f, 0f, 255f, 0.5f);
            }
        }
    }

    public void FindAnswer(PlayerHand playerHand)
    {
        List<Card> cards = new List<Card>();

        cards.Add(playerHand.GetRepCard());
        for (int i = 0; i < playerHand.transform.childCount; i++)
        {
            if (playerHand.transform.GetChild(i).GetComponent<Card>() != null)
            {
                cards.Add(playerHand.transform.GetChild(i).GetComponent<Card>());
            }
        }

        answerManager.FindAnswer(cards, !playerHand.isOppo);
    }

    public List<Card> GetPlayerCards()
    {
        List<Card> cards = new List<Card>();

        cards.Add(nowPlayerHand.GetRepCard());
        for (int i = 0; i < nowPlayerHand.transform.childCount; i++)
        {
            if (nowPlayerHand.transform.GetChild(i).GetComponent<Card>() != null)
            {
                cards.Add(nowPlayerHand.transform.GetChild(i).GetComponent<Card>());
            }
        }

        return cards;
    }


    public GameObject selectCard = null;
    public void SetSelectCard(GameObject card)
    {
        selectCard = card;
    }

    [SerializeField]
    GameObject FinishAnswerPopUp;

    private void OpenFinishAnswerPopup()
    {
        FinishAnswerPopUp.GetComponent<UI_FinishAnswerPopUp>().SetAnswerList(answerManager.playerFinishAnswerList);
        FinishAnswerPopUp.SetActive(true);
    }

    [SerializeField]
    GameObject FinishDefaultAnswerPopUp;

    private void OpenFinishDefaultAnswerPopup()
    {
        FinishDefaultAnswerPopUp.GetComponent<UI_FinishDefaultAnswerPopUp>().SetAnswerList(answerManager.playerFinishDefaultAnswerList);
        FinishDefaultAnswerPopUp.SetActive(true);
    }

    public void FinishAnswer(PlayerAnswer playerAnswer)
    {
        FinishAnswerPopUp.SetActive(false);
        if(isMyTurn)
        {
            playerPoint += playerAnswer.point;
            uiManager.OnFinish(playerPoint, 0);
        }
        else
        {
            enemyPoint += playerAnswer.point;
            uiManager.OnFinish(enemyPoint, 1);
        }
        FinishAnswerBtn.gameObject.SetActive(false);
        FinishDefaultAnswerBtn.gameObject.SetActive(false);
        if (roundNum < 2)
        {
            ResetRound();
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        EndGamePopUp.SetActive(true);
        EndGamePopUp.GetComponent<UI_EndGame>().SetPopUp(playerPoint, enemyPoint);

    }

    public void FinishDefaultAnswer(PlayerDefaultAnswer playerDefaultAnswer)
    {
        FinishDefaultAnswerPopUp.SetActive(false);
        if (isMyTurn)
        {
            playerPoint += playerDefaultAnswer.point;

            List<Card> cards = new List<Card>();
            List<Card> newCards = new List<Card>();

            Card repCard = playerDefaultAnswer.playerMemberCards.Find(element => element.CompareCard(nowPlayerHand.GetRepCard().cardInfo.univerSity, nowPlayerHand.GetRepCard().cardInfo.num));

            if (repCard != null)
            {
                newCards.Add(nowPlayerHand.SetRepCard(cardDeckManager.DrawCard()));

                for (int i = 0; i < nowPlayerHand.transform.childCount; i++)
                {
                    if (nowPlayerHand.transform.GetChild(i).GetComponent<Card>() != null)
                    {
                        cards.Add(nowPlayerHand.transform.GetChild(i).GetComponent<Card>());
                    }
                }

                for (int i = 0; i < playerDefaultAnswer.playerMemberCards.Count; i++)
                {
                    Card card = cards.Find(element => element.CompareCard(playerDefaultAnswer.playerMemberCards[i].cardInfo.univerSity, playerDefaultAnswer.playerMemberCards[i].cardInfo.num));

                    if (card != null)
                    {
                        hands[nowPlayerIndex].Card.Remove(card.cardInfo);
                        Destroy(card.gameObject);

                        CardStruct cardInfo = cardDeckManager.DrawCard();
                        hands[nowPlayerIndex].Card.Add(cardInfo);
                        newCards.Add(uiManager.OnDraw(cardInfo, nowPlayerIndex));
                    }
                }
            }
            else
            {
                for (int i = 0; i < nowPlayerHand.transform.childCount; i++)
                {
                    if (nowPlayerHand.transform.GetChild(i).GetComponent<Card>() != null)
                    {
                        cards.Add(nowPlayerHand.transform.GetChild(i).GetComponent<Card>());
                    }
                }

                for (int i = 0; i < playerDefaultAnswer.playerMemberCards.Count; i++)
                {
                    Card card = cards.Find(element => element.CompareCard(playerDefaultAnswer.playerMemberCards[i].cardInfo.univerSity, playerDefaultAnswer.playerMemberCards[i].cardInfo.num));
                    if (card != null)
                    {
                        hands[nowPlayerIndex].Card.Remove(card.cardInfo);
                        Destroy(card.gameObject);

                        CardStruct cardInfo = cardDeckManager.DrawCard();
                        hands[nowPlayerIndex].Card.Add(cardInfo);
                        newCards.Add(uiManager.OnDraw(cardInfo, nowPlayerIndex));
                    }
                }
            }
            uiManager.OnFinish(playerPoint, 0);
            FinishAnswerBtn.gameObject.SetActive(false);
            FinishDefaultAnswerBtn.gameObject.SetActive(false);

            answerManager.FindAnswer(newCards, true);

            if (answerManager.isFinishAnswer) FinishAnswerBtn.gameObject.SetActive(true);
            if (answerManager.isFinishDefaultAnswer) FinishDefaultAnswerBtn.gameObject.SetActive(true);
        }
        else
        {
            enemyPoint += playerDefaultAnswer.point;
            uiManager.OnFinish(enemyPoint, 1);
        }
    }

    public void PlayerPareticleOff()
    {
        if (selectCard != null) return;
        List<Card> cards = GetPlayerCards();
        foreach(Card card in cards)
        {
            card.SetParticle(false);
        }
    }

    public PlayerHand nowPlayerHand;
    public bool isFinish = false;
    public GameObject EndGamePopUp;
    public bool isEnd = false;
}
