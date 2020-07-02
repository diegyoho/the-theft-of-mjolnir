using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

[RequireComponent(typeof(LoadingUIController))]
public class LoadingController :
    SingletonMonoBehaviour<LoadingController> {

    LoadingUIController uiController;
    
    public override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        uiController = GetComponent<LoadingUIController>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
            LoadScene(1);
    }

    public static void LoadScene(int sceneIndex) {
        instance.StartCoroutine(
            instance.IELoadScene(sceneIndex)
        );
    }

    IEnumerator IELoadScene(int sceneIndex) {
        
        uiController.UpdateLoadingBar(0);
        yield return StartCoroutine(uiController.Show());

        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneIndex);

        while(!loadingScene.isDone) {
            float progress = Mathf.Clamp01(loadingScene.progress / .9f);

            uiController.UpdateLoadingBar(progress);

            yield return null;
        }

        GetComponent<Canvas>().worldCamera = Camera.main;

        yield return new WaitForSeconds(.5f);
        yield return StartCoroutine(uiController.Hide());
    }
}
