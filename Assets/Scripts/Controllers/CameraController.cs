using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class CameraController : SingletonMonoBehaviour<CameraController> {
    
    public int cameraWidth;

    void Start() {
        Miscellaneous.SetCameraOrthographicSizeByWidth(
            GetComponent<Camera>(),
            cameraWidth
        );
    }
}
