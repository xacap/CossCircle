using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CGameViewObject : MonoBehaviour
{
    public BoxState currentBoxState = BoxState.eObjectEmpty;
    public Sprite circle_Empty;
    public Sprite circle_X;
    public Sprite circle_O;

    void Start()
    {
        SetBoxState(BoxState.eObjectEmpty);
    }

   
    public void SetBoxState (BoxState newBoxState)
    {
        if (newBoxState == BoxState.eObjectEmpty)
        {
            this.GetComponent<SpriteRenderer>().sprite = circle_Empty;
        }
        if (newBoxState == BoxState.eObjectCross)
        {
            this.GetComponent<SpriteRenderer>().sprite = circle_X;
        }
        if(newBoxState == BoxState.eObjectCircle)
        {
            this.GetComponent<SpriteRenderer>().sprite = circle_O;
        }

        currentBoxState = newBoxState;
    }

    public bool HitTest(Vector2 coordinates)
    {
        var worldPoint = Camera.main.ScreenToWorldPoint((Vector3)coordinates);

        return GetComponent<Collider2D>().OverlapPoint((Vector2)worldPoint);
    }
}
