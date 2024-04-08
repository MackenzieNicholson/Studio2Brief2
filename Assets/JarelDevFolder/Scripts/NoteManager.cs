using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public RectTransform spriteRectTransform;
    float slideSpeed = 10f;

    private Vector2 initialPosition;
    private Vector2 targetPosition;

    public bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        int noteSize = Random.Range(1, 10);
        Debug.Log("Note size is " + noteSize);
        float noteSizeY = (float)noteSize;

        Vector3 currentScale = transform.localScale;
        currentScale.y = noteSizeY;
        transform.localScale = currentScale;

        // Get the initial position of the sprite
        initialPosition = spriteRectTransform.anchoredPosition;

        // Calculate the target position outside of the canvas bounds
        Vector2 canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
        float canvasHeight = canvasSize.y;
        targetPosition = initialPosition - new Vector2(0, canvasHeight);
        
        // Start sliding the sprite
        StartCoroutine(SlideCoroutine());
    }

    IEnumerator SlideCoroutine()
    {
        while (spriteRectTransform.anchoredPosition.y > targetPosition.y)
        {
            // Move the sprite downwards
            spriteRectTransform.anchoredPosition -= new Vector2(0, slideSpeed * Time.deltaTime);

            yield return null;
        }

        // Ensure the sprite is at the target position
        spriteRectTransform.anchoredPosition = targetPosition;
    }
}
