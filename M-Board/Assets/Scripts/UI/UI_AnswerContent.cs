using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AnswerManager;

public class UI_AnswerContent : MonoBehaviour
{
    [SerializeField]
    private GameObject leaderImage;
    [SerializeField]
    private GameObject memberImage;
    [SerializeField]
    private TMP_Text pointText;
    [SerializeField]
    private TMP_Text leaderText;
    [SerializeField]
    private TMP_Text memberText;


    public void SetAnswer(PlayerAnswer playerAnswer)
    {
        pointText.text = playerAnswer.point.ToString();
        leaderText.text = playerAnswer.playerLeaderName;

        memberText.text = playerAnswer.playerMemberName;

        for(int i = 0; i < leaderImage.transform.childCount; i++)
        {
            leaderImage.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < memberImage.transform.childCount; i++)
        {
            memberImage.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < playerAnswer.playerLeaderCards.Count; i++)
        {
            leaderImage.transform.GetChild(i).gameObject.SetActive(true);
            leaderImage.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Icon/Icon_" + playerAnswer.playerLeaderCards[i].cardInfo.univerSity.ToString());
            switch (playerAnswer.playerLeaderCards[i].cardInfo.num)
            {
                case 1:
                    leaderImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "F";
                    break;
                case 2:
                    leaderImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "D";
                    break;
                case 3:
                    leaderImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "C";
                    break;
                case 4:
                    leaderImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "B";
                    break;
                case 5:
                    leaderImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "A";
                    break;
            }
        }
        for (int i = 0; i < playerAnswer.playerMemberCards.Count; i++)
        {
            memberImage.transform.GetChild(i).gameObject.SetActive(true);
            memberImage.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Icon/Icon_" + playerAnswer.playerMemberCards[i].cardInfo.univerSity.ToString());
            switch (playerAnswer.playerMemberCards[i].cardInfo.num)
            {
                case 1:
                    memberImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "F";
                    break;
                case 2:
                    memberImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "D";
                    break;
                case 3:
                    memberImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "C";
                    break;
                case 4:
                    memberImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "B";
                    break;
                case 5:
                    memberImage.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = "A";
                    break;
            }
        }
    }
}
