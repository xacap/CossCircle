using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFieldData
{
    private BoxState[,] mGrid;

   
    public CFieldData()
    {
        mGrid = new BoxState[3, 3];
        
        reset();
    }

    public void reset()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                mGrid[i,j] = BoxState.eObjectEmpty;
            }
        }
    }

    public bool checkDraw()
    {
        bool draw = false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (mGrid[i, j] == BoxState.eObjectEmpty)
                {
                   draw = true;
                   break;
                }
            }
        }
        return draw;
    }

    public BoxState getValueByCell(int x, int y)
    {
        return mGrid[x,y];
    }
    public bool isCellEmpty(int x, int y)
    {
        return (mGrid[x,y] == BoxState.eObjectEmpty);
    }

    public void SetValueByCell(int x, int y, BoxState aState)
    {
        mGrid[x, y] = aState;
    }
}
