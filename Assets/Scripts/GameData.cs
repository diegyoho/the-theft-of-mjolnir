using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[System.Flags]
public enum ItemType {
    Head = 1,
    Body = 2,
    Legs = 4,
    Feet = 8,
    Accessories = 16
}

[System.Serializable]
public class ItemData : IEquatable<ItemData> {
    public string name;
    public ItemType type;
    public Sprite sprite;
    public Vector2 spritePosition;

    public ItemData(
        string name,
        ItemType type,
        Sprite sprite,
        Vector2 spritePosition
    ) {
        SetItemData(
            name,
            type,
            sprite,
            spritePosition
        );
    }

    public void SetItemData(
        string name,
        ItemType type,
        Sprite sprite,
        Vector2 spritePosition
    ) {
        this.name = name;
        this.type = type;
        this.sprite = sprite;
        this.spritePosition = spritePosition;
    }

    public void SetItemData(ItemData other) {
        SetItemData(
            other.name,
            other.type,
            other.sprite,
            other.spritePosition
        );
    }

    public bool Equals(ItemData other) {
        if (other == null)
            return false;

        return this.sprite.name == other.sprite.name;
    }
}

[System.Serializable]
public class AudioClipInfo {
    public string name;
    public AudioClip clip;
}

[CreateAssetMenu(fileName = "GameData", menuName = "TToM/GameData")]
public class GameData : SingletonScriptableObject<GameData> {
    
    [SerializeField]
    List<ItemData> items = new List<ItemData>();
    [SerializeField]
    List<AudioClipInfo> audioClipInfos = new List<AudioClipInfo>();

    public static List<ItemData> ItemsOfType(ItemType type = ~((ItemType) 0)) {
        return instance.items.FindAll(item => (item.type & type) != (ItemType) 0);
    }

    public static AudioClip GetAudioClip(string name) {
        AudioClipInfo audioClipInfo = instance.audioClipInfos.Find(
            aci => aci.name == name
        );

        if(audioClipInfo != null)
            return audioClipInfo.clip;

        return null;
    }

#if UNITY_EDITOR
    public static ItemData SetItemData(ItemData itemData) {
        if(!instance.items.Contains(itemData)) {
            instance.items.Add(itemData);
        } else {
            (instance.items.Find(item => item.Equals(itemData)))
            .SetItemData(
                itemData
            );
        }

        return itemData;
    }

    public static ItemData GetItemData(Sprite sprite) {
        return instance.items.Find(item => item.sprite == sprite);
    }
#endif
}
