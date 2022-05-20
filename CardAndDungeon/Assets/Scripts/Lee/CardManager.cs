using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject a;
    int r = 0;

    List<Item1> itemBuffer;
    List<Item2> itemBuffer2;

    Card selectCard;

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
        itemBuffer2.RemoveAt(1);
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
        
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    r = Random.Range(0, 2);
        //    AddCard(true);
        //}
       

    }
    public void Carddrow()
    {
        r = Random.Range(0, 2);
        AddCard(true);
    }

    void AddCard(bool isMine)
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
            if(myCards.Count <4)    //배열 4 미만일때만 패에 추가하기 
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

    #region MyCard

    public void CardMouseOver(Card card)
    {
        selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }

    void EnlargeCard(bool isEnlarge, Card card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, 1.6f, -10f);
           // card.MoveTransform(new PRS(, Utils.QI, Vector3.one * 1.2f), false);
             card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 1.2f), false);
            //Debug.Log(card.originPRS.pos.x + "와" + card.originPRS.pos.y);
        }
        else
        {
           card.MoveTransform(card.originPRS, false);
        }
           

        card.GetComponentInChildren<Order>().SetMostFrontOrder(isEnlarge);
    }


    #endregion

}
