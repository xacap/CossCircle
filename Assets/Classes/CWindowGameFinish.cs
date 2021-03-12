using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CWindowGameFinish : MonoBehaviour
{
    
    public void Restart(EEventType eventType)
    {
        Destroy(this.gameObject);

        CGameController.instance.GetNotificationManager().Invoke(eventType);

    }

    void Start()
    {
        GameObject panelNode = gameObject.transform.Find("Panel").gameObject;
        GameObject obj = panelNode.transform.Find("Button").gameObject;
        Button button = obj.GetComponent<Button>();
        button.onClick.AddListener(() => Restart(EEventType.eRestartGameEvent));
    }
    public void Show(BoxState winnerBoxState)
    { 
        Text winText = GameObject.Find("Panel/Text").GetComponent<Text>();

       switch (winnerBoxState)
       {
            case BoxState.eObjectCircle:
                winText.text = "Победили нолики!";
                break;
            case BoxState.eObjectCross:
                winText.text = "Победили крестики!";
                break;
            case BoxState.eObjectEmpty:
                winText.text = "Ничья";
                break;
        }
    }
    public void DrawShow()
    {
        Text drawText = GameObject.Find("Panel/Text").GetComponent<Text>();
        drawText.text = "Ничья";
    }
}
