using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject StartUI;
    
    [SerializeField]
    PlayUI playUI;

    [SerializeField]
    Button ResetButton;


    public GameObject DiscardZone;

    private void Start()
    {
        ResetButton.onClick.AddListener(ResetGame);
    }

    public void OnPlay(int nowPlayerNum, List<Hand> hands)
    {
        StartUI.SetActive(false);
        playUI.gameObject.SetActive(true);
        playUI.SetHands(nowPlayerNum, hands);
    }

    public void SetRepCard(List<CardStruct> repCards)
    {
        playUI.SetRepCard(repCards);
    }

    public void SetRepCard(CardStruct card, int playerNum)
    {
        playUI.SetRepCard(card, playerNum);
    }

    public void StartGame()
    {
        StartUI.SetActive(true);
        playUI.gameObject.SetActive(false);
    }

    public void OnDraw(CardStruct card, int playerNum)
    {
        playUI.DrawCard(card, playerNum);
    }

    public void OnFinish(int point, int playerNum)
    {

    }

    public void SetDeckCard(CardStruct card)
    {
        playUI.SetDeckCard(card);
    }

    public void ResetGame()
    {
        StartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public GameObject GetPlayerHand(int playerNum)
    {
        return playUI.GetEnemyHand(playerNum);
    }
}
