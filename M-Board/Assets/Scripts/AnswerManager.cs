using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


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
    // 고정
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
            //1
            leaderAnswerSet.Add(new LeaderAnswerSet("파란학기제 (개발)",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Software, CardDeckManager.UniverSity.Engineering }, 1));
            //2
            leaderAnswerSet.Add(new LeaderAnswerSet("파란학기제 (출판)",
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.Humanities, CardDeckManager.UniverSity.Joker }, 2));
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
                new List<CardDeckManager.UniverSity>() { CardDeckManager.UniverSity.SocialSciences, CardDeckManager.UniverSity.Joker }, 2));
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

            return leaderAnswerSet;
        }
    }

    // 고정
    public class MemberAnswer
    {
        public List<CardDeckManager.UniverSity> university = new List<CardDeckManager.UniverSity>();
        public List<int> grade = new List<int>();

        public MemberAnswer()
        {
            
        }

        public void SetMemberAnswer()
        {

        }
    }

    // 유동
    public class InGameAnswer
    {
        public List<LeaderAnswer> leaders = new List<LeaderAnswer>();
        public List<MemberAnswer> members = new List<MemberAnswer>();

        public InGameAnswer(List<LeaderAnswer> leaders, List<MemberAnswer> members)
        {
            this.leaders = leaders;
            this.members = members;
        }
    }

    //
    void SetLeaders()
    {

    }

    List<InGameAnswer> NowAnswerPaper = new List<InGameAnswer>();

    public void SetAnswer()
    {
        /*
        NowAnswerPaper = new List<Answer>();

        // 전부 리셋
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        System.Random random = new System.Random();

        List<int> list = new List<int>();
        for (int i = 0; i < 20; i++)
        {
            list.Add(i);
        }

        List<int> answerNum = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int a = random.Next(0, 20 - i);
            answerNum.Add(list[a]);
            list.RemoveAt(a);
        }

        for(int i = 0; i < answerNum.Count; i++)
        {
            NowAnswerPaper.Add(answerPaper[answerNum[i]]);
        }

        for(int i = 0; i < NowAnswerPaper.Count; i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/AnswerCard"), transform);

            go.GetComponent<AnswerCard>().SetAnswer(NowAnswerPaper[i]);
        }
        */
    }

    public void FindAnswer(List<Card> playerCards)
    {
        /*
        int num = 0;
        foreach (Answer answer in NowAnswerPaper)
        {
            List<Card> sortCards = (from cards in playerCards
                                    orderby cards.cardInfo.num
                                    select cards).ToList();

            sortCards = (from cards in sortCards
                         orderby cards.cardInfo.univerSity
                         select cards).ToList();

            List<Card> result = new List<Card>();
            if(answer.university.Count > 0)
            {
                for (int i = 0; i < answer.university.Count; i++)
                {
                    for(int j = 0; j < sortCards.Count; j++)
                    {
                        if (sortCards[j].cardInfo.univerSity == answer.university[i])
                        {
                            result.Add(sortCards[j]);
                            sortCards.RemoveAt(j);
                            break;
                        }
                    }
                }
            }

            if (answer.grades.Count > 0)
            {
                List<Grades> sortGrades = (from grade in answer.grades
                                        orderby grade.grade
                                        select grade).ToList();


                for(int i = 0; i <  sortGrades.Count; i++)
                {
                    if (sortGrades[i].compare == Grades.Compare.Equal)
                    {
                        for (int j = 0; j < sortCards.Count; j++)
                        {
                            if (sortCards[j].cardInfo.num == sortGrades[i].grade)
                            {
                                result.Add(sortCards[j]);
                                sortCards.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < sortGrades.Count; i++)
                {
                    if (sortGrades[i].compare == Grades.Compare.Up)
                    {
                        for (int j = 0; j < sortCards.Count; j++)
                        {
                            if (sortCards[j].cardInfo.num >= sortGrades[i].grade)
                            {
                                result.Add(sortCards[j]);
                                sortCards.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }
            }

            if (result.Count >= 5)
            {
                transform.GetChild(num).GetComponent<AnswerCard>().SetResult(result);
                GameManager.Instance.EndGame(transform.GetChild(num).GetComponent<AnswerCard>(), result);
            }
            num++;
        }
        */

        #region Default Answer List
        // 전부 같은 등급일 경우
        bool isDefaultAnswer_SameGrade = false;
        int answerNum = 0;

        for(int i = 1; i <= 5; i++)
        {
            int num = 0;
            for(int j = 0; i < playerCards.Count; j++)
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
