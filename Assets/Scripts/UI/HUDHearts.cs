using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHearts : MonoBehaviour {
    
    [SerializeField]
    GameObject text;
    int currentHeart = 0;

    public void CheckCurrentHeart() {
        ++currentHeart;

        if(currentHeart == ChallengeController.dateScore) {
            GetComponent<Animator>().speed = 0;
            EndUIController.UpdateDateScore(ChallengeController.dateScore);
            ChallengeController.totalScore += ChallengeController.dateScore;
            text.SetActive(true);
            if(ChallengeController.currentDate < 5)
                ChallengeController.GoToRoom();
            else
                EndUIController.ShowEndScreen();
        }
    }
}
