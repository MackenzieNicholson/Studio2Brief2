using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axolotl : MonoBehaviour
{
    SpriteRenderer sr;

    int colorNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

        colorNum = Random.Range(0, 4);

        switch(colorNum)
        {
            case 0:
                sr.color = new Color(0.6196f, 0.6824f, 1.0f);
                FishLibrary.axolotlColor = 0;
                break;
            case 1:
                sr.color = new Color(1.0f, 0.4902f, 0.4902f);
                FishLibrary.axolotlColor = 1;
                break;
            case 2:
                sr.color = new Color(0.5882f, 1.0f, 0.4902f);
                FishLibrary.axolotlColor = 2;
                break;
            case 3:
                sr.color = new Color(0.5882f, 1.0f, 0.4902f);
                FishLibrary.axolotlColor = 3;
                break;
            default:
                sr.color = Color.white;
                break;
        }
    }
}
