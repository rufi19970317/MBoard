using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    private int playerNum = 2;


    public void OnPlay()
    {
        GameManager.Instance.OnPlay(playerNum);
    }
}
