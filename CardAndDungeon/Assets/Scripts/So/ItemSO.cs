using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item1
{
    public string name;
    public int attack;
    public Sprite sprite;
    public string subname;
    public string text;
    public float percent;
}
[System.Serializable]
public class Item2
{
    public string name;
    public int number;
    public Sprite sprite;
    public string subname;
    public string text;
    public float percent;
}

[CreateAssetMenu(fileName ="ItemSO", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item1[] items1;
    public Item2[] items2;
}
