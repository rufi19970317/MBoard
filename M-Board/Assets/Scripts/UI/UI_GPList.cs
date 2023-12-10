using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnswerManager;

public class UI_GPList : MonoBehaviour
{
    [SerializeField]
    GameObject answerFolder;
    [SerializeField]
    GameObject contentPrefab;


    public void ResetList()
    {
        for (int i = 0; i < answerFolder.transform.childCount; i++)
        {
            Destroy(answerFolder.transform.GetChild(i).gameObject);
        }
    }

    public void SetAnswerList(List<PlayerDefaultAnswer> AllPlayerAnswerCardsList)
    {
        ResetList();
        foreach (PlayerDefaultAnswer card in AllPlayerAnswerCardsList)
        {
            Instantiate(contentPrefab, answerFolder.transform).GetComponent<UI_GeneralProject>().SetAnswer(card, false);
        }
    }
}
