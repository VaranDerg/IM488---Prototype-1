using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PickupDataSO : ScriptableObject
{
    public string PickupName;
    public Color PickupColor;
    public string PopupText;
    public string EndPopupText;
    [Space]
    public float PickupValue;
    [Space]
    public float PickupDuration;
    public List<ScalableStat> StatBuff;
}
