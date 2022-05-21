using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer cardIcon;
    [SerializeField] TMP_Text main_name_TMP;
    [SerializeField] TMP_Text sub_name_TMP;
    [SerializeField] TMP_Text text_TMP;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;
    [SerializeField] SpriteRenderer card_outlinecolor;
    [SerializeField] SpriteRenderer jewel_color;
    [SerializeField] SpriteRenderer image_color;


    public Item1 item1;
    public Item2 item2;
    bool isFront;
    public PRS originPRS;
    public PRS origin2PRS;

    private Touch tempTouchs;
    private Vector2 touchedPos;
    private bool touchOn;


    private void Start()
    {
        origin2PRS.pos = originPRS.pos;
    }

    private void FixedUpdate()
    {
        cardSet();
    }
    private void Update()
    {
        if(Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);
            touchedPos = Camera.main.ScreenToWorldPoint(touch.position);
               
        }

        Ray2D ray = new Ray2D(touchedPos, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        //if (hit.collider.tag == ")
        //    Debug.Log("Dd");
        //originPRS.pos.x = this.transform.position.x;
        //originPRS.pos.x = Camera.main.transform.position.x + 1;


        // originPRS.pos = 
    }

    void cardSet()
    {
        if (TestCamera.check == true)
            originPRS.pos.x = this.transform.position.x;
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
            card_outlinecolor.color = this.item1.color_outline;
            jewel_color.color = this.item1.color_jewel;
            image_color.color = this.item1.color_image;
            sub_name_TMP.color = this.item1.color_text;
            
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
            sub_name_TMP.text = this.item2.subname.ToString();
            text_TMP.text = this.item2.text.ToString();
            card_outlinecolor.color = this.item2.color_outline;
            jewel_color.color = this.item2.color_jewel;
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
       
    }

    void OnMouseExit()
    {

    }

    void MobileTouch()
    {
        CardManager.Inst.CardMouseDown();

    }

    private void OnMouseDown()
    {
        if (isFront)
        {
            CardManager.Inst.CardMouseOver(this);

        }
    }

    private void OnMouseUp()
    {
        if (isFront)
        {
            CardManager.Inst.CardMouseExit(this);
            CardManager.Inst.CardMouseUp();
        }
            
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
