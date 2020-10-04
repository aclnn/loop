using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public void DisableCursor()
    {
        Cursor.visible = false;
    }
    public void EnableCursor()
    {
        Cursor.visible = true;
    }
}
