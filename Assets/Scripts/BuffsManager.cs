using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsManager : MonoBehaviour
{
    public List<Buffs> AvailableBuffs;
    public List<Buffs> AvailableDebuffs;

    [Range(0, 100)]
    public float BuffChance;

    [Range(0, 100)]
    public float DebuffChance;
}
