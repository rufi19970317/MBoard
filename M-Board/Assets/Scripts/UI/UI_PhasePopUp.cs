using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PhasePopUp : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;

    public void OnPhasePopUp(GameManager.Phase phase, bool isMyTurn)
    {
        switch(phase)
        {
            case GameManager.Phase.Draw:
            case GameManager.Phase.Discard:
                text.text = (isMyTurn ? "My Turn : " : "Enemy Turn : ") + phase.ToString();
                text.color = isMyTurn ? Color.black : Color.red;
                break;
        }
    }
}
