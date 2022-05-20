using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer cardIcon;
    [SerializeField] TMP_Text main_name_TMP;
    [SerializeField] TMP_Text sub_name_TMP;
    [SerializeField] TMP_Text text_TMP;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;
    

    public Item1 item1;
    public Item2 item2;
    bool isFront;
    public PRS originPRS;

    private void Update()
    {
        originPRS.pos.x = this.transform.position.x;
       // originPRS.pos = 
    }

    // Start is called before the first frame update
    public void Setup(Item1 item1, bool isFront)
    {
        this.item1 = item1;
        this.isFront = isFront;

        if (this.isFront)
        {
            cardIcon.sprite = this.item1.sprite;
            main_name_TMP.text = this.item1.name;
            sub_name_TMP.text = this.item1.subname.ToString();
            text_TMP.text = this.item1.text.ToString();
        }
        else
        {
            card.sprite = cardBack;
            main_name_TMP.text = "";
            sub_name_TMP.text = "";
            text_TMP.text = "";
        }
    }

    public void Setup2(Item2 item2, bool isFront)
    {
        this.item2 = item2;
        this.isFront = isFront;

        if (this.isFront)
        {
            cardIcon.sprite = this.item2.sprite;
            main_name_TMP.text = this.item2.name;
        }
        else
        {
            card.sprite = cardBack;
            main_name_TMP.text = "";
            sub_name_TMP.text = "";
            text_TMP.text = "";
        }
    }

    void OnMouseOver()
    {
        if (isFront)
            CardManager.Inst.CardMouseOver(this);
    }

    void OnMouseExit()
    {
        if (isFront)
            CardManager.Inst.CardMouseExit(this);
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if(useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }



}
