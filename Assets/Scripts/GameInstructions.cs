using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstructions : MonoBehaviour
{
    public FishManager fishmanager;

    public GameObject CastInstructions;
    public GameObject HookInstructions;
    public GameObject CatchingInstructions;
    public GameObject KeepThrowInstructions;
    public GameObject MenuInstructions;

    public GameObject GameUI;
    // Start is called before the first frame update
    void Start()
    {
        fishmanager.CanFish = false;

        CastInstructions.SetActive(true);
        HookInstructions.SetActive(false);
        CatchingInstructions.SetActive(false);
        KeepThrowInstructions.SetActive(false);
        MenuInstructions.SetActive(false);
        GameUI.SetActive(false);
    }

    public void HookIN()
    {
        CastInstructions.SetActive(false);
        HookInstructions.SetActive(true);
    }
    public void CatchIN()
    {
        HookInstructions.SetActive(false);
        CatchingInstructions.SetActive(true);
    }
    public void KeepIN()
    {
        CatchingInstructions.SetActive(false);
        KeepThrowInstructions.SetActive(true);
    }
    public void MenuIN()
    {
        KeepThrowInstructions.SetActive(false);
        MenuInstructions.SetActive(true);
    }
    public void FinishIN()
    {
        MenuInstructions.SetActive(false);
        GameUI.SetActive(true);
        fishmanager.CanFish = true;
    }
}