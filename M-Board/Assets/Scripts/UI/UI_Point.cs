using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Point : MonoBehaviour
{
    [SerializeField]
    TMP_Text playerPoint;
    [SerializeField]
    TMP_Text enemyPoint;

    public void SetPoint()
    {
        playerPoint.text = "0";
        enemyPoint.text = "0";
    }

    public void SetPlayerPoint(int point)
    {
        playerPoint.text = point.ToString();
    }
    public void SetEnemyPoint(int point)
    {
        enemyPoint.text = point.ToString();
    }
}
