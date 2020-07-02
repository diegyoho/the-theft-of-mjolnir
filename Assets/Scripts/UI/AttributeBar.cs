using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttributeBar : MonoBehaviour {
    
    public TextMeshProUGUI value;

    public void UpdateValue(float value) {
        this.value.text = value.ToString();
    }
}
