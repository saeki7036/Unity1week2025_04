using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun_Controller : MonoBehaviour
{
    [SerializeField] Transform TargetTransform;
    [SerializeField] Transform BodyTransform;
    [SerializeField] Transform GunTransform;

    [SerializeField] GameObject Bullet_Player;
    [SerializeField] float Bullet_power = 5f;
    [SerializeField] int Max_Bullet_Slot = 5;
    [SerializeField] float Rerode_Time = 1f;
    Vector2 getDirection => TargetTransform.position - transform.position;

    int Bullet_Slot;
    // Start is called before the first frame update
    void Start()
    {
        Bullet_Slot = Max_Bullet_Slot;
        IsRerode = false;
    }

    void GunArmRotation()
    {
        // ターゲットへのベクトルを取得
        Vector2 direction = getDirection;

        if (BodyTransform.localScale.x >= 0f)
            direction *= -1;

        // 角度を取得（ラジアン → 度）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    bool LeftClick;
    bool RemoveClick;

    bool IsRerode;


    void Update()
    {
        LeftClick = Input.GetMouseButton(0);

        if (!LeftClick) 
            RemoveClick = true;

        if (Input.GetMouseButtonDown(1))
            GunRerode();     
    }

    void GunRerode()
    {
        if (Bullet_Slot == Max_Bullet_Slot || IsRerode)
            return;
        
        StartCoroutine(WaitOneSecondCoroutine(Rerode_Time));
    }

    private IEnumerator WaitOneSecondCoroutine(float waitTime)
    {
        Debug.Log("待機開始");
        IsRerode = true;

        yield return new WaitForSeconds(waitTime);

        Debug.Log("経過！");
        IsRerode = false;

        Bullet_Slot = Max_Bullet_Slot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GunArmRotation();

        if (LeftClick && RemoveClick)
        {
            GunShot();
        }
    }

    void GunShot()
    {
        if (Bullet_Slot > 0 && IsRerode == false)
        {
            ShotBullet();

            RemoveClick = false;
            Bullet_Slot--;

            if (Bullet_Slot == 0)
            {
                GunRerode();
            }
        }
    }


    void ShotBullet()
    {
        // ターゲットへのベクトルを取得
        Vector2 direction = getDirection;
        // 角度を取得（ラジアン → 度）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion bulletQuaternion = Quaternion.Euler(0,0, angle);

        GameObject gameObject = Instantiate(Bullet_Player, GunTransform.position, bulletQuaternion);

        gameObject.GetComponent<Rigidbody2D>().velocity 
            = getDirection.normalized * Bullet_power;

        Destroy(gameObject, 5);

        /*State s = new Move();
        s.Enter();
        s = new Die();
        s.Enter();*/
    }
}

class State 
{
    public virtual void Enter() { }
    public virtual void Up(State s) { }
    public virtual void Exit() { }
}
class Move: State 
{
    public override void Enter() { Debug.Log("Move"); }
    public override void Up(State s) 
    {
        if (true)
        {
            s = new Die();
        }
    }
}
class Die : State
{
    public override void Enter() { Debug.Log("Die"); }
}

