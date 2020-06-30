using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class SlotsController : SingletonMonoBehaviour<SlotsController> {
    public RectTransform content;
    public List<ItemSlot> slots = new List<ItemSlot>();

    void Start() {
        UpdateSlots(GameData.ItemsOfType());
        GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    void UpdateSlots(List<ItemData> itemsData) {
        
        slots.ForEach(slot => slot.gameObject.SetActive(false));

        foreach(ItemData itemData in itemsData) {
            ItemSlot itemSlot = slots.Find(slot =>
                slot.currentItemData.name == itemData.name
            );

            if(!itemSlot) {
                itemSlot = Instantiate(
                    Resources.Load<GameObject>(
                        "Prefabs/UI/item-slot"
                    ), Vector2.zero,
                    Quaternion.identity,
                    content
                ).GetComponent<ItemSlot>();

                slots.Add(itemSlot);
            }

            itemSlot.SetItemData(itemData);
        }
    }
}
