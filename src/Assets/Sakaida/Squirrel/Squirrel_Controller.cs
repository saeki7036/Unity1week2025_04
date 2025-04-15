using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Squirrel_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] SR_Tree sr_Tree;

    public float Speed = 2;
    public float PlayerLookDistance = 0;

    float LostLookCoount = 0;

    float TargetBranceXpos;

    Rigidbody2D rb;

    //プレイヤーが完成次第、自動取得するコードに変更
    [SerializeField] GameObject Player;

    public enum MoveType 
    { 
        Wait,

        Up,
        Down,
    }
   public MoveType movetype = MoveType.Wait;

    public enum ModeType 
    { 
    Move,
    Branch,
    Attack,
    BackBranch
    }
    public ModeType mode = ModeType.Move;

    public enum BranchType
    {
        Movepoint,
        MoveSideBefore,
        MoveSideRight,
        MoveSideLeft,
    }
    public BranchType branchtype = BranchType.Movepoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (mode == ModeType.Move)
        {
            switch (movetype)
            {
                case MoveType.Wait:

                    break;
                case MoveType.Up:
                    UPorDownMove(1);
                    break;
                case MoveType.Down:
                    UPorDownMove(-1);
                    break;

            }
            PlayerSearch();
        }
        else if (mode == ModeType.Branch)
        {

            switch (movetype)
            {
                case MoveType.Wait:

                    break;
                case MoveType.Up:
                    UPorDownMove(1);
                    break;
                case MoveType.Down:
                    UPorDownMove(-1);
                    break;

            }

            switch (branchtype)
            {
                case BranchType.Movepoint:
                    MoveSearch(sr_Tree.MovePoint);
                    break;
                case BranchType.MoveSideBefore:
                    MoveSideBeforeSearch();
                    break;
                case BranchType.MoveSideRight:
                    MoveSideRight();
                    break;
                case BranchType.MoveSideLeft:
                    MoveSideLeft();
                    break;

            }

        }
        else if (mode == ModeType.Attack) 
        {

            float PointLookDistanceNow = Vector2.Distance(Player.transform.position, transform.position);
            if (PointLookDistanceNow < PlayerLookDistance)
            {
                LostLookCoount = 0;
            }
            else 
            { 
                LostLookCoount += Time.deltaTime;
                if (LostLookCoount > 4) 
                {
                    LostLookCoount = 0;
                    mode = ModeType.BackBranch ;
                }
            }

        }
        else if (mode == ModeType.BackBranch) 
        {
            float MovePointDistance = Vector2.Distance(sr_Tree.MovePoint.transform.position, transform.position);
            if (MovePointDistance < 0.1)
            {
                rb.velocity = Vector2.zero;
                mode = ModeType.Move;
                branchtype = BranchType.Movepoint;
            }
            else
            {
                if (sr_Tree.MovePoint.transform.position.x < transform.position.x)
                {
                    RightOrLeftMove(-1);
                }
                else
                {
                    RightOrLeftMove(1);
                }
            }
        }

    }


    public void MoveSideRight() 
    {
        rb.velocity = transform.right * Speed;
        if (sr_Tree.gameObject.transform.position.x + TargetBranceXpos < transform.position.x) 
        {
            mode = ModeType.Attack;
            rb.velocity = Vector2.zero;
        }
    }
    public void MoveSideLeft()
    {
        rb.velocity = transform.right * -Speed;
        if (sr_Tree.gameObject.transform.position.x + TargetBranceXpos > transform.position.x)
        {
            mode = ModeType.Attack;
            rb.velocity = Vector2.zero;
        }
    }

    public void MoveSideBeforeSearch() 
    {
        TargetBranceXpos = Random.RandomRange(1,3);
        int BranchNumber = 0;
        if (sr_Tree.Right) { BranchNumber++; }
        if (sr_Tree.Left) { BranchNumber++; }
        if (BranchNumber == 2)
        {
            int randomBranch = Random.Range(0, 2);
            if (randomBranch == 1) { branchtype = BranchType.MoveSideRight; } else { branchtype = BranchType.MoveSideLeft; TargetBranceXpos *= -1; }
        }
        else
        {
            if (sr_Tree.Right) { branchtype = BranchType.MoveSideRight; }
            if (sr_Tree.Left) { branchtype = BranchType.MoveSideLeft; TargetBranceXpos *=-1; }
        }
    }
    public void RightOrLeftMove(int i) 
    {
        rb.velocity = transform.right * Speed * i;
    }
    public void UPorDownMove(int i) 
    { 
    rb.velocity = transform.up * Speed * i;
    }

    public void MoveSearch(GameObject MovePoint) 
    {
        float PointLookDistanceNow = Vector2.Distance(MovePoint.transform.position, transform.position);
        if (PointLookDistanceNow > 0.1)
        {
            //攻撃範囲外
            if (transform.position.y > MovePoint.transform.position.y)
            {
                movetype = MoveType.Down;
            }
            else
            {
                movetype = MoveType.Up;
            }
        }
        else
        {
            //攻撃範囲内
            rb.velocity = Vector2.zero;
            branchtype = BranchType.MoveSideBefore;
            movetype = MoveType.Wait;
        }
    }
    public void PlayerSearch() 
    { 
    //プレイヤーとの距離を計算
        float PlayerLookDistanceNow = Vector2.Distance(Player.transform.position, transform.position);
        if (PlayerLookDistanceNow > PlayerLookDistance )
        {
            //攻撃範囲外
            if (transform.position.y > Player.transform.position.y)
            {
                movetype = MoveType.Down;
            }
            else
            {
                movetype = MoveType.Up;
            }
        }
        else 
        {
            //攻撃範囲内
            mode = ModeType.Branch;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Tree")) 
        { 
        
            SR_Tree _sr_Tree = other.GetComponent<SR_Tree>();
            if (_sr_Tree != null) 
            { 
            sr_Tree = _sr_Tree;
            }

        }
    }
}
