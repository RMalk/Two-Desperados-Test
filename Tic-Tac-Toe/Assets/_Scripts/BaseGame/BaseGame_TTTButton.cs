using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGame_TTTButton : MonoBehaviour
{
    public int x, y;

    int currentStyle = 0;
    [SerializeField] private SymbolSwitch symbolSwitch;

    [SerializeField] private AnimationScriptController anim;

    void OnEnable ()
    {
        /*
        //Doublechecking that the button indicies are assigned
        if (x == 0 || x == null)
        {
            for (int i = 0; i < 3; i++) //TODO make editable
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
            for (int i = 0; i < 3; i++) //TODO make editable
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
        }*/

        //Set symbol style
        if (PlayerPrefs.HasKey("Symbol Style"))
        {
            int symbolStyle = PlayerPrefs.GetInt("Symbol Style");
            if (symbolStyle != currentStyle)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = symbolSwitch.symbolPair[symbolStyle].symbol[0];
                transform.GetChild(1).GetComponent<Image>().sprite = symbolSwitch.symbolPair[symbolStyle].symbol[1];
                currentStyle = symbolStyle;
            }
        }

        if (!anim)
        {
            anim = gameObject.GetComponent<AnimationScriptController>();
        }
    }

    public void AnimateButton ()
    {
        int random = UnityEngine.Random.Range(0, anim.animationBlueprint.Length);
        anim.PlayAnimation(random);
    }
}
