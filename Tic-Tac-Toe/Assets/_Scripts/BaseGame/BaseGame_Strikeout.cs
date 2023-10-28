using UnityEngine;
using UnityEngine.UI;

public class BaseGame_Strikeout : MonoBehaviour
{
    int currentStyle = 0;
    [SerializeField] private StrokeSwitch strokeSwitch;
    [SerializeField] private Image[] strikeoutImages = new Image[4];

    void OnEnable()
    {
        //Set stroke style
        if (PlayerPrefs.HasKey("Symbol Style"))
        {
            int strokeStyle = PlayerPrefs.GetInt("Symbol Style");
            if (strokeStyle != currentStyle)
            {
                for (int i = 0; i < strikeoutImages.Length; i++)
                {
                    strikeoutImages[i].sprite = strokeSwitch.stroke[strokeStyle].strokeImage;
                    strikeoutImages[i].color = strokeSwitch.stroke[strokeStyle].color[0];
                }
                currentStyle = strokeStyle;
            }
        }
    }
}
