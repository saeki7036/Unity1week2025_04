using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Squirrel_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float Score = 100;
    SR_ScoreManager scoreManager => SR_ScoreManager.instance;
    SR_CameraMove cameraMove => SR_CameraMove.instance;

    [SerializeField] SR_Tree sr_Tree;
    [SerializeField] SR_Tree SaveTree;
    [SerializeField] Animator animator;

    public bool Rare = false;

    Vector2 PlayerDirection;
    Vector2 SaveVelocity;

    public float Speed = 2;
    public float PlayerLookDistance = 0;

    float LostLookCoount = 0;

    float TargetBranceXpos;

    float AttackCount = 0;
    int AttackPhase = 0;
    int AttackNumber = 0;

    Rigidbody2D rb;
    SR_AudioManager audioManager => SR_AudioManager.instance;
    [SerializeField] AudioClip DieClip;
    [SerializeField] AudioClip AttackClip;
    [SerializeField] AudioClip AttackStartClip;
    [SerializeField] AudioClip RareClip;

    [SerializeField] GameObject AnimBody;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject Arm;
    [SerializeField] GameObject AttackPoint;
    [SerializeField] GameObject Laser;

    public List<GameObject> DieBodys = new List<GameObject>();

    [SerializeField] GameObject DieEffect;
    [SerializeField] GameObject DieEffect2;
    [SerializeField] GameObject RareDieEffect3;
    bool DieFlag = false;

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
        BackBranch,
        Die
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

    public enum EnemyType
    {

        Nomal,
        NoAttack,
        Kusotuyo

    }
    public EnemyType enemytype = EnemyType.Nomal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player_Body");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //AI部分はほぼブラックボックス。わからないことは坂井田にきいてくれ！
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
            Arm.SetActive(true);

            ShotMode();

            float PointLookDistanceNow = Vector2.Distance(Player.transform.position, transform.position);
            if (PointLookDistanceNow < PlayerLookDistance *1.5)
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
                    animator.SetInteger("Anim", 0);
                    Arm.SetActive(false);

                    AttackPhase = 0;
                    AttackCount = 0;
                    AttackNumber = 0;
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
        }else if (mode == ModeType.Die) 
        {
            if (DieFlag)
            {
                
            }
            else 
            { 
                DieFlag = true;
            GameObject CL_DieEffect = Instantiate(DieEffect, transform.position, Quaternion.identity);
                if (Rare)
                {
                    Instantiate(RareDieEffect3, transform.position, Quaternion.identity);
                    audioManager.isPlaySE(RareClip);
                }
                cameraMove.Shake();
                Destroy(gameObject, 0.1f);
                audioManager.isPlaySE(DieClip);

                scoreManager.KillEnemy(Score);

                foreach (GameObject dieBody in DieBodys) 
                {
                    GameObject CL_DieBody = Instantiate(dieBody, transform.position, Quaternion.identity);
                    Rigidbody2D DieRB = CL_DieBody.GetComponent<Rigidbody2D>();

                    /*
                    Vector2 randomVELO = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
                    DieRB.velocity = randomVELO.normalized *4;*/
                    DieRB.AddTorque(500);
                    Destroy(CL_DieBody, 5);


                    float T_Angle = (Random.RandomRange(-30, 30)) * Mathf.Deg2Rad;


                    Vector2 direction = transform.position -Player.transform.position;
                    Vector3 T_velocity = new Vector3(
                        Mathf.Cos(T_Angle) * direction.x - Mathf.Sin(T_Angle) * direction.y,
                        Mathf.Sin(T_Angle) * direction.x + Mathf.Cos(T_Angle) * direction.y,
                        0
                    );
                    float RandomVelo = Random.Range(1, 5);
                    DieRB.velocity = T_velocity.normalized * RandomVelo;

                    GameObject CL_DieEffect2 = Instantiate(DieEffect2, transform.position,Quaternion.identity);



                    //CL_DieEffect2.transform.up = direction;
                    //CL_DieEffect2.transform.Rotate(0, 0, 90);
                }
            }
        
        }


    }

    public void ChangeRotate(float x,float y) 
    { 
    
        AnimBody.transform.up = new Vector3(x,y,0);
        AnimBody.transform.Rotate(0, 0, 90);

        if (x == -1)
        {

            AnimBody.transform.localScale = new Vector3(AnimBody.transform.localScale.x, -0.4f, AnimBody.transform.localScale.z);
            Arm.transform.localScale = new Vector3(-2.5f, -2.5f, Arm.transform.localScale.z);

        }
        else 
        {
            AnimBody.transform.localScale = new Vector3(AnimBody.transform.localScale.x, 0.4f, AnimBody.transform.localScale.z);
            Arm.transform.localScale = new Vector3(2.5f, 2.5f, Arm.transform.localScale.z);
        }

    }

    public void ShotMode() 
    {

        animator.SetInteger("Anim", 1);
        if (Player.transform.position.x > transform.position.x)
        {
            ChangeRotate(1, 0);
        }
        else
        {
            ChangeRotate(-1, 0);
        }

        if (AttackPhase == 0 && AttackCount == 0) 
        { 
        audioManager.isPlaySE(AttackStartClip);
        }
        AttackCount += Time.deltaTime;
        if (AttackPhase == 0) 
        {
            
            PlayerDirection = Player.transform.position - transform.position;
            Arm.transform.up = PlayerDirection;
            Arm.transform.Rotate(0, 0, 90);
            Laser.SetActive(true);

            if (AttackCount > 1) 
            {
                
            AttackPhase = 1;
                AttackCount = 0;
            }
        }
        if (AttackPhase == 1) 
        {

            if (AttackCount > 0.3) 
            {
                AttackPhase = 2;
                AttackCount = 0;
            }

        }
        if (AttackPhase == 2) 
        {
            if (enemytype == EnemyType.Nomal)
            {
                if (AttackCount > 0.2)
                {
                    Shot();

                    AttackCount = 0;
                    AttackNumber++;
                }
                if (AttackNumber == 2)
                {
                    AttackNumber = 0;
                    AttackCount = 0;
                    AttackPhase = 3;
                }
            }
            else if (enemytype == EnemyType.Kusotuyo) 
            {
                PlayerDirection = Player.transform.position - transform.position;
                Arm.transform.up = PlayerDirection;
                Arm.transform.Rotate(0, 0, 90);

                if (AttackCount > 0.15)
                {
                    Shot();

                    AttackCount = 0;
                    AttackNumber++;
                }
                if (AttackNumber == 3)
                {
                    AttackNumber = 0;
                    AttackCount = 0;
                    AttackPhase = 3;
                }
            }
        }
        if (AttackPhase == 3) 
        {
            if (AttackCount > 1)
            {
                Laser.SetActive(false);
                AttackPhase = 0;
                AttackCount = 0;
            }
        }

    }
    void Shot() 
    {
        audioManager.isPlaySE(AttackClip);

        GameObject CL_Bullet = Instantiate(Bullet, AttackPoint.transform.position, Quaternion.identity);
        Rigidbody2D CL_RB = CL_Bullet.GetComponent<Rigidbody2D>();
        CL_RB.velocity = PlayerDirection.normalized * 5;

        CL_Bullet.transform.up = PlayerDirection.normalized;
        CL_Bullet.transform.Rotate(0, 0, 90);

        Destroy(CL_Bullet, 3);
    }

    public void MoveSideRight() 
    {
        ChangeRotate(1, 0);
        rb.velocity = transform.right * Speed;
        if (sr_Tree.gameObject.transform.position.x + TargetBranceXpos < transform.position.x) 
        {
            mode = ModeType.Attack;
            rb.velocity = Vector2.zero;
        }
    }
    public void MoveSideLeft()
    {
        ChangeRotate(-1, 0);
        rb.velocity = transform.right * -Speed;
        if (sr_Tree.gameObject.transform.position.x + TargetBranceXpos > transform.position.x)
        {
            mode = ModeType.Attack;
            rb.velocity = Vector2.zero;
        }
    }

    public void MoveSideBeforeSearch() 
    {
        TargetBranceXpos = Random.RandomRange(2,4);
        int BranchNumber = 0;
        if (sr_Tree.Right) { BranchNumber++; }
        if (sr_Tree.Left) { BranchNumber++; }
        if (BranchNumber == 2)
        {
            rb.velocity = Vector2.zero;
            int randomBranch = Random.Range(0, 2);
            if (randomBranch == 1) { branchtype = BranchType.MoveSideRight; } else { branchtype = BranchType.MoveSideLeft; TargetBranceXpos *= -1; }
        }
        else if (BranchNumber == 1)
        {
            rb.velocity = Vector2.zero;
            if (sr_Tree.Right) { branchtype = BranchType.MoveSideRight; }
            if (sr_Tree.Left) { branchtype = BranchType.MoveSideLeft; TargetBranceXpos *= -1; }
        }
        else 
        {
            rb.velocity = SaveVelocity;
            if (SaveTree == sr_Tree) 
            { 
            branchtype = BranchType.Movepoint;
            }
        }
    }
    public void RightOrLeftMove(int i) 
    {
        rb.velocity = transform.right * Speed * i;

        //Debug.Log("横");
        ChangeRotate(i, 0);
    }
    public void UPorDownMove(int i) 
    { 
    rb.velocity = transform.up * Speed * i;
        ChangeRotate(0, i);
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
            SaveVelocity = rb.velocity;
            SaveTree = sr_Tree;
            branchtype = BranchType.MoveSideBefore;
            movetype = MoveType.Wait;
        }
    }
    public void PlayerSearch() 
    {
        //プレイヤーとの距離を計算
        if (enemytype == EnemyType.Nomal || enemytype == EnemyType.Kusotuyo)
        {
            float PlayerLookDistanceNow = Vector2.Distance(Player.transform.position, transform.position);
            if (PlayerLookDistanceNow > PlayerLookDistance)
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
                if (Player.transform.position.y > transform.position.y - 1 && Player.transform.position.y < transform.position.y + 1)
                {
                    mode = ModeType.Branch;
                }
            }
            else
            {
                //攻撃範囲内
                mode = ModeType.Branch;
            }
        }
        else 
        {
            movetype = MoveType.Up;
            if (transform.position.y > 5.5) 
            {
                Destroy(gameObject);
            }
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
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player_Bullet"))
        {
            mode = ModeType.Die;
            Debug.Log("突然の死");
            //Destroy(other.gameObject);
        }
    }
}
