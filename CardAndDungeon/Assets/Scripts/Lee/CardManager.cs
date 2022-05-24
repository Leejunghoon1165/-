using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Card> myCards;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform myCardLeft;
    [SerializeField] Transform myCardRight;
    [SerializeField] Camera mainCamera;
    int r = 0;

    List<Item1> itemBuffer;
    List<Item2> itemBuffer2;
    bool onMyCardArea;
    //public static bool onMyCardArea;
    bool isMyCardDrag;
    Card selectCard;

    int myPutCount;


    public Item1 PopItem()
    {
        if (itemBuffer.Count == 0)
            SetupItemBuffer();

        Item1 item1 = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item1;
    }

    public Item2 PopItem2()
    {
        if (itemBuffer2.Count == 0)
            SetupItemBuffer2();
        Item2 item2 = itemBuffer2[0];
        itemBuffer2.RemoveAt(0);
        return item2;
    }

    void SetupItemBuffer()
    {
        itemBuffer = new List<Item1>(100);
        for (int i = 0; i < itemSO.items1.Length; i++)
        {
            Item1 item1 = itemSO.items1[i];
            for (int j = 0; j < item1.percent; j++)
                itemBuffer.Add(item1);
        }


        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item1 temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }
    void SetupItemBuffer2()
    {
        itemBuffer2 = new List<Item2>(100);
        for (int i = 0; i < itemSO.items2.Length; i++)
        {
            Item2 item2 = itemSO.items2[i];
            for (int j = 0; j < item2.percent; j++)
                itemBuffer2.Add(item2);
        }


        for (int i = 0; i < itemBuffer2.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer2.Count);
            Item2 temp = itemBuffer2[i];
            itemBuffer2[i] = itemBuffer2[rand];
            itemBuffer2[rand] = temp;
        }
    }

    private void Start()
    {
        SetupItemBuffer();
        SetupItemBuffer2();
    }
    private void Update()
    {
        if (isMyCardDrag)
            CardDrag();
          DetectCardArea();

    }
   

    public void Carddrow()
    {
        r = Random.Range(0, 2);
        if(myCards.Count <4)
        {
            AddCard(true);
        }

    }

    public void AddCard(bool isMine)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
         //cardObject.transform.parent = a.transform;
        cardObject.transform.parent = mainCamera.transform;

        var card = cardObject.GetComponent<Card>();
        if(r == 0)
        {
            card.Setup(PopItem(), isMine);
        }
        else if(r == 1)
        {
            card.Setup2(PopItem2(), isMine);
        }
               
        if (isMine == true)
        {  
            myCards.Add(card);
        }
        else
            return;

        SetOriginOrder(isMine);
        CardAlignment(isMine); 

    }
    void SetOriginOrder(bool isMine)
    {
        int count = myCards.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = myCards[i];
            //targetCard?.GetComponent<Order>().SetOriginOrder(i);
            targetCard?.GetComponentInChildren<Order>().SetOriginOrder(i);
        }
    }

    public void CardAlignment(bool isMine)
    {
        List<PRS> originCardPRSs = new List<PRS>();
        if (isMine)
        {
            originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * 0.7f);
        }
        var targetCards = myCards;
        for(int i = 0; i< targetCards.Count; i++)
        {
            var targetCard = targetCards[i];
            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
         }
        
    }
    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch(objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            case 4:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;
            if (objCount == 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);

            }


            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }

    public bool TryPutCard(bool isMine)
    {
        if (isMine && myPutCount >= 100)
            return false;
        if (!isMine)
            return false;
        if (selectCard == null)
            return false;

        Card card = selectCard;
        var targetCards = myCards;

        if(EntityManager.Inst.SpawnEntity(isMine, card.item1))
        {
            targetCards.Remove(card);
            card.transform.DOKill();
            DestroyImmediate(card.gameObject);
           if(card.item1.name != null || card.item2.name != null)
            {
                CardSkill(card);
            }
            if(isMine)
            {
                selectCard = null;
                myPutCount++;
            }
            CardAlignment(isMine);
            return true;
        }
        else
        {
            targetCards.ForEach(x => x.GetComponent<Order>().SetMostFrontOrder(false));
            CardAlignment(isMine);
            return false;
        }
    }

    #region MyCard

    public void CardMouseOver(Card card)
    {
        selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(Card card)
    {
            EnlargeCard(false, card);
            selectCard = null;
       

    
    }

    public void CardMouseDown()
    {
        isMyCardDrag = true;
    }

    public void CardMouseUp()
    {
        if(selectCard !=null)
        {
            isMyCardDrag = false;
            if (onMyCardArea)
            {
                EntityManager.Inst.RemoveMyEmptyEntity();
            }    
            else
            {
                TryPutCard(true);
                
            }
                
        }
        else
        {
            TryPutCard(true);
        }
      
            
    }
    void CardDrag()
    {
        if(!onMyCardArea)
        {
            // Vector3 cardPos = new Vector3(selectCard.origin2PRS.pos)
            // selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
            if(selectCard != null)
            {
                Vector3 Cardpos = new Vector3(selectCard.origin2PRS.pos.x, selectCard.origin2PRS.pos.y, -100);
                selectCard.MoveTransform(new PRS(Cardpos, Utils.QI, selectCard.originPRS.scale), false);
                EntityManager.Inst.InsertMyEmptyEntity(Cardpos.x);
            }
            
        }
    }
    public void DetectCardArea()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);  //i번째 터치에 대한 정보
                int index = touch.fingerId; //i번째 터치에 대한 id 값
                Vector3 position = touch.position;  //i번째 터치의 위치

                TouchPhase phase = touch.phase; //i번째 터치의 상태
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 originCard = new Vector3(pos.x, pos.y, -10);
                RaycastHit2D[] hits = Physics2D.RaycastAll(originCard, Camera.main.transform.forward);

                int layer = LayerMask.NameToLayer("MyCardArea");
                onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
            }
        }




        // RaycastHit2D[] hits = Physics2D.RaycastAll(selectCard.origin2PRS.pos, Vector3.forward);

    }


    //카드 확대 
    void EnlargeCard(bool isEnlarge, Card card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.origin2PRS.pos.x, card.origin2PRS.pos.y + 3.5f, -100);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 1.2f), false);

        }
        else //축소
        {
            if (selectCard == null)
                return;

            Vector3 OriginCard = new Vector3(selectCard.originPRS.pos.x, selectCard.originPRS.pos.y, -100);
            selectCard.MoveTransform(new PRS(OriginCard, Utils.QI, selectCard.originPRS.scale), false);
  
            //card.MoveTransform(card.originPRS, false);
        }
        if(card !=null)
            card.GetComponentInChildren<Order>().SetMostFrontOrder(isEnlarge);
    }




    #endregion


    void CardSkill(Card card)
    {
        if(card.item2.name == "")
        {
            switch (card.item1.name)
            {
                case "시공간 폭발":
                    Debug.Log("시공간 폭발");
                    break;

                case "매직 미사일":
                    Debug.Log("매직 미사일");
                    break;

                case "화염의 허리케인":
                    Debug.Log("화염의 허리케인");
                    break;

                case "인탱글":
                    Debug.Log("인탱글");
                    break;

                case "산성 비":
                    Debug.Log("산성 비");
                    break;

                case "대지의 뿔":
                    Debug.Log("대지의 뿔");
                    break;
            }

        }
        else
        {
            switch (card.item2.name)
            {
                case "일반 카드팩":
                    Debug.Log("카드 드로우");
                    break;
            }
        }
       
       
    }

}
