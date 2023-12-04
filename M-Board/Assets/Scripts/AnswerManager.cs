using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using static AnswerManager;

public class DataManager
{
    // 데이터 저장용 딕셔너리
    //public Dictionary<int, Data.Spawn> SpawnDict { get; private set; } = new Dictionary<int, Data.Spawn>();

    // 딕셔너리 생성
    public void Init()
    {
        //SpawnDict = LoadJson<Data.SpawnData, int, Data.Spawn>("SpawnData").MakeDict();
    }

    // Json 데이터 불러오기
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}

public class AnswerManager : MonoBehaviour
{
    [SerializeField]
    UI_AnswerList ui_AnswerList;

    #region Leader Answer
    public struct LeaderAnswerSet
    {
        public string name;
        public List<CardDeckManager.UniverSity> university;
        public int point;

        public LeaderAnswerSet(string name, List<CardDeckManager.UniverSity> university, int point)
        {
            this.name = name;
            this.university = university;
            this.point = point;
        }
    }

    public class LeaderAnswer
    {
        public List<LeaderAnswerSet> leaderAnswerSet = new List<LeaderAnswerSet>();

        public LeaderAnswer()
        {
            SetLeaderAnswer();
        }

        void SetLeaderAnswer()
        {
            leaderAnswerSet = new List<LeaderAnswerSet>();
            //1
            leaderAnswerSet.Add(new LeaderAnswerSet("파란학기제 (개발)",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Software, CardDeckManager.UniverSity.Engineering }, 1));
            //2
            leaderAnswerSet.Add(new LeaderAnswerSet("파란학기제 (출판)",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Joker, CardDeckManager.UniverSity.Humanities  }, 2));
            //3
            leaderAnswerSet.Add(new LeaderAnswerSet("파란학기제 (의료)",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Medical, CardDeckManager.UniverSity.Pharmacy }, 1));
            //4
            leaderAnswerSet.Add(new LeaderAnswerSet("파란학기제 (창업)",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Software, CardDeckManager.UniverSity.Business }, 1));
            //5
            leaderAnswerSet.Add(new LeaderAnswerSet("파란학기제 (실험)",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.NaturalScience, CardDeckManager.UniverSity.NaturalScience }, 3));
            //6
            leaderAnswerSet.Add(new LeaderAnswerSet("대동제 무대",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Joker, CardDeckManager.UniverSity.Joker }, 4));
            //7
            leaderAnswerSet.Add(new LeaderAnswerSet("축구부 프론트",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.IC, CardDeckManager.UniverSity.Nursing }, 1));
            //8
            leaderAnswerSet.Add(new LeaderAnswerSet("아주팝스",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.IC, CardDeckManager.UniverSity.Pharmacy }, 1));
            //9
            leaderAnswerSet.Add(new LeaderAnswerSet("소프트콘",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Software, CardDeckManager.UniverSity.Software }, 3));
            //10
            leaderAnswerSet.Add(new LeaderAnswerSet("캡스톤디자인",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Humanities, CardDeckManager.UniverSity.SocialSciences }, 1));
            //11
            leaderAnswerSet.Add(new LeaderAnswerSet("아주 Debate",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Business, CardDeckManager.UniverSity.Humanities }, 1));
            //12
            leaderAnswerSet.Add(new LeaderAnswerSet("아주대 학습공동체",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Joker, CardDeckManager.UniverSity.SocialSciences  }, 2));
            //13
            leaderAnswerSet.Add(new LeaderAnswerSet("문학과 철학",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Medical, CardDeckManager.UniverSity.Nursing }, 2));
            //14
            leaderAnswerSet.Add(new LeaderAnswerSet("문학과 예술",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.NaturalScience, CardDeckManager.UniverSity.Engineering }, 1));
            //15
            leaderAnswerSet.Add(new LeaderAnswerSet("인간과 사회",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Pharmacy, CardDeckManager.UniverSity.Nursing }, 1));
            //16
            leaderAnswerSet.Add(new LeaderAnswerSet("자연과 과학",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.SocialSciences, CardDeckManager.UniverSity.Business }, 1));
            //17
            leaderAnswerSet.Add(new LeaderAnswerSet("아주희망",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Humanities, CardDeckManager.UniverSity.Engineering }, 1));
            //18
            leaderAnswerSet.Add(new LeaderAnswerSet("창업 경진대회",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Engineering, CardDeckManager.UniverSity.IC }, 1));
            //19
            leaderAnswerSet.Add(new LeaderAnswerSet("전공기부단",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Medical, CardDeckManager.UniverSity.IC }, 1));
            //20
            leaderAnswerSet.Add(new LeaderAnswerSet("GAIA",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.SocialSciences, CardDeckManager.UniverSity.Business }, 1));

            leaderAnswerSet.Shuffle();
        }

        public List<LeaderAnswerSet> GetRanLeaderAnswer()
        {
            List<LeaderAnswerSet> answerSet = new List<LeaderAnswerSet>();
            answerSet.Add(DrawLeaderCard());
            answerSet.Add(DrawLeaderCard());
            answerSet.Add(DrawLeaderCard());
            answerSet.Add(DrawLeaderCard());

            return answerSet;
        }

        private LeaderAnswerSet DrawLeaderCard()
        {
            LeaderAnswerSet nowCard = leaderAnswerSet[0];
            leaderAnswerSet.RemoveAt(0);
            return nowCard;
        }
    }
    #endregion

    #region Member Answer
    public struct MemberAnswerSet
    {
        public int num;
        public Nullable<CardDeckManager.UniverSity> university;
        public List<int> grade;
        public int point;

        public MemberAnswerSet(int num, int point, Nullable<CardDeckManager.UniverSity> university)
        {
            this.num = num;
            this.university = university;
            this.grade = null;
            this.point = point;
        }
        public MemberAnswerSet(int num, int point, List<int> grade = null)
        {
            this.num = num;
            this.university = null;
            this.grade = grade;
            this.point = point;
        }
    }

    public static class MemberAnswer
    {
        private static List<MemberAnswerSet> memberAnswerSet = new List<MemberAnswerSet>();

        static MemberAnswer()
        {
            foreach(CardDeckManager.UniverSity university in Enum.GetValues(typeof(CardDeckManager.UniverSity)))
            {
                memberAnswerSet.Add(new MemberAnswerSet(university == CardDeckManager.UniverSity.Joker ? 4:0, university == CardDeckManager.UniverSity.Joker?30:20, university));
            }

            for(int i = 1; i <= 5; i++)
            {
                memberAnswerSet.Add(new MemberAnswerSet(1, i >= 4?20:15, new List<int>() { i, i, i }));
            }

            memberAnswerSet.Add(new MemberAnswerSet(2, 15, new List<int>() { 5, 4, 3 }));
            memberAnswerSet.Add(new MemberAnswerSet(2, 10, new List<int>() { 4, 3, 2 }));
            memberAnswerSet.Add(new MemberAnswerSet(2, 5, new List<int>() { 3, 2, 1 }));
        }

        public static List<MemberAnswerSet> GetMemberAnswer()
        {
            return memberAnswerSet;
        }
    }
    #endregion

    List<LeaderAnswerSet> NowLeaderAnswerSet = new List<LeaderAnswerSet>();
    LeaderAnswer LeaderAnswerDeck = new LeaderAnswer();


    public void ResetAnswerList()
    {
        NowLeaderAnswerSet = new List<LeaderAnswerSet>();
    }

    public void SetAnswer()
    {
        SetLeaders();
        SetAnswerOnBoard();
    }

    void SetLeaders()
    {
        NowLeaderAnswerSet = LeaderAnswerDeck.GetRanLeaderAnswer();
    }

    private void SetAnswerOnBoard()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AnswerCard>().SetAnswer(NowLeaderAnswerSet[i]);
        }
    }

    public struct PlayerAnswer
    {
        public string playerLeaderName;
        public List<Card> playerLeaderCards;
        public int playerMemberNum;
        public List<Card> playerMemberCards;
        public int point;

        public PlayerAnswer(string playerLeaderName, List<Card> playerLeaderCards, int playerMemberNum, List<Card>playerMemberCards, int point)
        {
            this.playerLeaderName = playerLeaderName;
            this.playerLeaderCards = playerLeaderCards;
            this.playerMemberNum = playerMemberNum;
            this.playerMemberCards = playerMemberCards;
            this.point = point;
        }

        public int GetPlayerAnswerCardCount()
        {
            return playerLeaderCards.Count + playerMemberCards.Count;
        }
    }

    private List<Card> FindMemberAnswer(List<Card> leftCard, int num)
    {
        List<Card> memberCards = new List<Card>();
        List<Card> LEFTCARD = new List<Card>();
        MemberAnswerSet memberAnswer = MemberAnswer.GetMemberAnswer()[num];

        for(int i = 0; i < leftCard.Count; i++)
        {
            LEFTCARD.Add(leftCard[i]);
        }

        if (memberAnswer.university != null)
        {
            memberCards = leftCard.FindAll(element => element.cardInfo.univerSity == memberAnswer.university);
        }
        else
        {
            for (int i = 0; i < memberAnswer.grade.Count; i++)
            {
                Card card = LEFTCARD.Find(element => element.cardInfo.num == memberAnswer.grade[i]);
                if (card != null)
                {
                    memberCards.Add(card);
                    LEFTCARD.Remove(card);
                }
            }
        }

        return memberCards;
    }

    public void FindAnswer(List<Card> playerCards, bool isPlayer)
    {
        #region Leader + Member Answer List
        // 리더카드 먼저 확인
        // 조커카드가 있는 경우, 조커카드 먼저 계산
        // 조커카드가 없는 경우, (5, 5) (5, 4) (4, 5) (4, 4)
        List<PlayerAnswer> AllPlayerAnswerCardsList = new List<PlayerAnswer>();

        foreach (LeaderAnswerSet leaderAnswer in NowLeaderAnswerSet)
        {
            List<CardDeckManager.UniverSity> univerSitiesJoker = leaderAnswer.university.FindAll(element => element == CardDeckManager.UniverSity.Joker);
            List<Card> playerCardsList = new List<Card>();
            for(int copy = 0; copy < playerCards.Count; copy++)
            {
                playerCardsList.Add(playerCards[copy]);
            }

            if(univerSitiesJoker.Count == 0)
            {
                List<Card> playerLeaderCardsList = new List<Card>();
                List<Card> playerMemberCardsList = new List<Card>();

                if (leaderAnswer.university[0] != leaderAnswer.university[1])
                {
                    List<Card> leaderCards = new List<Card>();
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 4)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 5)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[1], 4)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[1], 5)));

                    
                    for(int leader1 = 0; leader1 <= 1; leader1++)
                    {
                        for(int leader2 = 2; leader2 <= 3; leader2++)
                        {
                            if (leaderCards[leader1] != null && leaderCards[leader2] != null)
                            {
                                playerCardsList = new List<Card>();
                                for (int copy = 0; copy < playerCards.Count; copy++)
                                {
                                    playerCardsList.Add(playerCards[copy]);
                                }

                                playerLeaderCardsList = new List<Card>();
                                playerMemberCardsList = new List<Card>();

                                playerCardsList.Remove(leaderCards[leader1]);
                                playerLeaderCardsList.Add(leaderCards[leader1]);
                                playerCardsList.Remove(leaderCards[leader2]);
                                playerLeaderCardsList.Add(leaderCards[leader2]);

                                for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                {
                                    List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                    if (memberCardList.Count >= 2)
                                    {
                                        PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                        AllPlayerAnswerCardsList.Add(playerAnswer);
                                    }
                                }
                            }
                            else if (leaderCards[leader1] != null || leaderCards[leader2] != null)
                            {
                                playerCardsList = new List<Card>();
                                for (int copy = 0; copy < playerCards.Count; copy++)
                                {
                                    playerCardsList.Add(playerCards[copy]);
                                }
                                playerLeaderCardsList = new List<Card>();
                                playerMemberCardsList = new List<Card>();

                                if (leaderCards[leader1] != null)
                                {
                                    if(leader2 == 2)
                                    {
                                        playerCardsList.Remove(leaderCards[leader1]);
                                        playerLeaderCardsList.Add(leaderCards[leader1]);
                                        for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                        {
                                            List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                            if (memberCardList.Count == 3)
                                            {
                                                PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                                AllPlayerAnswerCardsList.Add(playerAnswer);
                                            }
                                        }
                                    }
                                }
                                else if (leaderCards[leader2] != null)
                                {
                                    if (leader1 == 0)
                                    {
                                        playerCardsList.Remove(leaderCards[leader2]);
                                        playerLeaderCardsList.Add(leaderCards[leader2]);
                                        for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                        {
                                            List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                            if (memberCardList.Count == 3)
                                            {
                                                PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                                AllPlayerAnswerCardsList.Add(playerAnswer);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Card card_01 = playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 4));
                    Card card_02 = playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[1], 5));

                    if (card_01 != null && card_02 != null)
                    {
                        playerCardsList.Remove(card_01);
                        playerLeaderCardsList.Add(card_01);
                        playerCardsList.Remove(card_02);
                        playerLeaderCardsList.Add(card_02);

                        for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                        {
                            List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                            if (memberCardList.Count >= 2)
                            {
                                PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                AllPlayerAnswerCardsList.Add(playerAnswer);
                            }
                        }
                    }
                    else if (card_01 != null || card_02 != null)
                    {
                        playerCardsList = new List<Card>();
                        for (int copy = 0; copy < playerCards.Count; copy++)
                        {
                            playerCardsList.Add(playerCards[copy]);
                        }
                        playerLeaderCardsList = new List<Card>();
                        playerMemberCardsList = new List<Card>();

                        if (card_01 != null)
                        {
                            playerCardsList.Remove(card_01);
                            playerLeaderCardsList.Add(card_01);
                        }
                        if (card_02 != null)
                        {
                            playerCardsList.Remove(card_02);
                            playerLeaderCardsList.Add(card_02);
                        }

                        for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                        {
                            List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                            if (memberCardList.Count == 3)
                            {
                                PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                AllPlayerAnswerCardsList.Add(playerAnswer);
                            }
                        }
                    }
                }
            }
            else if(univerSitiesJoker.Count >= 1)
            {
                List<Card> playerLeaderCardsList = new List<Card>();
                List<Card> playerMemberCardsList = new List<Card>();

                if (leaderAnswer.university[0] != leaderAnswer.university[1])
                {
                    List<Card> leaderCards = new List<Card>();
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 10)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 11)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 12)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[1], 4)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[1], 5)));

                    for (int leader1 = 0; leader1 <= 2; leader1++)
                    {
                        for (int leader2 = 3; leader2 <= 4; leader2++)
                        {
                            if (leaderCards[leader1] != null && leaderCards[leader2] != null)
                            {
                                playerCardsList = new List<Card>();
                                for (int copy = 0; copy < playerCards.Count; copy++)
                                {
                                    playerCardsList.Add(playerCards[copy]);
                                }
                                playerLeaderCardsList = new List<Card>();
                                playerMemberCardsList = new List<Card>();

                                playerCardsList.Remove(leaderCards[leader1]);
                                playerLeaderCardsList.Add(leaderCards[leader1]);
                                playerCardsList.Remove(leaderCards[leader2]);
                                playerLeaderCardsList.Add(leaderCards[leader2]);

                                for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                {
                                    List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                    if (memberCardList.Count >= 2)
                                    {
                                        PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                        AllPlayerAnswerCardsList.Add(playerAnswer);
                                    }
                                }
                            }
                            else if (leaderCards[leader1] != null || leaderCards[leader2] != null)
                            {
                                playerCardsList = new List<Card>();
                                for (int copy = 0; copy < playerCards.Count; copy++)
                                {
                                    playerCardsList.Add(playerCards[copy]);
                                }
                                playerLeaderCardsList = new List<Card>();
                                playerMemberCardsList = new List<Card>();

                                if (leaderCards[leader1] != null)
                                {
                                    if (leader2 == 3)
                                    {
                                        playerCardsList.Remove(leaderCards[leader1]);
                                        playerLeaderCardsList.Add(leaderCards[leader1]);

                                        for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                        {
                                            List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                            if (memberCardList.Count == 3)
                                            {
                                                PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                                AllPlayerAnswerCardsList.Add(playerAnswer);
                                            }
                                        }
                                    }
                                }
                                else if (leaderCards[leader2] != null)
                                {
                                    if (leader1 == 0)
                                    {
                                        playerCardsList.Remove(leaderCards[leader2]);
                                        playerLeaderCardsList.Add(leaderCards[leader2]);

                                        for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                        {
                                            List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                            if (memberCardList.Count == 3)
                                            {
                                                PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                                AllPlayerAnswerCardsList.Add(playerAnswer);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    List<Card> leaderCards = new List<Card>();
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 10)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 11)));
                    leaderCards.Add(playerCardsList.Find(element => element.CompareCard(leaderAnswer.university[0], 12)));

                    for (int leader1 = 0; leader1 <= 1; leader1++)
                    {
                        for (int leader2 = leader1 + 1; leader2 <= 2; leader2++)
                        {
                            if (leaderCards[leader1] != null && leaderCards[leader2] != null)
                            {
                                playerCardsList = new List<Card>();
                                for (int copy = 0; copy < playerCards.Count; copy++)
                                {
                                    playerCardsList.Add(playerCards[copy]);
                                }
                                playerLeaderCardsList = new List<Card>();
                                playerMemberCardsList = new List<Card>();

                                playerCardsList.Remove(leaderCards[leader1]);
                                playerLeaderCardsList.Add(leaderCards[leader1]);
                                playerCardsList.Remove(leaderCards[leader2]);
                                playerLeaderCardsList.Add(leaderCards[leader2]);

                                for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                {
                                    List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                    if (memberCardList.Count >= 2)
                                    {
                                        PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                        AllPlayerAnswerCardsList.Add(playerAnswer);
                                    }
                                }
                            }
                            else if (leaderCards[leader1] != null || leaderCards[leader2] != null)
                            {
                                playerCardsList = new List<Card>();
                                for (int copy = 0; copy < playerCards.Count; copy++)
                                {
                                    playerCardsList.Add(playerCards[copy]);
                                }
                                playerLeaderCardsList = new List<Card>();
                                playerMemberCardsList = new List<Card>();

                                if (leaderCards[leader1] != null)
                                {
                                    if (leader2 == 1)
                                    {
                                        playerCardsList.Remove(leaderCards[leader1]);
                                        playerLeaderCardsList.Add(leaderCards[leader1]);

                                        for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                        {
                                            List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                            if (memberCardList.Count == 3)
                                            {
                                                PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                                AllPlayerAnswerCardsList.Add(playerAnswer);
                                            }
                                        }
                                    }
                                }
                                else if (leaderCards[leader2] != null)
                                {
                                    if (leader1 == 0)
                                    {
                                        playerCardsList.Remove(leaderCards[leader2]);
                                        playerLeaderCardsList.Add(leaderCards[leader2]);

                                        for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
                                        {
                                            List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                                            if (memberCardList.Count == 3)
                                            {
                                                PlayerAnswer playerAnswer = new PlayerAnswer(leaderAnswer.name, playerLeaderCardsList, i, memberCardList, leaderAnswer.point * MemberAnswer.GetMemberAnswer()[i].point);
                                                AllPlayerAnswerCardsList.Add(playerAnswer);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (AllPlayerAnswerCardsList.Count == 0)
        {
            List<Card> playerLeaderCardsList = new List<Card>();
            List<Card> playerMemberCardsList = new List<Card>();

            for (int i = 0; i < MemberAnswer.GetMemberAnswer().Count; i++)
            {
                List<Card> playerCardsList = new List<Card>();
                for (int copy = 0; copy < playerCards.Count; copy++)
                {
                    playerCardsList.Add(playerCards[copy]);
                }

                List<Card> memberCardList = FindMemberAnswer(playerCardsList, i);
                if (memberCardList.Count == 3)
                {
                    PlayerAnswer playerAnswer = new PlayerAnswer("", playerLeaderCardsList, i, memberCardList, 1 * MemberAnswer.GetMemberAnswer()[i].point);
                    AllPlayerAnswerCardsList.Add(playerAnswer);
                }
            }
        }
        Debug.Log(AllPlayerAnswerCardsList.Count);
        if (isPlayer)
        {
            ui_AnswerList.SetAnswerList(AllPlayerAnswerCardsList);
        }
        #endregion

        #region Default Answer List
        // 전부 같은 등급일 경우
        bool isDefaultAnswer_SameGrade = false;
        int answerNum = 0;

        for(int i = 1; i <= 5; i++)
        {
            int num = 0;
            for(int j = 0; j < playerCards.Count; j++)
            {
                if (playerCards[j].cardInfo.num == i)
                {
                    num++;
                }
            }

            if (num >= 5)
            {
                isDefaultAnswer_SameGrade = true;
                answerNum = i;
                break;
            }
        }
        // 전부 같은 대학일 경우
        bool isDefaultAnswer_SameUniversity = false;
        CardDeckManager.UniverSity answerUniversity;
        for(int i = 0; i < System.Enum.GetValues(typeof(CardDeckManager.UniverSity)).Length - 1; i++)
        {
            int num = 0;
            for(int j = 0; j < playerCards.Count; j++)
            {
                if (playerCards[j].cardInfo.univerSity == (CardDeckManager.UniverSity)i)
                {
                    num++;
                }
            }

            if(num >= 5)
            {
                isDefaultAnswer_SameUniversity = true;
                answerUniversity = (CardDeckManager.UniverSity)i;
                break;
            }
        }

        // 1, 2, 3, 4, 5
        bool isDefaultAnswer_GradeStair = false;
        for(int i = 1; i <= 5; i++)
        {
            bool isSame = false;
            for(int j = 0; j < playerCards.Count; j++)
            {
                if (playerCards[j].cardInfo.num == i)
                {
                    isSame = true;
                    break;
                }
            }

            if (!isSame)
                break;

            if (i == 5)
                isDefaultAnswer_GradeStair = true;
        }
        #endregion
    }

    public void ConfirmAnswer()
    {

    }
}
