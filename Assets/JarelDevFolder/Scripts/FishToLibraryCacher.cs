using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishToLibraryCacher : MonoBehaviour
{
    public GameObject carpImageUI;
    public GameObject bassImageUI;
    public GameObject silverfishImageUI;
    public GameObject bootImageUI;
    public GameObject controllerImageUI;
    public GameObject junkImageUI;
    public GameObject koiImageUI;
    public GameObject sturgeonImageUI;
    public GameObject bluegillImageUI;
    public GameObject goldfishImageUI;
    public GameObject mustacheImageUI;
    public GameObject bullroutImageUI;

    public GameObject carp;
    public GameObject bass;
    public GameObject silverfish;
    public GameObject leatherBoot;
    public GameObject gameController;
    public GameObject junkObject;
    public GameObject koi;
    public GameObject sturgeon;
    public GameObject bluegill;
    public GameObject goldfish;
    public GameObject mustache;
    public GameObject bullrout;
    void Start()
    {
        if (!FishLibrary.isCached)
        {
            FishLibrary.fishObjects.Add(carp);
            FishLibrary.fishImages.Add(carpImageUI);
            
            FishLibrary.fishObjects.Add(bass);
            FishLibrary.fishImages.Add(bassImageUI);
            
            FishLibrary.fishObjects.Add(silverfish);
            FishLibrary.fishImages.Add(silverfishImageUI);
            
            FishLibrary.fishObjects.Add(leatherBoot);
            FishLibrary.fishImages.Add(bootImageUI);

            FishLibrary.fishObjects.Add(gameController);
            FishLibrary.fishImages.Add(controllerImageUI);

            FishLibrary.fishObjects.Add(junkObject);
            FishLibrary.fishImages.Add(junkImageUI);

            FishLibrary.fishObjects.Add(koi);
            FishLibrary.fishImages.Add(koiImageUI);

            FishLibrary.fishObjects.Add(sturgeon);
            FishLibrary.fishImages.Add(sturgeonImageUI);

            FishLibrary.fishObjects.Add(bluegill);
            FishLibrary.fishImages.Add(bluegillImageUI);

            FishLibrary.fishObjects.Add(goldfish);
            FishLibrary.fishImages.Add(goldfishImageUI);

            FishLibrary.fishObjects.Add(mustache);
            FishLibrary.fishImages.Add(mustacheImageUI);

            FishLibrary.fishObjects.Add(bullrout);
            FishLibrary.fishImages.Add(bullroutImageUI);


            //keep game from restarting this in other instances after the first instance
            FishLibrary.isCached = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}