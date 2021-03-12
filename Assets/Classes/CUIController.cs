using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIController : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void ShowGameFinishWindow(BoxState winBoxState)
    {
        GameObject go = Instantiate(Resources.Load("WInCanvas")) as GameObject;
        go.GetComponent<RectTransform>().localPosition = new Vector3(960f, 540f, 0);

        go.GetComponent<CWindowGameFinish>().Show(winBoxState);

    }
    

    public void UpdateUI(int countO, int countX)
    {
        Text textCountO = GameObject.Find("UI/Canvas/TextCountO/CountO").GetComponent<Text>();
        Text textCountX = GameObject.Find("UI/Canvas/TextCountX/CountX").GetComponent<Text>();
                
        textCountO.text = Convert.ToString(JsonUtility.FromJson<CGameData.WinnerData>(PlayerPrefs.GetString("gameData")).mWinO);
        textCountX.text = Convert.ToString(JsonUtility.FromJson<CGameData.WinnerData>(PlayerPrefs.GetString("gameData")).mWinX);
    }

}
