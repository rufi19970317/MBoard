using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Linq;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public Image frontImage;

    [SerializeField]
    GameObject BackGround;

    public CardStruct cardInfo;
    public PlayerHand hand;

    public enum CardState
    {
        MyCard,
        OppoCard,
        DeckCard,
        DumpCard,
        RepCard
    }

    private CardState cardState;

    void OnDisable()
    {
        if (particle != null)
            particle.SetActive(false);
    }

    // 카드 세팅
    public Card SetCard(PlayerHand hand, CardStruct cardStruct, CardState cardState)
    {
        this.cardState = cardState;

        switch(cardState)
        {
            case CardState.MyCard:
            case CardState.RepCard:
                string path = "Images/Students/Student_" + cardStruct.univerSity.ToString() + "_" + cardStruct.num.ToString();
                Debug.Log(path);
                frontImage.sprite = Resources.Load<Sprite>(path);
                isDiscard = false;
                break;
            case CardState.DeckCard:
                ReverseCard(true);
                transform.position = transform.parent.position;
                break;
        }

        cardInfo = cardStruct;
        if(hand != null) this.hand = hand;
        return this;
    }

    public bool CompareCard(CardDeckManager.UniverSity university, int num)
    {
        if (cardInfo.univerSity == university && cardInfo.num == num)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 버리는 카드일 때
    public void SetCard(CardStruct discardCard)
    {
        string path = "Images/Students/Student_" + discardCard.univerSity.ToString() + "_" + discardCard.num.ToString();
        frontImage.sprite = Resources.Load<Sprite>(path);
        cardInfo = discardCard;

        cardState = CardState.DumpCard;
    }

    public void ReverseCard(bool isReverse)
    {
        BackGround.SetActive(isReverse);
    }

    Vector2 startPos = Vector2.zero;
    float startTouchPosY = 0f;
    Transform parent;
    bool isStartDrag = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.selectCard != null) return;
        if (GameManager.Instance.isEnd) return;
        GameManager.Instance.selectCard = gameObject;

        switch(cardState)
        {
            case CardState.MyCard:
                if (!(!GameManager.Instance.isMyTurn && GameManager.Instance.GamePhase == GameManager.Phase.Draw))
                {
                    GameManager.Instance.SetActiveDiscardZone(true);
                    parent = transform.parent;
                    GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/NullCard"), parent);
                    go.name = "NullCard";
                    transform.SetParent(parent.parent);
                    isStartDrag = true;
                }
                break;
            case CardState.DumpCard:
                return;
            case CardState.OppoCard:
            case CardState.DeckCard:
                if (GameManager.Instance.GamePhase == GameManager.Phase.Draw && GameManager.Instance.isMyTurn)
                {
                    startTouchPosY = eventData.position.y;
                    startPos = transform.position;
                }
                break;
        }
    }

    float maxDrawCardPosY = 30f;

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.selectCard != gameObject) return;
        if (GameManager.Instance.isEnd) return;

        Vector2 touchPos = eventData.position;
        switch (cardState)
        {
            case CardState.MyCard:
                if (isStartDrag)
                {
                    transform.position = touchPos;

                    List<Transform> parentChildren = new List<Transform>();
                    Transform tr = null;
                    for (int i = 0; i < parent.childCount; i++)
                    {
                        if (parent.GetChild(i).name != "NullCard")
                        {
                            parentChildren.Add(parent.GetChild(i));
                        }
                        else
                        {
                            parentChildren.Add(transform);
                            tr = parent.GetChild(i);
                        }
                    }

                    List<Transform> sortList = (from child in parentChildren
                                                orderby child.position.x
                                                select child).ToList();

                    for (int i = 0; i < sortList.Count; i++)
                    {
                        if (sortList[i] != transform)
                        {
                            sortList[i].SetParent(null);
                            sortList[i].SetParent(parent);
                        }
                        else
                        {
                            tr.SetParent(null);
                            tr.SetParent(parent);
                        }
                    }
                }
                break;
            case CardState.DumpCard:
                return;
            case CardState.OppoCard:
            case CardState.DeckCard:
                if (GameManager.Instance.GamePhase == GameManager.Phase.Draw)
                {
                    float maxDis = 0f;
                    if (cardState == CardState.OppoCard) maxDis = 2f;
                    else if (cardState == CardState.DeckCard) maxDis = 6f;
                    if (startTouchPosY > touchPos.y && startTouchPosY - touchPos.y  < maxDrawCardPosY * maxDis)
                    {
                        float disPosY = startTouchPosY - touchPos.y;
                        transform.position = new Vector2(transform.position.x, startPos.y - disPosY);
                    }

                }
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.selectCard != gameObject) return;
        if (GameManager.Instance.isEnd) return;
        GameManager.Instance.selectCard = null;

        Transform tr = transform.parent;
        List<Transform> parentChildren = new List<Transform>();

        switch (cardState)
        {
            case CardState.MyCard:
                if (isStartDrag)
                {
                    tr = null;
                    for (int i = 0; i < parent.childCount; i++)
                    {
                        if (parent.GetChild(i).name != "NullCard")
                            parentChildren.Add(parent.GetChild(i));
                        else
                        {
                            parentChildren.Add(transform);
                            tr = parent.GetChild(i);
                        }
                    }

                    Destroy(tr.gameObject);

                    List<Transform> sortList = (from child in parentChildren
                                                orderby child.position.x
                                                select child).ToList();

                    for (int i = 0; i < sortList.Count; i++)
                    {
                        sortList[i].SetParent(null);
                        sortList[i].SetParent(parent);
                    }

                    if (isDiscard)
                    {
                        GameManager.Instance.DiscardCard(this);
                        isDiscard = false;
                    }
                    else
                    {
                        GameManager.Instance.SetActiveDiscardZone(false);
                    }

                    if(isChange)
                    {
                        GameManager.Instance.ChangeCard(this);
                    }
                    isStartDrag = false;
                }
                break;
            case CardState.OppoCard:
                tr = transform.parent;
                for (int i = 0; i < tr.childCount; i++)
                {
                    parentChildren.Add(tr.GetChild(i));
                }

                for (int i = 0; i < parentChildren.Count; i++)
                {
                    parentChildren[i].SetParent(null);
                    parentChildren[i].SetParent(tr);
                }
                break;
            case CardState.DumpCard:
                return;
            case CardState.DeckCard:
                transform.position = transform.parent.position;
                break;
        }

        if(cardState == CardState.OppoCard || cardState == CardState.DeckCard)
        {
            if (startTouchPosY - eventData.position.y > maxDrawCardPosY)
            {
                GameManager.Instance.DrawCard(this, cardState);
            }
            startTouchPosY = 0f;
        }
    }

    bool isDiscard = false;
    bool isChange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(cardState == CardState.MyCard)
        {
            if(collision.name == "DiscardZone_Discard" || collision.name == "DiscardZone_Oppocard")
            {
                collision.GetComponent<Image>().color = new Color(255, 0f, 0f, 0.5f);
                isDiscard = true;
            }

            if(collision.name == "ChangeZone" && GameManager.Instance.GamePhase == GameManager.Phase.Draw)
            {
                collision.GetComponent<Image>().color = new Color(255, 0f, 0f, 0.5f);
                isChange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (cardState == CardState.MyCard)
        {
            if (collision.name == "DiscardZone_Discard" || collision.name == "DiscardZone_Oppocard")
            {
                collision.GetComponent<Image>().color = new Color(0f, 0f, 255f, 0.5f);
                isDiscard = false;
            }


            if (collision.name == "ChangeZone" && GameManager.Instance.GamePhase == GameManager.Phase.Draw)
            {
                collision.GetComponent<Image>().color = new Color(0f, 0f, 255f, 0.5f);
                isChange = false;
            }
        }
    }

    [SerializeField]
    GameObject particle;

    public void SetParticle(bool isActive)
    {
        particle.SetActive(isActive);
    }
}
