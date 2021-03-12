using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFieldView : MonoBehaviour
{
    public CGameViewObject boxPrefab;
    public CGameViewObject[,] grid;
    public float startPosX;
    public float startPosY;
    public float outX;
    public float outY;

    public CInput mousePos;
    public void StartGame()
    {
        grid = new CGameViewObject[3, 3];

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                grid[x, y] = RegisterIcon(new CCell(x, y));
            }
        }

        //CGameController.OnRestartGameEvent += ResetBoxState;
        //CGameController.instance.GetNotificationManager().Register(EEventType.eRestartGameEvent, ResetBoxState);
    }
    public CGameViewObject RegisterIcon(CCell pos)
    {
        CGameViewObject box = Instantiate(boxPrefab);
        box.transform.localScale = Vector3.one;
        box.transform.position = GetIconCenterByCell(pos);
        
        return box;
    }

    public Vector2 ObjectByTouch (Vector2 position)
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (grid[x, y].HitTest(position))
                {
                    return new Vector2(x, y);
                }
            }
        }
        return new Vector2(-1, -1);
    }

    public void AnimatedBoxState(Vector2 position, BoxState newboxStt)
    {
        int x = (int)position.x;
        int y = (int)position.y;
        CGameViewObject curentBox = grid[x,y];

        if (curentBox.currentBoxState != newboxStt)
        {
            curentBox.SetBoxState(newboxStt);
        }
    }

    public void ResetBoxState()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                grid[x, y].SetBoxState(BoxState.eObjectEmpty);
            }
        }
    }

    public Vector3 GetIconCenterByCell(CCell cell)
    {
        return new Vector3(
            startPosX + cell.x * outX,
            startPosY - cell.y * outY,
            this.transform.position.z
        );
    }
    public class CCell
    {
        public int x;
        public int y;
        public CCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

