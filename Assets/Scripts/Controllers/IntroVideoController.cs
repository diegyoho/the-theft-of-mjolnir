using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideoController : MonoBehaviour {
    public GameObject skipButton;
    VideoPlayer videoPlayer;
    
    void Start() {
        videoPlayer = GetComponent<VideoPlayer>();
        
        videoPlayer.loopPointReached += EndReached;

        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo() {
        
        skipButton.SetActive(false);
        
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"intro-ttom.mp4");
        
        videoPlayer.Prepare();
        
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        videoPlayer.Play();

        yield return new WaitForSeconds(5);

        skipButton.SetActive(true);
    }

    void EndReached(VideoPlayer vp) {
        LoadingController.LoadScene(2);
    }

    public void Skip() {
        if(videoPlayer.isPlaying)
            videoPlayer.frame = (long) videoPlayer.frameCount;
    }
}
