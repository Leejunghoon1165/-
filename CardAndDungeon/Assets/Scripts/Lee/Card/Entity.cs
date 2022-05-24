using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Entity : MonoBehaviour
{
    [SerializeField] Item1 item1;
    [SerializeField] Item2 item2;
    [SerializeField] TMP_Text main_name_TMP;
    // Start is called before the first frame update

    public float damage;
    public bool isMine;
    public bool isBossOrEmpty;
    public Vector3 originPos;

    public void setup(Item1 item1)
    {
        damage = item1.attack;
        this.item1 = item1;
    }

    public void setup2(Item2 item2)
    {
        damage = item2.number;
        this.item2 = item2;
    }
    public void MoveTransform(Vector3 pos, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(pos, dotweenTime);
        }
        else
        {
            transform.position = pos;
        }
    }
}
