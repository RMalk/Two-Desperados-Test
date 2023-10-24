using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame_TTTButton : MonoBehaviour
{
    public int x, y;

    void Awake ()
    {
        //Doublechecking that the button indicies are assigned
        if (x == 0 || x == null)
        {
            for (int i = 0; i < 3/*TODO make editable*/; i++)
            {
                x = i+1;
                if (transform.parent.parent.GetChild(i) == transform.parent)
                {
                    break;
                } 
                else if ( i == 2)
                {
                    Debug.LogError("Missing parent/child reference. Please check hiearchy");
                }
            }
        }

        if (y == 0 || y == null)
        {
            for (int i = 0; i < 3/*TODO make editable*/; i++)
            {
                y = i+1;
                if (transform.parent.GetChild(i) == transform)
                {
                    break;
                }
                else if (i == 2)
                {
                    Debug.LogError("Missing parent/child reference. Please check hiearchy");
                }
            }
        }

    }
}
