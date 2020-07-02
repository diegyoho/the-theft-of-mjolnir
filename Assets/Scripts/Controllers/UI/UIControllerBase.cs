﻿using System.Collections;
using System.Collections.Generic;
using Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIControllerBase<T> :
    SingletonMonoBehaviour<T> where T : UIControllerBase<T> {

    public override void Awake() {
        base.Awake();
    }

    protected CanvasGroup currentScreen;

    protected IEnumerator IEHideScreen(CanvasGroup screen) {
        if(screen) {
            while(screen.alpha > 0) {
                screen.alpha -= Time.deltaTime * 2;
                yield return null;
            }
            
            screen.alpha = 0;
            screen.gameObject.SetActive(false);
        }
    }

    protected IEnumerator IEShowScreen(CanvasGroup screen) {
        if(screen) {
            screen.alpha = 0;
            screen.gameObject.SetActive(true);
        
            while(screen.alpha < 1) {
                screen.alpha += Time.deltaTime * 2;
                yield return null;
            }
        
            screen.alpha = 1;
        }
    }

    protected IEnumerator IEChangeScreen(
        CanvasGroup screen,
        System.Action executeBefore = null,
        System.Action executeAfter = null
    ) {
        if(executeBefore != null)
            executeBefore();

        screen.alpha = 0;
        screen.gameObject.SetActive(false);

        yield return StartCoroutine(
            IEHideScreen(currentScreen)
        );

        currentScreen = screen;
        currentScreen.gameObject.SetActive(true);

        yield return StartCoroutine(
            IEShowScreen(currentScreen)
        );

        if(executeAfter != null)
            executeAfter();
    }
}