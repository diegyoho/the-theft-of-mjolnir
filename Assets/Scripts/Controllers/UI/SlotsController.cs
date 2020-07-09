using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class SlotsController : SingletonMonoBehaviour<SlotsController> {
    public RectTransform content;
    public Slider slider;
    public List<ItemSlot> slots = new List<ItemSlot>();

    void Start() {
        GameData.ItemsOfType().ForEach(item => {
            item.charmPoints = Random.Range(-3, 4);
            item.funcionalityPoints = Random.Range(-3, 4);
        });
        UpdateSlots(GameData.ItemsOfType());
    }

    void UpdateSlots(List<ItemData> itemsData) {
        
        slots.ForEach(slot => slot.gameObject.SetActive(false));
        
        foreach(ItemData itemData in itemsData) {
            ItemSlot itemSlot = slots.Find(slot =>
                slot.currentItemData.Equals(itemData)
            );

            if(!itemSlot) {
                itemSlot = Instantiate(
                    Resources.Load<GameObject>(
                        "Prefabs/UI/item-slot"
                    ), Vector3.zero,
                    Quaternion.identity,
                    content
                ).GetComponent<ItemSlot>();

                slots.Add(itemSlot);
            }
            itemSlot.transform.localPosition = Vector3.zero;
            itemSlot.gameObject.SetActive(true);
            itemSlot.SetItemData(itemData);
        }
        Canvas.ForceUpdateCanvases();

        GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;

    }

    public void SetFilter(TToMDropdown dropdown) {
        
        int value = 1 << dropdown.value;

        if(dropdown.value == dropdown.options.Count - 1)
            value = -1;

        ItemType i = (ItemType) value;
        UpdateSlots(GameData.ItemsOfType(i));
    }

    public void UpdateSlider(Vector2 normalizedPosition) {
        slider.value = normalizedPosition.x;
    }
}
