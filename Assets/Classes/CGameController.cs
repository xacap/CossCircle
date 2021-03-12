using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CGameController : MonoBehaviour, IInputObserver
{
    public static CGameController instance;
    private CInput input;
    private CGameData mGameData;
    private CFieldView mFieldViev;
    private CUIController mUIController = new CUIController();
    private bool mIsGameFinished = false;
    private CPlayer player;
    private CEvents mNotificationManager = new CEvents();

    public CEvents GetNotificationManager()
    {
        return mNotificationManager;
    }

       
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    void Update()
    {
        input.Check();

        if (mGameData != null)
       
            return;
       

        if (!mIsGameFinished && !mGameData.checkGameDraw())
        {
            mUIController.ShowGameFinishWindow(BoxState.eObjectEmpty);
            mIsGameFinished = true;
        }
    }
    private bool ResetDraw()
    {
        return false;
    }
    public void IsGameFinished()
    {
        mFieldViev.ResetBoxState();
        mGameData.ResetWinnerPlayer();
        mIsGameFinished = false;
        mGameData.AddWinnerCount(mGameData.CountPlayerO, mGameData.CountPlayerX);

        //if (OnRestartGameEvent != null)
        //{ OnRestartGameEvent(); }
    }

    public void CountZero()
    {
        mGameData.DeleteCount();
    }
    public bool OnInputBegin(Vector2 position)
    {
        if (mIsGameFinished)
        {
            return false;
        }
        if (ResetDraw())
        {
            return false;
        }

        if ((mGameData != null) && mGameData.isPlayerActive())
        {
            Vector2 objPosition = mFieldViev.ObjectByTouch(position);

            if(objPosition.x != -1)
            {
                int targetX = (int)objPosition.x;
                int targetY = (int)objPosition.y;

                if(mGameData.mFieldData.isCellEmpty(targetX,targetY))
                {
                    BoxState currentBox = mGameData.GetCurrentBoxState();

                    mGameData.mFieldData.SetValueByCell(targetX, targetY, currentBox);
                    mFieldViev.AnimatedBoxState(objPosition, currentBox);

                    if(mGameData.checkGameFinish())
                    {
                        mIsGameFinished = true;
                        
                        mUIController.ShowGameFinishWindow(mGameData.getWinnerPlayer().GetBoxKind());
                        mUIController.UpdateUI(mGameData.CountPlayerO, mGameData.CountPlayerX);

                    }
                    else
                    {
                        mGameData.switchActivePlayer();
                    }
                }    
                
            }

        }

        return true;
        
    }
   
    public void OnInputMove(Vector2 position)
    {

    }

    public void OnInputEnd(Vector2 position)
    {
       Vector2 objPosition = mFieldViev.ObjectByTouch(position);

        mFieldViev.AnimatedBoxState(objPosition, BoxState.eObjectCircle);
    }


    void Start()
    {
        mGameData = new CGameData();
        
        if (mFieldViev == null)
        {
            mFieldViev = GameObject.Find("Field").GetComponent<CFieldView>();
        }
        mFieldViev.StartGame();

        input = new CInput();
        input.registerObserver(this, 0);

        mNotificationManager.Register(EEventType.eRestartGameEvent, IsGameFinished); 
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("Home");
    }

}
   
    

