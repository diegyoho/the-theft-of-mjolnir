using System.Collections;
using System.Collections.Generic;
using Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonMonoBehaviour<UIController> {
    [SerializeField]
    GameData gameData;

    [Header("Screens")]
    [SerializeField]
    CanvasGroup chestScreen;
    [SerializeField]
    CanvasGroup clothingScreen;

    [Header("Clothing Screen")]

    CanvasGroup currentScreen;

    void Start() {
        StartCoroutine(
            IEChangeScreen(chestScreen)
        );
    }

    public static void ShowClothingScreen() {
        instance.StartCoroutine(
            instance.IEChangeScreen(instance.clothingScreen)
        );
    }

    public void ShowClothingScreenNonStatic() {
        ShowClothingScreen();
    }

    IEnumerator IEChangeScreen(
        CanvasGroup screen,
        System.Action executeBefore = null, System.Action executeAfter = null
    ) {
        if(executeBefore != null)
            executeBefore();

        screen.alpha = 0;
        screen.gameObject.SetActive(false);

        if(currentScreen) {
            while(currentScreen.alpha > 0) {
                currentScreen.alpha -= Time.deltaTime * 2;
                yield return null;
            }
            currentScreen.gameObject.SetActive(false);
        }

        currentScreen = screen;
        currentScreen.gameObject.SetActive(true);

        while(currentScreen.alpha < 1) {
            currentScreen.alpha += Time.deltaTime * 2;
            yield return null;
        }

        if(executeAfter != null)
            executeAfter();
    }
}