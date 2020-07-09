using System.Collections;
using System.Collections.Generic;
using Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndUIController :
    UIControllerBase<EndUIController> {
    
    [Header("Screens")]
    [SerializeField]
    CanvasGroup endScreen;

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

    [Header("End Screen")]
    [SerializeField]
    TextMeshProUGUI phrase;
    
    void Start() {
       UpdateTotalScore(ChallengeController.totalScore);
    }

    public static void Spotligh() {
        instance.overlay.color = Color.white;
        instance.overlay.texture = instance.spotligth;
        ThorBaseController.EnableVisibility();

        for(int i = 0; i < 5; ++i)
            Instantiate(
                Resources.Load<GameObject>("shine-particle"),
                new Vector3(
                    Random.Range(-1.5f, 1.5f),
                    Random.Range(-2.5f, 2.5f)
                ),
                Quaternion.identity
            );
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

    public static void ShowEndScreen() {
        instance.StartCoroutine(instance.IEShowEndScreen());
    }

    public void GoToMain() {
        LoadingController.LoadScene(0);
    }

    IEnumerator IEShowEndScreen() {
        yield return new WaitForSeconds(2f);
        
        if(ChallengeController.totalScore >= 20)
            phrase.text = "Você recuperou o Mjölnir!";
        else
            phrase.text = "Você foi descoberto!";
        
        ShowScreen(endScreen);
        ChallengeController.ResetChallenge();
    }
}