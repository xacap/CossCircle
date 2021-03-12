using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CGameData
{
    private List<CPlayer> mPlayersArray;
    public CFieldData mFieldData;

    ArrayList mWinLines = new ArrayList();         //создаю массив объектов линий (массив массивов)
   
    
    public int CountPlayerO
    {
        get { return mPlayersArray[0].WinCount; }
    }
    public int CountPlayerX
    {
        get { return mPlayersArray[1].WinCount; }
    }



    public CGameData()
    {
        for (int x = 0; x < 3; x++)
        {
            Vector2Int[] line = new Vector2Int[3]; //создаю массив вертикальных  
                                                   //line {(0,0), (0,1), (0,2)};
                                                   //line {(1,0), (1,1), (1,2)};
                                                   //line {(2,0), (2,1), (2,2)};
            for (int y = 0; y < 3; y++)
            {
                line[y] = new Vector2Int(x, y);
            }

            mWinLines.Add(line);
        }

        for (int y = 0; y < 3; y++)
        {
            Vector2Int[] line = new Vector2Int[3];  //создаю массив горизоньтальных
                                                    //line {(0,0), (1,0), (2,0)};
                                                    //line {(0,1), (1,1), (2,1)};
                                                    //line {(0,2), (1,2), (2,2)};
            for (int x = 0; x < 3; x++)
            {
                line[x] = new Vector2Int(x, y);
            }

            mWinLines.Add(line);
        }

        {
            int y = 0;
            Vector2Int[] line = new Vector2Int[3]; //создаю массив горизонь 1
                                                   //line {(0,0), (1,1), (2,2)};

            for (int x = 0; x < 3; x++, y++)
            {
                line[x] = new Vector2Int(x, y); 
            }

            mWinLines.Add(line);
        }

        {
            int y = 2;
            Vector2Int[] line = new Vector2Int[3]; //создаю массив горизонт 2
                                                   //line {(0,2), (1,1), (2,0)};

            for (int x = 0; x < 3; x++, y--)
            {
                line[x] = new Vector2Int(x, y);
            }

            mWinLines.Add(line);
        }

        mPlayersArray = new List<CPlayer>(); // создаю список игроков
        mFieldData = new CFieldData();      // ???

        mPlayersArray.Add(new CPlayer(BoxState.eObjectCircle, 0)); // добавляю в список игроков и назначаем им BoxState
        mPlayersArray.Add(new CPlayer(BoxState.eObjectCross, 0));

        mPlayersArray[0].SetState(EPlayerState.ePlayerActive); // назначаю первому игроку состояние актив
        mPlayersArray[1].SetState(EPlayerState.ePlayerWaiting); // назначаю второму игроку состояние ожидание

        if (PlayerPrefs.HasKey("gameData"))
        {
            mPlayersArray[0].WinCount = JsonUtility.FromJson<WinnerData>(PlayerPrefs.GetString("gameData")).mWinO;
            mPlayersArray[1].WinCount = JsonUtility.FromJson<WinnerData>(PlayerPrefs.GetString("gameData")).mWinX;
        }
    }


    public BoxState GetCurrentBoxState() 
    {
        for(int i = 0; i < mPlayersArray.Count; i++)
        {
            if(mPlayersArray[i].GetState() == EPlayerState.ePlayerActive)
            {
                return mPlayersArray[i].GetBoxKind();
            }
        }

        return BoxState.eObjectEmpty;
    }

    public void ResetWinnerPlayer()
    {
        mPlayersArray[0].SetState(EPlayerState.ePlayerActive);
        mPlayersArray[1].SetState(EPlayerState.ePlayerWaiting);

        mFieldData.reset();
    }

    

    public bool checkGameDraw()
    {
        return mFieldData.checkDraw();
    }

    public void ResetPlayerBoxState()
    {
        for (int i = 0; i < mPlayersArray.Count; i++)
        {
            //mPlayersArray[i].SetBoxState(BoxState.eObjectEmpty);
            mPlayersArray[i].PlayBoxState = BoxState.eObjectEmpty;
            mPlayersArray[i].SetState(EPlayerState.ePlayerWinner);

        }
    }

    public bool checkGameFinish()
    {
        bool findedMatch = false;

        for(int lineIndex = 0; lineIndex < mWinLines.Count; lineIndex++)  //пербираем массив линий
        {
            Vector2Int[] line = (Vector2Int[]) mWinLines[lineIndex];    //назначем в массив линии значения из массива линий 

            int matchCounter = 0;           
            BoxState lastBoxState = BoxState.eObjectEmpty;
           
            for (int i = 0; i < line.Length - 1; i++)      // перебираем сами линии
            {
                Vector2Int point = line[i];                // объявляем  перменную и присвемваем ей первое значение из массива линии
                BoxState currentBoxState = mFieldData.getValueByCell(point.x, point.y); 

                point = line[i+1];                        // присвемваем перменной второе значение из массива линии

                BoxState nextBoxState = mFieldData.getValueByCell(point.x, point.y);
                lastBoxState = nextBoxState;

                if ((currentBoxState != BoxState.eObjectEmpty) && (nextBoxState != BoxState.eObjectEmpty))
                {
                    if(currentBoxState == nextBoxState)
                    {
                        matchCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if(matchCounter == 2)
            {
                findedMatch = true;

                for (int j = 0; j < mPlayersArray.Count; j++)
                {
                    if (mPlayersArray[j].GetBoxKind() == lastBoxState)
                    {
                        mPlayersArray[j].SetState(EPlayerState.ePlayerWinner);
                        mPlayersArray[j].WinCount += 1;
                        //AddWinnerCount(CountPlayerO, CountPlayerX); 
                    }
                }

                break;
            }
        }

        return findedMatch;
    }

    

    public CPlayer getWinnerPlayer()
    {
        CPlayer winnerPlayer = null;

        for (int j = 0; j < mPlayersArray.Count; j++)
        {
            if (mPlayersArray[j].GetState() == EPlayerState.ePlayerWinner)
            {
                winnerPlayer = mPlayersArray[j];
                
            }
        }
        return winnerPlayer;
    }

    public bool isPlayerActive()
    {
        for (int i = 0; i < mPlayersArray.Count; i++)
        {
            CPlayer currentPlayer = mPlayersArray[i];

            if (currentPlayer.GetState() == EPlayerState.ePlayerActive)
            {
                return true;
            }
        }

        return false;

    }

    public void switchActivePlayer()
    {
        for (int i = 0; i < mPlayersArray.Count; i++)
        {
            CPlayer currentPlayer = mPlayersArray[i];

            if (currentPlayer.GetState() == EPlayerState.ePlayerWaiting || currentPlayer.GetState() == EPlayerState.ePlayerWinner)
            {
                currentPlayer.SetState(EPlayerState.ePlayerActive);
            }
            else
            {
                currentPlayer.SetState(EPlayerState.ePlayerWaiting);
            }
        }
    }

    [Serializable]
    public class WinnerData
    {
        public int mWinO = 0;
        public int mWinX = 0;
    }

    public void AddWinnerCount(int winO, int winX)
    {
        WinnerData winnerData = new WinnerData { mWinO = winO, mWinX = winX };


        /*if(PlayerPrefs.HasKey("gameData"))
        {
            WinnerData jsonCount = new WinnerData();

            jsonCount = JsonUtility.FromJson<WinnerData>(PlayerPrefs.GetString("gameData"));

            winnerData.mWinO += mPlayersArray[0].WinCount;
            winnerData.mWinX += mPlayersArray[0].WinCount;

            //winnerData.mWinO += jsonCount.mWinO;
            //winnerData.mWinX += jsonCount.mWinX;
        }*/
       

        string json = JsonUtility.ToJson(winnerData);
        PlayerPrefs.SetString("gameData", json);
        PlayerPrefs.Save();

        Debug.Log("Побед ноликов: " + winnerData.mWinO + " Побед крестиков: " + winnerData.mWinX);
    }
    public void DeleteCount()
    {
        if (PlayerPrefs.HasKey("gameData"))
        {
            PlayerPrefs.DeleteKey("gameData");
            WinnerData winnerData = new WinnerData();
            string json = JsonUtility.ToJson(winnerData);
            winnerData.mWinO = 0;
            winnerData.mWinX = 0;

            PlayerPrefs.SetString("gameData", json);
            PlayerPrefs.Save();
        }
    }

    /*public void StartCGameData()
    {

        CGameController.instance.GetNotificationManager().Register(EEventType.eRestartGameEvent, ResetWinnerPlayer);
    }*/

    /*Vector2Int point = line[0];
            BoxState checkState = mFieldData.getValueByCell(point.x, point.y);
            
            if(checkState != BoxState.eObjectEmpty)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    Vector2Int point = line[i];
                    BoxState currentBoxState = mFieldData.getValueByCell(point.x, point.y);

                    if (currentBoxState == checkState)
                    {
                        matchCounter++;
                    }
                }
            }*/
}
