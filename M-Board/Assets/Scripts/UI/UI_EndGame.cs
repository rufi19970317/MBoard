using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_EndGame : MonoBehaviour
{
    [SerializeField]
    GameObject playerPoint;
    [SerializeField]
    GameObject enemyPoint;
    [SerializeField]
    Button ConfirmBtn;

    void Awake()
    {
        ConfirmBtn.onClick.AddListener(ResetGame);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetPopUp(int PlayerPoint, int EnemyPoint)
    {
        if(PlayerPoint > EnemyPoint)
        {
            playerPoint.transform.SetSiblingIndex(0);
            playerPoint.transform.GetChild(0).GetComponent<TMP_Text>().text = "1";

            enemyPoint.transform.GetChild(0).GetComponent<TMP_Text>().text = "2";
        }
        else
        {
            enemyPoint.transform.SetSiblingIndex(0);
            enemyPoint.transform.GetChild(0).GetComponent<TMP_Text>().text = "1";
            playerPoint.transform.GetChild(0).GetComponent<TMP_Text>().text = "2";

        }
        playerPoint.transform.GetChild(2).GetComponent<TMP_Text>().text = PlayerPoint.ToString();
        enemyPoint.transform.GetChild(2).GetComponent<TMP_Text>().text = EnemyPoint.ToString();
    }
}
