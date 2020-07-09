using System.Collections;
using System.Collections.Generic;
using Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndUIController :
    UIControllerBase<EndUIController> {
    
    [Header("HUD Screen")]
    [SerializeField]
    Animator hudHearts;
    [SerializeField]
    RawImage overlay;
    [SerializeField]
    Texture spotligth;
    [SerializeField]
    TextMeshProUGUI totalScore;
    [SerializeField]
    TextMeshProUGUI dateScore;
    
    void Start() {
       UpdateTotalScore(ChallengeController.totalScore);
    }

    public static void Spotligh() {
        instance.overlay.color = Color.white;
        instance.overlay.texture = instance.spotligth;
        ThorBaseController.EnableVisibility();
    }

    public static void StartHeartsAnimation() {
        instance.hudHearts.SetInteger("hearts", 5);
    }

    public static void UpdateTotalScore(int value) {
        if(instance) {
            instance.totalScore.text = value.ToString();
            instance.totalScore.GetComponentInParent<Animator>()
                .SetTrigger("pulse");
        }
    }

    public static void UpdateDateScore(int value) {
        if(instance)
            instance.dateScore.text = $"+{value.ToString()}";
    }
}