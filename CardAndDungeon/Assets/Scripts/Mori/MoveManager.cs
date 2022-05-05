using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class MoveManager : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rigid;

    //타겟
    Transform Player;

    //위아래 움직임
    public int widthMove;
    public int highMove;
    public bool IdleMove;

    //스탯
    public float MovementSpeed;
    public float max_HP;
    public float cur_HP;
    public float AttackRange;
    public int FindRange;
    public float Strengh;

    //플레이어와의 거리 변수
    public float dist;

    //공격변수
    public bool touch;      //근접
    public bool longRange;  //원거리

    //맞을때 색깔 변경하기 위한 변수
    SpriteRenderer sprite;
    
    //좌표위치조정
    float startY, targetY;

    //Astar에 쓰이는 변수
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;
    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    void Awake() {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        //공격변수 초기화
        touch = false;
        longRange = false;

        //Idle 무빙 초기화
        IdleMove = true;

        cur_HP = max_HP;
        dist = 10;
    }

    void Update() {
        Player =  GameObject.FindWithTag("Player").transform;
        targetPos = Vector2Int.RoundToInt(Player.transform.position);
        startPos = Vector2Int.RoundToInt(this.transform.position);

        startY = this.transform.position.y - 0.33f;
        targetY = Player.position.y + 0.4f;

        Vector2 Start = new Vector2(this.transform.position.x, startY);
        Vector2 Target = new Vector2(Player.position.x, targetY);

        dist = Vector2.Distance(Start, Target);

        if(cur_HP <= 0 ){
            anim.SetTrigger("Die");
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
            Destroy(gameObject, 1f);
        }
        
        if(this.gameObject.GetComponent<Spawn>().mob_num == GameObject.Find("Main Camera").GetComponent<TestCamera>().MapNum)
        {
            PathFinding();
            Movement();
        }

        //플레이어 위치에 따라 좌우 반전해서 바라보기
        float x = this.transform.position.x - Player.position.x;
        float y = this.transform.position.x * x;
        if(y < 0)
            transform.eulerAngles = new Vector2(0, 180);
        else
            transform.eulerAngles = new Vector2(0, 0);

    }
    public void PathFinding()
    {
        // NodeArray의 크기 정해주고, isWall, x, y 대입
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }

        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();
        
        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            // 마지막
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();
                /*
                for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                return;
                */
            }

            // ↗↖↙↘
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }
            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;
            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;
            
            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);
            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;
                OpenList.Add(NeighborNode);
            }
        }
    }

    void Movement()
    {   
        if(touch == true || AttackRange >= dist) {
            Attack();
            Debug.Log("att");
        }
        else if(longRange == true) {
            LongRangeAttack();
            Debug.Log("lratt");
        }
        else if(FindRange >= dist) {
            Chase();
            Debug.Log("chase");
        }
        else {
            Debug.Log("idle");
            Idle();
        }
    }

    void Chase()
    {
        if(FinalNodeList.Count != 0) {
            for (int i = 0; i < FinalNodeList.Count - 1 && new Vector2(FinalNodeList[i].x, FinalNodeList[i].y) == startPos; i++)
                {
                    Vector2Int path = new Vector2Int(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y);
                    transform.position = Vector2.MoveTowards(transform.position, path,Time.deltaTime * MovementSpeed);    
                }
        }
        anim.SetTrigger("Walk");
    }
    void Idle()
    {
        if(IdleMove == true)
        {
            StartCoroutine(IdleMoving());
            rigid.velocity = new Vector2(widthMove, highMove);
        }
        if(widthMove != 0 || highMove != 0)
            anim.SetTrigger("Walk");
        else
            anim.SetTrigger("Idle");
    }

    //Idle상태일 때 자동적으로 이동하는 모습 구현을 위한 코루틴함수
    IEnumerator IdleMoving() {
        IdleMove = false;
        widthMove = Random.Range(-1, 2);
        highMove = Random.Range(-1, 2);
        yield return new WaitForSeconds(3f);
        IdleMove = true;
    }

    //공격 모션 실행
    void Attack() { }
    void LongRangeAttack() {
        rigid.velocity = new Vector2(0, 0);
    }

    //피격시 스프라이트를 일시적으로 빨갛게 표시하는 코루틴함수
    IEnumerator HitedColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(.33f);
        sprite.color = Color.white;
    }

    public void TakeDamage(float damage)
    {
        cur_HP = cur_HP - damage;
        StartCoroutine(HitedColor());
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
            touch = true;
        } 
    }
    void OnCollisionExit2D(Collision2D collision)
    {   
        touch = false;
    }
}