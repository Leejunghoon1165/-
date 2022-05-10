using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer cardIcon;
    [SerializeField] TMP_Text main_name_TMP;
    [SerializeField] TMP_Text sub_name_TMP;
    [SerializeField] TMP_Text text_TMP;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;

    public Item item;
    bool isFront;
    // Start is called before the first frame update
    public void Setup(Item item, bool isFront)
    {
        this.item = item;
        this.isFront = isFront;

        if (this.isFront)
        {
            cardIcon.sprite = this.item.sprite;
            main_name_TMP.text = this.item.name;
            sub_name_TMP.text = this.item.subname.ToString();
            text_TMP.text = this.item.text.ToString();
        }
        else
        {
            card.sprite = cardBack;
            main_name_TMP.text = "";
            sub_name_TMP.text = "";
            text_TMP.text = "";
        }
    }
}
