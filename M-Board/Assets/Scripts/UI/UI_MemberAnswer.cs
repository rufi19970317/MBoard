using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UI_MemberAnswer : MonoBehaviour
{
    [SerializeField]
    GameObject openPos;
    [SerializeField]
    GameObject closePos;

    [SerializeField]
    Button openBtn;
    [SerializeField]
    Button closeBtn;

    bool isOpen;
    bool isClose;
    float speed = 400f;


    private void Awake()
    {
        openBtn.onClick.AddListener(OnOpenList);
        closeBtn.onClick.AddListener(OnCloseList);
        OnOpenList();
    }
    private void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPos.transform.position, Time.deltaTime * speed);
            if (transform.position == openPos.transform.position) isOpen = false;
        }
        else if (isClose)
        {
            transform.position = Vector3.MoveTowards(transform.position, closePos.transform.position, Time.deltaTime * speed);
            if (transform.position == closePos.transform.position) isClose = false;
        }
    }
    void OnOpenList()
    {
        openBtn.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(true);
        isOpen = true;
        isClose = false;
    }

    void OnCloseList()
    {
        closeBtn.gameObject.SetActive(false);
        openBtn.gameObject.SetActive(true);
        isOpen = false;
        isClose = true;
    }
}
