using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToClubHouse : MonoBehaviour
{
    public GameObject clubhouseUI;
    // Start is called before the first frame update
    void Start()
    {
        clubhouseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToClubhouse()
    {
        clubhouseUI.SetActive(false);
        SceneManager.LoadScene("Clubhouse");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            clubhouseUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            clubhouseUI.SetActive(false);
        }
    }
}
