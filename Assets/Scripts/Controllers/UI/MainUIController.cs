using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController :
    UIControllerBase<MainUIController> {
    
    [Header("Screens")]
    [SerializeField]
    CanvasGroup bgIntroScreen;
    [SerializeField]
    CanvasGroup uiScreen;

    void Start() {
        StartCoroutine(
            IEShowScreen(bgIntroScreen)
        );
    }

    public static void ShowUI() {
        instance.StartCoroutine(
            instance.IEShowScreen(instance.uiScreen)
        );
        
        instance.currentScreen = instance.uiScreen;
    }
    
}
