using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject entityPrefab;
    [SerializeField] List<Card> myEntities;
    [SerializeField] Card myEmptyEntity;
    [SerializeField] Card item2Card;

    const int MAX_ENTITY_COUNT = 100;
    public bool IsFullMyEntities => myEntities.Count >= MAX_ENTITY_COUNT && !ExistMyEmptyEntity;
    bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);
    int MyEmptyEntityIndex => myEntities.FindIndex(x => x == myEmptyEntity);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertMyEmptyEntity(float xPos)
    {
        if (IsFullMyEntities)
            return;
        if (!ExistMyEmptyEntity)
            myEntities.Add(myEmptyEntity);

        Vector3 emptyEntityPos = myEmptyEntity.transform.position;
        emptyEntityPos.x = xPos;
        myEmptyEntity.transform.position = emptyEntityPos;

        int _emptyEntityIndex = MyEmptyEntityIndex;
        myEntities.Sort((entity1, entity2) => entity1.transform.position.x.CompareTo(entity2.transform.position.x));
        //if(MyEmptyEntityIndex != _emptyEntityIndex)
            

    }

    public void RemoveMyEmptyEntity()
    {
      //  myEntities.RemoveAt(MyEmptyEntityIndex);

    }

    public bool SpawnEntity(bool isMine, Item1 item1)
    {

        var entity = entityPrefab.GetComponent<Card>();
        if (isMine)
            myEntities[MyEmptyEntityIndex] = entity;
        entity.isMine = isMine;
        entity.Setup(item1, true);
        return true;

          
    }

}
