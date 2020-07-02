using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ThorBaseController :
    SingletonMonoBehaviour<ThorBaseController> {
    
    [SerializeField]
    Item head, body, legs, feet, accessories;

    public static int charmPoints {
        get {
            return (
                instance.head.currentItemData.charmPoints +
                instance.body.currentItemData.charmPoints +
                instance.legs.currentItemData.charmPoints +
                instance.feet.currentItemData.charmPoints +
                instance.accessories.currentItemData.charmPoints
            );
        }
    }

    public static int funcionalityPoints {
        get {
            return (
                instance.head.currentItemData.funcionalityPoints +
                instance.body.currentItemData.funcionalityPoints +
                instance.legs.currentItemData.funcionalityPoints +
                instance.feet.currentItemData.funcionalityPoints +
                instance.accessories.currentItemData.funcionalityPoints
            );
        }
    }

    public static void SetItem(ItemData itemData) {
        switch(itemData.type) {
            case ItemType.Head:
                instance.head.SetCurrentData(itemData);
            break;
            case ItemType.Body:
                instance.body.SetCurrentData(itemData);
            break;
            case ItemType.Legs:
                instance.legs.SetCurrentData(itemData);
            break;
            case ItemType.Feet:
                instance.feet.SetCurrentData(itemData);
            break;
            case ItemType.Accessories:
                instance.accessories.SetCurrentData(itemData);
            break;
        }

        UIController.UpdateAttributes(charmPoints, funcionalityPoints);
    }

    public static void ClearItems() {
        instance.head.SetCurrentData(null);
        instance.body.SetCurrentData(null);
        instance.legs.SetCurrentData(null);
        instance.feet.SetCurrentData(null);
        instance.accessories.SetCurrentData(null);
        UIController.UpdateAttributes(0, 0);
    }

    public void ClearItemsNonStatic() {
        ClearItems();
    }

    public void ScreenShoot() {
        StartCoroutine(IEScreenShoot());            
    }

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void DownloadFile(byte[] array, int byteLength, string fileName);

    IEnumerator IEScreenShoot() {
        int width = (int) ((360/1280f) * Screen.width);
        int height = (int) ((500/720f) * Screen.height);
        int startX = (int) ((740/1280f) * Screen.width);
        int startY = (int) ((220/720f) * Screen.height);
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
 
        Rect rex = new Rect(startX, startY, width, height);

        yield return new WaitForEndOfFrame();

        tex.ReadPixels(rex, 0, 0);
        tex.Apply();
 
        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
    #if UNITY_EDITOR

        Destroy(tex);
 
        System.IO.File.WriteAllBytes(Application.dataPath + $"{System.DateTime.Now.Ticks}-ORdM.png", bytes);
    #elif UNITY_WEBGL
        string customFileName = $"{System.DateTime.Now.Ticks}-ORdM.png";
        DownloadFile(bytes, bytes.Length, customFileName);
    #endif
    }
}
