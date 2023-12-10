using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnswerManager;

public class UI_FinishAnswerPopUp : MonoBehaviour
{
    [SerializeField]
    GameObject answerFolder;
    [SerializeField]
    GameObject contentPrefab;

    public void SetAnswerList(List<PlayerAnswer> AllPlayerAnswerCardsList)
    {
        for (int i = 0; i < answerFolder.transform.childCount; i++)
        {
            Destroy(answerFolder.transform.GetChild(i).gameObject);
        }

        foreach (PlayerAnswer card in AllPlayerAnswerCardsList)
        {
            Instantiate(contentPrefab, answerFolder.transform).GetComponent<UI_AnswerContent>().SetAnswer(card, true);
        }
    }
}
