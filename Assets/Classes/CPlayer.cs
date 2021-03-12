using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EPlayerState
{
    ePlayerActive,
    ePlayerWaiting,
    ePlayerInAction,
    ePlayerWinner,
    ePlayerNone
}
public enum BoxState
{
    eObjectCross,
    eObjectCircle,
    eObjectEmpty
}

public class CPlayer
{
    private EPlayerState mState = EPlayerState.ePlayerNone;

    private BoxState mBoxKind;

    private int mWinCount;
    public int WinCount
    { 
        get { return mWinCount; }
        set { mWinCount = value;  }
    }

    public BoxState PlayBoxState
    {
        get { return mBoxKind; }
        set { mBoxKind = value; }
    }
    public void SetBoxState(BoxState empty)
    {
        mBoxKind = empty;
    }

    public CPlayer(BoxState aBoxKind, int mWinCount)
    {
        mBoxKind = aBoxKind;
        mWinCount = 0;
    }

    public BoxState GetBoxKind()
    {
        return mBoxKind;
    }
    public EPlayerState GetState()
    {
        return mState;
    }
    


    public void SetState(EPlayerState aState)
    {
        mState = aState;
    }

    
}
