using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterRotater : MonoBehaviour
{
    private Vector2 dir;

    static readonly Vector3 RightSide = new Vector3(-1, 2.5f, 1);
    static readonly Vector3 LeftSide = new Vector3(1, 2.5f, 1);

    public Vector2 Dir
    {
        get { return dir; }
        set 
        { 
            dir = value;
            UpdateDir();
        }
    }


    private void UpdateDir()
    {
        if(dir.x > 0) { transform.localScale = RightSide; }
        else if (dir.x < 0) { transform.localScale = LeftSide; }
    }
}
