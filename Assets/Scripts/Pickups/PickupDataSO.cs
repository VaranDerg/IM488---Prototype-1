using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PickupDataSO : ScriptableObject
{
    public string PickupName;
    public Color PickupColor;
    public string PopupText;
    [Space]
    public float PickupValue;
    public float PickupDuration;
}
