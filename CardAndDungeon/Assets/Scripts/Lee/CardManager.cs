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


    List<Item> itemBuffer;

    public Item PopItem()
    {
        if (itemBuffer.Count == 0)
            SetupItemBuffer();

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    void SetupItemBuffer()
    {
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for (int j = 0; j < item.percent; j++)
                itemBuffer.Add(item);
        }


        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    private void Start()
    {
        SetupItemBuffer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            AddCard(true);
    }

    void AddCard(bool isMine)
    {
        var cardObject = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        var card = cardObject.GetComponent<Card>();
        card.Setup(PopItem(), isMine);
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
