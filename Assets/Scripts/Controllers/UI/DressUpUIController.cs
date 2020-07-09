using System.Collections;
using System.Collections.Generic;
using Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DressUpUIController :
    UIControllerBase<DressUpUIController> {
    
    [SerializeField]
    GameData gameData;

    [Header("Screens")]
    [SerializeField]
    CanvasGroup chestScreen;
    [SerializeField]
    CanvasGroup clothingScreen;
    [SerializeField]
    CanvasGroup warningPopup;

    [Header("Clothing Screen")]
    [SerializeField]
    Slider charmBar;
    [SerializeField]
    Slider funcionalityBar;

    [Header("Chalenge")]
    [SerializeField]
    TextMeshProUGUI challengeCharm;
    [SerializeField]
    TextMeshProUGUI challengeFuncionality;
    [SerializeField]
    TextMeshProUGUI warningCharm;
    [SerializeField]
    TextMeshProUGUI warningFuncionality;

    
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

    public static void UpdateAttributes(
        float charm, float funcionality
    ) {
        instance.charmBar.value = charm;
        instance.funcionalityBar.value = funcionality;
    }

    public static void UpdateChallengeAttributes(
        float charm, float funcionality
    ) {
        instance.challengeCharm.text =
        instance.warningCharm.text = $"≥{charm}";

        instance.challengeFuncionality.text =
        instance.warningFuncionality.text = $"≥{funcionality}";
    }

    public static void Warning(bool show = true) {
        instance.warningPopup.gameObject.SetActive(show);
    }
}