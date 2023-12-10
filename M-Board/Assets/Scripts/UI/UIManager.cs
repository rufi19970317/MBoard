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

    [SerializeField]
    UI_Point pointUI;

    public GameObject DiscardZone_Discard;
    public GameObject DiscardZone_Oppocard;
    public GameObject ChangeZone;

    private void Start()
    {
        ResetButton.onClick.AddListener(ResetGame);
    }

    public void OnPlay(int nowPlayerNum, List<Hand> hands)
    {
        StartUI.SetActive(false);
        playUI.gameObject.SetActive(true);
        SetHands(nowPlayerNum, hands);
        pointUI.SetPoint();
    }

    public List<Card> SetHands(int nowPlayerNum, List<Hand> hands)
    {
        return playUI.SetHands(nowPlayerNum, hands);
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

    public Card OnDraw(CardStruct card, int playerNum)
    {
        return playUI.DrawCard(card, playerNum);
    }

    public void OnFinish(int point, int playerNum)
    {
        if (playerNum == 0) pointUI.SetPlayerPoint(point);
        else if (playerNum == 1) pointUI.SetEnemyPoint(point);
    }

    public void SetDeckCard(CardStruct card)
    {
        playUI.SetDeckCard(card);
    }

    public void SetDrawAnimActive(bool isActive)
    {
        playUI.SetDrawAnimActive(isActive);
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
