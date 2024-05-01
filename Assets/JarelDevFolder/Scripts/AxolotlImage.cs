using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxolotlImage : MonoBehaviour
{
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        switch(FishLibrary.axolotlColor)
        {
            case 0:
                image.color = new Color(0.6196f, 0.6824f, 1.0f);
                break;
            case 1:
                image.color = new Color(1.0f, 0.4902f, 0.4902f);
                break;
            case 2:
                image.color = new Color(0.5882f, 1.0f, 0.4902f);
                break;
            case 3:
                image.color = new Color(0.5882f, 1.0f, 0.4902f);
                break;
            default:
                image.color = Color.white;
                break;
        }
    }
}
