using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ThorBaseController :
    SingletonMonoBehaviour<ThorBaseController> {
    
    [SerializeField]
    Item head, body, legs, feet, accessories;

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
    }
}
