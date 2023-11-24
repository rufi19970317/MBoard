using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AnswerManager : MonoBehaviour
{
    public class Answer
    {
        public List<CardDeckManager.UniverSity> university = new List<CardDeckManager.UniverSity>();
        public List<Grades> grades = new List<Grades>();

        public Answer(List<CardDeckManager.UniverSity> university, List<Grades> grades)
        {
            this.university = university;
            this.grades = grades;
        }
    }

    public class Grades
    {
        public enum Compare
        {
            Up,
            Equal,
            Down
        }

        public Compare compare;
        public int grade;

        public Grades(Compare compare, int grade)
        {
            this.compare = compare;
            this.grade = grade;
        }
    }

    List<Answer> answerPaper = new List<Answer>();
    #region 답지
    public void SetAllAnswer()
    {
        answerPaper.Clear();
        #region 1번
        List<CardDeckManager.UniverSity> university = new List<CardDeckManager.UniverSity>();
        List<Grades> grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Engineering);
        university.Add(CardDeckManager.UniverSity.SocialSciences);
        university.Add(CardDeckManager.UniverSity.Software);
        university.Add(CardDeckManager.UniverSity.Business);
        grades.Add(new Grades(Grades.Compare.Up, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 2번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Humanities);
        university.Add(CardDeckManager.UniverSity.SocialSciences);
        university.Add(CardDeckManager.UniverSity.Software);
        university.Add(CardDeckManager.UniverSity.Business);
        grades.Add(new Grades(Grades.Compare.Up, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 3번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Medical);
        university.Add(CardDeckManager.UniverSity.Nursing);
        university.Add(CardDeckManager.UniverSity.Nursing);
        university.Add(CardDeckManager.UniverSity.Pharmacy);
        grades.Add(new Grades(Grades.Compare.Equal, 5));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 4번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Engineering);
        university.Add(CardDeckManager.UniverSity.Engineering);
        university.Add(CardDeckManager.UniverSity.NaturalScience);
        university.Add(CardDeckManager.UniverSity.NaturalScience);
        grades.Add(new Grades(Grades.Compare.Up, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 5번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Humanities);
        university.Add(CardDeckManager.UniverSity.Humanities);
        university.Add(CardDeckManager.UniverSity.SocialSciences);
        university.Add(CardDeckManager.UniverSity.Business);
        grades.Add(new Grades(Grades.Compare.Up, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 6번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 2));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 7번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Nursing);
        university.Add(CardDeckManager.UniverSity.Engineering);
        university.Add(CardDeckManager.UniverSity.Humanities);
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 8번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Pharmacy);
        university.Add(CardDeckManager.UniverSity.Medical);
        university.Add(CardDeckManager.UniverSity.IC);
        university.Add(CardDeckManager.UniverSity.NaturalScience);
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 9번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Engineering);
        university.Add(CardDeckManager.UniverSity.Engineering);
        university.Add(CardDeckManager.UniverSity.IC);
        university.Add(CardDeckManager.UniverSity.Software);
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 10번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Software);
        university.Add(CardDeckManager.UniverSity.Software);
        university.Add(CardDeckManager.UniverSity.Software);
        grades.Add(new Grades(Grades.Compare.Equal, 5));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 11번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.NaturalScience);
        university.Add(CardDeckManager.UniverSity.SocialSciences);
        university.Add(CardDeckManager.UniverSity.IC);
        university.Add(CardDeckManager.UniverSity.Engineering);
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 12번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Humanities);
        university.Add(CardDeckManager.UniverSity.Business);
        university.Add(CardDeckManager.UniverSity.SocialSciences);
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 13번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        grades.Add(new Grades(Grades.Compare.Equal, 5));
        grades.Add(new Grades(Grades.Compare.Equal, 5));
        grades.Add(new Grades(Grades.Compare.Equal, 2));
        grades.Add(new Grades(Grades.Compare.Equal, 2));
        grades.Add(new Grades(Grades.Compare.Equal, 1));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 14번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Pharmacy);
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 15번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Medical);
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 16번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.IC);
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 17번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Humanities);
        university.Add(CardDeckManager.UniverSity.Nursing);
        university.Add(CardDeckManager.UniverSity.SocialSciences);
        university.Add(CardDeckManager.UniverSity.Business);
        grades.Add(new Grades(Grades.Compare.Equal, 3));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 18번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        grades.Add(new Grades(Grades.Compare.Equal, 2));
        grades.Add(new Grades(Grades.Compare.Equal, 2));
        grades.Add(new Grades(Grades.Compare.Equal, 2));
        grades.Add(new Grades(Grades.Compare.Equal, 1));
        grades.Add(new Grades(Grades.Compare.Equal, 1));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 19번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        university.Add(CardDeckManager.UniverSity.Business);
        university.Add(CardDeckManager.UniverSity.Business);
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

        #region 20번
        university = new List<CardDeckManager.UniverSity>();
        grades = new List<Grades>();

        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        grades.Add(new Grades(Grades.Compare.Equal, 4));
        answerPaper.Add(new Answer(university, grades));
        #endregion

    }
    #endregion

    List<Answer> NowAnswerPaper = new List<Answer>();

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
    }

    public void ConfirmAnswer()
    {

    }
}
