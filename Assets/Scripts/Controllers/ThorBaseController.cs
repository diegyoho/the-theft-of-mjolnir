﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ThorBaseController :
    SingletonMonoBehaviour<ThorBaseController> {
    
    [SerializeField]
    List<Item> parts = new List<Item>();

    public static int charmPoints {
        get {
            int total = 0;
            
            instance.parts.ForEach(item => {
                if(item.currentItemData != null)
                    total += item.currentItemData.charmPoints;
            });

            return total;
        }
    }

    public static int funcionalityPoints {
        get {
            int total = 0;
            
            instance.parts.ForEach(item => {
                if(item.currentItemData != null)
                    total += item.currentItemData.funcionalityPoints;
            });

            return total;
        }
    }

    public static List<ItemData> GetItems() {
        List<ItemData> items = new List<ItemData>();
        instance.parts.ForEach(part => items.Add(part.currentItemData));
        
        return items;
    }

    public static void SetItems(List<ItemData> items) {
        items.ForEach(item => SetItem(item));
    }

    public static void SetItem(ItemData itemData) {
        Item item = instance.parts.Find(part => part.type == itemData.type);
        
        if(item) item.SetCurrentData(itemData);

        DressUpUIController.UpdateAttributes(charmPoints, funcionalityPoints);
    }

    public static void ClearItems() {
        foreach(Item item in instance.parts)
            item.SetCurrentData(null);

        DressUpUIController.UpdateAttributes(0, 0);
    }

    public void ClearItemsNonStatic() {
        ClearItems();
    }

    public void ScreenShoot() {
        StartCoroutine(IEScreenShoot());            
    }

    public static void EnableVisibility(bool show = true) {
        if(show) {
            instance.GetComponent<SpriteRenderer>().color = Color.white;
            
            foreach (var part in instance.GetComponentsInChildren<SpriteRenderer>()) {
                part.color = Color.white;
            }
        } else {
            instance.GetComponent<SpriteRenderer>().color = Color.black;
            foreach (var part in instance.GetComponentsInChildren<SpriteRenderer>()) {
                part.color = Color.black;
            }
        }
    }

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void DownloadFile(byte[] array, int byteLength, string fileName);

    IEnumerator IEScreenShoot() {
        int width = (int) ((360/1280f) * Screen.width);
        int height = (int) ((480/720f) * Screen.height);
        int startX = (int) ((740/1280f) * Screen.width);
        int startY = (int) ((240/720f) * Screen.height);
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        Rect rex = new Rect(startX, startY, width, height);
        DressUpUIController.Warning(false);
        yield return new WaitForEndOfFrame();

        tex.ReadPixels(rex, 0, 0);
        tex.Apply();
        DressUpUIController.Warning();

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
