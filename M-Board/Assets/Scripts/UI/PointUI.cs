using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class PointUI : MonoBehaviour
{
    [SerializeField]
    GameObject PointPrefab;

    List<GameObject> playerPoints = new List<GameObject>();

    public void SetPointUI(int playerNum)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        playerPoints = new List<GameObject>();

        for (int i = 1; i < playerNum + 1; i++)
        {
            GameObject pointPrefab = Instantiate(PointPrefab);
            pointPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = i.ToString();
            
            pointPrefab.transform.SetParent(transform);
            playerPoints.Add(pointPrefab);
        }
    }

    public void AddPoint(int point, int playerNum)
    {
        int pointSum = 0;
        Int32.TryParse(playerPoints[playerNum].transform.GetChild(1).GetComponent<TMP_Text>().text, out pointSum);
        pointSum += point;
        playerPoints[playerNum].transform.GetChild(1).GetComponent<TMP_Text>().text = pointSum.ToString();
    }
}
