using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]
public class BGVideoController : MonoBehaviour {
    
    VideoPlayer videoPlayer;
    
    void Start() {
        videoPlayer =  new GameObject("Video Player 1").AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
        videoPlayer.waitForFirstFrame = false;
        videoPlayer.loopPointReached += EndReached;
        StartCoroutine(PlayVideo());
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
            EndReached(videoPlayer);
    }

    IEnumerator PlayVideo() {
        
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"bg-ttom.mp4");
        
        videoPlayer.Prepare();
        
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        yield return new WaitForSeconds(1f);

        videoPlayer.Play();

        yield return new WaitForSeconds(2.531f);

        MainUIController.ShowUI();
    }

    void EndReached(VideoPlayer videoPlayer) {
        videoPlayer.started += Jump;
        videoPlayer.Play();
    }

    void Jump(VideoPlayer videoPlayer) {
        videoPlayer.time = 2.531f;
        videoPlayer.started -= Jump;
    }
}
