using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TToMDropdown : TMP_Dropdown {
    RectTransform rt_DropdownList;
    RectTransform rt_DropdownItem;
    protected override GameObject CreateDropdownList(GameObject template) {
        GameObject currentDropdownList = base.CreateDropdownList(template);
        rt_DropdownList = currentDropdownList.GetComponent<RectTransform>();
        rt_DropdownItem = currentDropdownList.GetComponentInChildren<DropdownItem>()
                            .rectTransform;
        
        return currentDropdownList;
    }

    public override void OnPointerClick(PointerEventData eventData) {
        Show();
        
        rt_DropdownList.sizeDelta = new Vector2(
                0, rt_DropdownItem.sizeDelta.y * (options.Count + 1)
            );

        captionText.GetComponent<Canvas>().sortingOrder =
            rt_DropdownList.GetComponent<Canvas>().sortingOrder;
    }
}
