using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CWindowGameStart : MonoBehaviour
{
    public void Start()
    {
        Text countWinO = GameObject.Find("Canvas/Panel/countO").GetComponent<Text>();
        Text countWinX = GameObject.Find("Canvas/Panel/countX").GetComponent<Text>();

        CGameData.WinnerData count = new CGameData.WinnerData();

        if (PlayerPrefs.HasKey("gameData"))
        {
            count = JsonUtility.FromJson<CGameData.WinnerData>(PlayerPrefs.GetString("gameData"));
            int countO = count.mWinO;
            int countX = count.mWinX;

            countWinO.text = Convert.ToString(countO);
            countWinX.text = Convert.ToString(countX);
        }
        if (!PlayerPrefs.HasKey("gameData"))

        {
            countWinO.text = "0";
            countWinX.text = "0";
        }

    }

   
}
