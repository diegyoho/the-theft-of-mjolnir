using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour {
    
    public new TextMeshProUGUI name;
    public TextMeshProUGUI charm;
    public TextMeshProUGUI funcionality;
    public Image image;
    public ItemData currentItemData;

    public void SetItemData(ItemData itemData) {
        currentItemData = itemData;
        name.text = currentItemData.name;
        image.sprite = currentItemData.sprite;
        
        charm.text =
            $"{ currentItemData.charmPoints.ToString("+0;-#") }";

        funcionality.text =
            $"{ currentItemData.funcionalityPoints.ToString("+0;-#") }";
    }

    public void Click() {
        ThorBaseController.SetItem(currentItemData);
    }
}
