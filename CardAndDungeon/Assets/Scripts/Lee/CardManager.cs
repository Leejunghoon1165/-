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

    int r = 0;

    List<Item1> itemBuffer;
    List<Item2> itemBuffer2;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            r = Random.Range(0, 2);
           // Debug.Log(r+ "숫자 확인");
            AddCard(true);
        }
            
    }

    void AddCard(bool isMine)
    {
        var cardObject = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
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
}
