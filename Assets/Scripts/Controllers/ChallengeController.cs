using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ChallengeController : SingletonMonoBehaviour<ChallengeController> {

    public static int currentDate = 0;
    public static int targetCharm;
    public static int targetFuncionality;
    int _totalScore;
    public static int totalScore {
        get { return instance._totalScore; }
        set {
            instance._totalScore = value;
            EndUIController.UpdateTotalScore(value);
        }
    }
    public static int dateScore {
        get {
            int charm = (int) Miscellaneous.Map(
                ThorBaseController.charmPoints,
                0, targetCharm,
                1, 5
            );

            int funcionality = (int) Miscellaneous.Map(
                ThorBaseController.funcionalityPoints,
                0, targetFuncionality,
                1, 5
            );

            return (charm + funcionality)/2;
        }
    }

    public List<ItemData> items;

    void Start() {
        DontDestroyOnLoad(gameObject);
        SetupChallenge();
    }

    public void SetupChallenge() {
        targetCharm = Random.Range(5, 11);
        targetFuncionality = Random.Range(5, 11);

        DressUpUIController.UpdateChallengeAttributes(
            targetCharm, targetFuncionality
        );
    }

    public static void GoToDate() {
        instance.items = ThorBaseController.GetItems();

        currentDate++;

        instance.StartCoroutine(instance.IEGoToDate());
    }

    IEnumerator IEGoToDate() {
        LoadingController.LoadScene(3);

        yield return new WaitUntil(() => !LoadingController.isLoading);
        
        ThorBaseController.SetItems(items);
        ThorBaseController.EnableVisibility(false);

        yield return new WaitForSeconds(1f);

        EndUIController.Spotligh();

        yield return new WaitForSeconds(1f);

        EndUIController.StartHeartsAnimation();
    }

    public static void GoToRoom() {
        instance.StartCoroutine(instance.IEGoToRoom());
    }

    IEnumerator IEGoToRoom() {

        yield return new WaitForSeconds(3f);
        
        
        LoadingController.LoadScene(2);

        yield return new WaitUntil(() => !LoadingController.isLoading);
        
        SetupChallenge();
    }

    public static void ResetChallenge() {
        currentDate = 0;
        targetCharm = 0;
        targetFuncionality = 0;
        Destroy(instance.gameObject);
    }
}
