using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AnswerManager;

public class UI_DumpCardList : MonoBehaviour
{
    [SerializeField]
    GameObject dumpList;
    [SerializeField]
    GameObject dumpContentPrefab;

    public void SetDumpCardList(CardStruct card)
    {
        GameObject dumpcard = Instantiate(dumpContentPrefab, dumpList.transform, dumpList.transform);
        dumpcard.transform.SetAsFirstSibling();
        dumpcard.transform.GetChild(0).GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Images/Icon/Icon_" + card.univerSity.ToString());
    }

    public void ResetList()
    {
        for(int i = 0; i < dumpList.transform.childCount; i++)
        {
            Destroy(dumpList.transform.GetChild(i).gameObject);
        }
    }
}
