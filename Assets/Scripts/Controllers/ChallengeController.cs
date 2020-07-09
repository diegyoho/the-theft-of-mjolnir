using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ChallengeController : SingletonMonoBehaviour<ChallengeController> {

    public int currentDate = 0;

    public List<ItemData> items;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void GoToDate() {
        items = ThorBaseController.GetItems();

        currentDate++;

        StartCoroutine(IEGoToDate());
    }

    IEnumerator IEGoToDate() {
        LoadingController.LoadScene(3);

        yield return new WaitUntil(() => !LoadingController.isLoading);
        
        ThorBaseController.SetItems(items);
        ThorBaseController.EnableVisibility(false);
    }
}
