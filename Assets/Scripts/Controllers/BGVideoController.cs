using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]
public class BGVideoController : MonoBehaviour {
    
    VideoPlayer[] videoPlayer = new VideoPlayer[2];
    
    void Start() {
        videoPlayer[0] =  new GameObject("Video Player 1").AddComponent<VideoPlayer>();
        videoPlayer[0].playOnAwake = false;
        videoPlayer[0].renderMode = VideoRenderMode.CameraFarPlane;
        videoPlayer[0].targetCamera = Camera.main;
        videoPlayer[0].audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer[0].SetTargetAudioSource(0, GetComponent<AudioSource>());

        videoPlayer[1] =  new GameObject("Video Player 2").AddComponent<VideoPlayer>();
        videoPlayer[1].playOnAwake = false;
        videoPlayer[1].renderMode = VideoRenderMode.CameraFarPlane;
        videoPlayer[1].targetCamera = Camera.main;
        videoPlayer[1].audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer[1].SetTargetAudioSource(0, GetComponent<AudioSource>());
        
        videoPlayer[0].loopPointReached += EndReached;

        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo() {
        
        videoPlayer[0].url = System.IO.Path.Combine (Application.streamingAssetsPath,"bg-intro.mp4");

        videoPlayer[1].url = System.IO.Path.Combine (Application.streamingAssetsPath,"bg-loop.mp4");
        videoPlayer[1].isLooping = true;
        
        videoPlayer[0].Prepare();
        videoPlayer[1].Prepare();
        
        yield return new WaitUntil(() => videoPlayer[0].isPrepared);

        yield return new WaitForSeconds(1f);

        videoPlayer[0].Play();

        yield return new WaitUntil(() => !videoPlayer[0].isPlaying);
        
        yield return new WaitUntil(() => videoPlayer[1].isPrepared);

        Destroy(videoPlayer[0].gameObject);
        
        videoPlayer[1].Play();

        yield return new WaitForSeconds(.5f);

        MainUIController.ShowUI();
    }

    void EndReached(VideoPlayer vp) {
        vp.Pause();
    }
}
