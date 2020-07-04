using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]
public class BGVideoController : MonoBehaviour {
    
    VideoPlayer videoPlayer;
    
    void Start() {
        videoPlayer =  new GameObject("BG Video Player").AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
        videoPlayer.waitForFirstFrame = false;

        StartCoroutine(PlayVideo());
    }

    void Update() {
        long t = (long) (videoPlayer.frameRate * 6f);
        if(videoPlayer.frame >= t)
            Jump(videoPlayer);
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

    void Jump(VideoPlayer videoPlayer) {
        videoPlayer.Pause();
        videoPlayer.frame = (long) (videoPlayer.frameRate * 2.531f);
        videoPlayer.Play();
    }
}
