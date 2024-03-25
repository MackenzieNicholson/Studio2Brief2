using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public RectTransform uiElement; // Reference to the UI element
    public float speed = 200f; // Speed of the sliding movement

    // Start is called before the first frame update
    void Start()
    {
        int noteSize = Random.Range(1, 9);
        Debug.Log("Note size is " + noteSize);
        float noteSizeY = (float)noteSize * 100;

        Vector3 currentScale = transform.localScale;

        // Set the new Y scale
        currentScale.y = noteSizeY;

        // Apply the new scale to the GameObject
        transform.localScale = currentScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.down * speed * Time.deltaTime;

        // Move the GameObject downwards
        transform.position += moveDirection;
    }
}
