using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static AnswerManager;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GeneralProject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [SerializeField]
    private GameObject memberImage;
    [SerializeField]
    private TMP_Text pointText;
    [SerializeField]
    private TMP_Text gpText;

    public List<Card> answerResult = new List<Card>();
    PlayerDefaultAnswer playerAnswer;
    bool isPopupContent = false;

    public void SetAnswer(PlayerDefaultAnswer playerAnswer, bool isPopupContent)
    {
        this.playerAnswer = playerAnswer;
        this.isPopupContent = isPopupContent;

        pointText.text = playerAnswer.point.ToString();
        gpText.text = playerAnswer.playerMemberName;

        for (int i = 0; i < memberImage.transform.childCount; i++)
        {
            memberImage.transform.GetChild(i).gameObject.SetActive(false);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPopupContent) return;
        for (int i = 0; i < playerAnswer.playerMemberCards.Count; i++)
        {
            playerAnswer.playerMemberCards[i].SetParticle(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPopupContent) return;
        for (int i = 0; i < playerAnswer.playerMemberCards.Count; i++)
        {
            playerAnswer.playerMemberCards[i].SetParticle(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isPopupContent) return;
        GameManager.Instance.FinishDefaultAnswer(playerAnswer);
    }
}
