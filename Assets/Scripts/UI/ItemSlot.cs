using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour {
    
    public new TextMeshProUGUI name;
    public Image image;
    public ItemData currentItemData;

    public void SetItemData(ItemData itemData) {
        currentItemData = itemData;
        name.text = currentItemData.name;
        image.sprite = currentItemData.sprite;
    }
}
