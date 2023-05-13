using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemData",menuName = "Market/Item")]
public class item : ScriptableObject
{
    public string Name = "Item";
    public Sprite Icon;
    public int Price;
}
