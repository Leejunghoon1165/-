using System;
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
    public PRS origin3PRS;
    public Vector3 originPos;

    private Touch tempTouchs;
    private Vector2 touchedPos;
    public static bool touchOn;
    public bool isMine;
    public bool CheckOne;
    public bool CheckTwo;
    public int i;
    public int j;


    private void Start()
    {


    }

    private void FixedUpdate()
    {
        cardSet();
    }
    private void Update()
    {
        TouchCheck();
        // DetectCardArea();
        


    }

    public void TouchCheck()
    {
        if(Input.touchCount ==1)
        {
            Touch firstTouch = Input.GetTouch(0);
            Vector3 pos1 = Camera.main.ScreenToWorldPoint(firstTouch.position);
            RaycastHit2D[] hits1 = Physics2D.RaycastAll(pos1, Camera.main.transform.forward);
            RaycastHit2D hitInformation1 = Physics2D.Raycast(pos1, Camera.main.transform.forward);

            if (hits1.Length >0)
            {
                for (i = 0; i < hits1.Length; i++)
                {
                    if (hits1[i].collider.tag == "Card")
                    {
                        origin2PRS.pos = pos1;
                        switch (firstTouch.phase)
                        {
                            case TouchPhase.Stationary:
                                touchOn = true;
                                CardManager.Inst.CardMouseOver(hits1[i].collider.gameObject.GetComponent<Card>());
                                break;

                            case TouchPhase.Moved:
                                origin3PRS.pos = pos1;
                                if (isFront)
                                    CardManager.Inst.CardMouseDown();
                                break;

                            case TouchPhase.Ended:
                                if (isFront)
                                {
                                    CheckOne = false;
                                    CheckTwo = false;
                                    CardManager.Inst.CardMouseUp();
                                    if (hits1[i].collider != null)
                                    {

                                        CardManager.Inst.CardMouseExit(hits1[i].collider.gameObject.GetComponent<Card>());
                                    }

                                }
                                break;

                            case TouchPhase.Canceled:
                                if (isFront)
                                {

                                    CardManager.Inst.CardMouseUp();
                                    CardManager.Inst.CardMouseExit(hits1[i].collider.gameObject.GetComponent<Card>());
                                }
                                break;
                        }
                    }

                    if(hitInformation1.collider.tag=="MyCardArea")
                    {
                        CardManager.Inst.CardMouseExit(CardManager.Inst.selectCard);
                    }
                }
            }
            return;
        }
        if(Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            Vector3 pos1 = Camera.main.ScreenToWorldPoint(firstTouch.position);
            Vector3 pos2 = Camera.main.ScreenToWorldPoint(secondTouch.position);

            RaycastHit2D[] hits1 = Physics2D.RaycastAll(pos1, Camera.main.transform.forward);
            RaycastHit2D[] hits2 = Physics2D.RaycastAll(pos2, Camera.main.transform.forward);
            RaycastHit2D hitInformation1 = Physics2D.Raycast(pos1, Camera.main.transform.forward);
            RaycastHit2D hitInformation2 = Physics2D.Raycast(pos2, Camera.main.transform.forward);

            if (hits1.Length > 0 || hits2.Length > 0)
            {
                for (i = 0; i < hits1.Length; i++)
                {
                    if (hits1[i].collider.tag == "Card")
                    {
                        origin2PRS.pos = pos1;
                        switch (firstTouch.phase)
                        {
                            case TouchPhase.Stationary:
                                touchOn = true;
                                CardManager.Inst.CardMouseOver(hits1[i].collider.gameObject.GetComponent<Card>());
                                break;

                            case TouchPhase.Moved:
                                origin3PRS.pos = pos1;
                                if (isFront)
                                    CardManager.Inst.CardMouseDown();
                                break;

                            case TouchPhase.Ended:
                                if (isFront)
                                {
                                    CheckOne = false;
                                    CheckTwo = false;
                                    CardManager.Inst.CardMouseUp();
                                    if (hits2[i].collider != null)
                                    {
                                        CardManager.Inst.CardMouseExit(hits1[i].collider.gameObject.GetComponent<Card>());
                                    }

                                }
                                break;
                            case TouchPhase.Canceled:
                                if (isFront)
                                {

                                    CardManager.Inst.CardMouseUp();
                                    CardManager.Inst.CardMouseExit(hits1[i].collider.gameObject.GetComponent<Card>());
                                }
                                break;
                        }
                    }
                    if (hitInformation1.collider.tag == "MyCardArea")
                    {
                        CardManager.Inst.CardMouseExit(CardManager.Inst.selectCard);
                    }
                }
                for (i = 0; i < hits2.Length; i++)
                {
                    if (hits2[i].collider.tag == "Card")
                    {
                        origin2PRS.pos = pos2;
                        switch (secondTouch.phase)
                        {
                            case TouchPhase.Stationary:
                                touchOn = true;
                                CardManager.Inst.CardMouseOver(hits2[i].collider.gameObject.GetComponent<Card>());
                                break;

                            case TouchPhase.Moved:
                                origin3PRS.pos = pos2;
                                if (isFront)
                                    CardManager.Inst.CardMouseDown();
                                break;

                            case TouchPhase.Ended:
                                if (isFront)
                                {
                                    CheckOne = false;
                                    CheckTwo = false;
                                    CardManager.Inst.CardMouseUp();
                                    if (hits2[i].collider != null)
                                    {

                                        CardManager.Inst.CardMouseExit(hits2[i].collider.gameObject.GetComponent<Card>());
                                    }

                                }
                                break;
                            case TouchPhase.Canceled:
                                if (isFront)
                                {

                                    CardManager.Inst.CardMouseUp();
                                    CardManager.Inst.CardMouseExit(hits2[i].collider.gameObject.GetComponent<Card>());
                                }
                                break;
                                
                        }
                    }
                    if(hitInformation2.collider.tag == "MyCardArea")
                    {
                        CardManager.Inst.CardMouseExit(CardManager.Inst.selectCard);
                    }
                }
            }
        }

      
                //else
                //{
                //    CardManager.Inst.CardMouseExit(CardManager.Inst.selectCard);
                //}



               
          
       
    }

        //if (Input.touchCount > 0)
        //{
        //    for (int i = 0; i < Input.touchCount; i++)
        //    {
        //        Touch touch = Input.GetTouch(i);  //i번째 터치에 대한 정보
        //        int index = touch.fingerId; //i번째 터치에 대한 id 값
        //        Vector3 position = touch.position;  //i번째 터치의 위치

        //        TouchPhase phase = touch.phase; //i번째 터치의 상태
        //        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

        //        RaycastHit2D hitInformation = Physics2D.Raycast(pos, Camera.main.transform.forward);
        //        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Camera.main.transform.forward);
        //        if (hitInformation.collider.tag == "Card")
        //        {
        //            origin2PRS.pos = pos;
        //            switch (touch.phase)
        //            {
        //                case TouchPhase.Stationary:
        //                    touchOn = true;
        //                    CardManager.Inst.CardMouseOver(hitInformation.collider.gameObject.GetComponent<Card>());
        //                    break;

        //                case TouchPhase.Moved:
        //                    origin3PRS.pos = pos;
        //                    if (isFront)
        //                        CardManager.Inst.CardMouseDown();
        //                    break;

        //                case TouchPhase.Ended:
        //                    if (isFront)
        //                    {
        //                        CheckOne = false;
        //                        CheckTwo = false;
        //                        CardManager.Inst.CardMouseUp();
        //                        if (hitInformation.collider !=null)
        //                        {

        //                            CardManager.Inst.CardMouseExit(hitInformation.collider.gameObject.GetComponent<Card>());
        //                        }

        //                    }
        //                    break;
        //                case TouchPhase.Canceled:
        //                    if (isFront)
        //                    {

        //                        CardManager.Inst.CardMouseUp();
        //                        CardManager.Inst.CardMouseExit(hitInformation.collider.gameObject.GetComponent<Card>());
        //                    }
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            for (i = 0; i < hits.Length; i++)
        //            {
        //                if (hits[i].collider.tag == "Card")
        //                {
        //                    CheckOne = true;
        //                }
        //                if (hits[i].collider.tag == "MyCardArea")
        //                {
        //                    CheckTwo = true;
        //                }

        //            }
        //            if (hitInformation.collider.tag=="MyCardArea")
        //            {
        //                    CardManager.Inst.CardMouseExit(CardManager.Inst.selectCard);
        //                    // CardManager.Inst.CardMouseExit(hitInformation.collider.gameObject.GetComponent<Card>());
        //            }

        //            if (touch.phase == TouchPhase.Ended)
        //            {
        //                CardManager.Inst.CardMouseUp();
        //                //CardManager.Inst.CardMouseExit(this);

        //            }
        //        }

        //    }
        //}
 


    public void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin2PRS.pos, Vector3.forward);
        origin2PRS.pos.z = -10;
        int layer = LayerMask.NameToLayer("MyCardArea");
       // CardManager.onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
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
            image_color.color = this.item2.color_image;
        }
        else
        {
            card.sprite = cardBack;
            main_name_TMP.text = "";
            sub_name_TMP.text = "";
            text_TMP.text = "";
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
