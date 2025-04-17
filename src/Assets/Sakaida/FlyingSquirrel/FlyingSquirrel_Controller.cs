using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSquirrel_Controller : MonoBehaviour
{
    [SerializeField] SR_Tree sr_Tree;

    [SerializeField] float Score=100;
    SR_ScoreManager scoreManager => SR_ScoreManager.instance;

    Vector2 PlayerDirection;

    public float Speed = 2;
    public float PlayerLookDistance = 0;

    bool Attack = false;

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

    [SerializeField] GameObject Bomb;
    [SerializeField] GameObject MyBomb;
    [SerializeField] GameObject AttackPoint;
    [SerializeField] GameObject Laser;

    public List<GameObject> DieBodys = new List<GameObject>();

    [SerializeField] GameObject DieEffect;
    [SerializeField] GameObject DieEffect2;
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
        Start,
        Move,
        
        Attack,
        BackBranch,
        Die
    }
    public ModeType mode = ModeType.Start;

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


    private void FixedUpdate()
    {
        if (mode == ModeType.Start)
        {
            int RandomPhase = Random.Range(1, 3);

            if (RandomPhase == 1)
            {
                float RandomAngle = Random.Range(-5f, -1f);
                float angleRad = RandomAngle * Mathf.Deg2Rad;
                Vector2 Pos = new Vector2( -20 ,5);
                transform.position = Pos;

                Vector2 AddVelocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));


                rb.velocity = AddVelocity * 2;
            }
            else 
            {
                float RandomAngle = Random.Range(181f, 185f);
                float angleRad = RandomAngle * Mathf.Deg2Rad;
                Vector2 Pos = new Vector2(20, 5);
                transform.position = Pos;

                Vector2 AddVelocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));


                rb.velocity = AddVelocity * 2;
            }
            mode = ModeType.Move;

        }
        else if (mode == ModeType.Move) 
        {
            if (Attack)
            {

            }
            else 
            {
                if (transform.position.x+0.5 > Player.transform.position.x && transform.position.x - 0.5 < Player.transform.position.x) 
                { 
                    Attack = true;

                    GameObject CL_Bomb = Instantiate(Bomb,transform.position,Quaternion.identity);
                    SR_Bomb sR_Bomb = CL_Bomb.GetComponent<SR_Bomb>();

                    sR_Bomb.Target_Ypos = Player.transform.position.y;
                    MyBomb.SetActive(false);

                    audioManager.isPlaySE(AttackClip);
                }
            }
        }

        if (mode == ModeType.Die)
        {
            if (DieFlag)
            {

            }
            else
            {
                DieFlag = true;
                GameObject CL_DieEffect = Instantiate(DieEffect, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.1f);
                audioManager.isPlaySE(DieClip);

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


                    Vector2 direction = transform.position - Player.transform.position;
                    Vector3 T_velocity = new Vector3(
                        Mathf.Cos(T_Angle) * direction.x - Mathf.Sin(T_Angle) * direction.y,
                        Mathf.Sin(T_Angle) * direction.x + Mathf.Cos(T_Angle) * direction.y,
                        0
                    );
                    float RandomVelo = Random.Range(1, 5);
                    DieRB.velocity = T_velocity.normalized * RandomVelo;

                    GameObject CL_DieEffect2 = Instantiate(DieEffect2, transform.position, Quaternion.identity);
                    CL_DieEffect2.transform.up = direction;
                    CL_DieEffect2.transform.Rotate(0, 0, 90);

                    scoreManager.KillEnemy(Score);

                }
            }

        }
    }
}
