using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class CameraController : SingletonMonoBehaviour<CameraController> {
    
    [SerializeField]
    int cameraWidth = 0;

    void Start() {
        Miscellaneous.SetCameraOrthographicSizeByWidth(
            GetComponent<Camera>(),
            cameraWidth
        );
    }
}
