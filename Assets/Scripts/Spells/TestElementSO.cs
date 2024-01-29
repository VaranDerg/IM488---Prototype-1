using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TestElementSO : ScriptableObject
{
    public string ElementName;
    public Color ElementColor;
    public Sprite ElementIcon;
    [Space]
    public GameObject LoopingParticles;
    public GameObject BurstParticles;
    public string SoundEffectName;
}