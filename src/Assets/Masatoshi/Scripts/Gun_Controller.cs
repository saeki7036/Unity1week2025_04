using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun_Controller : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] Transform BodyTransform;
    [SerializeField] Transform GunTransform;

    [SerializeField] GameObject Bullet_Player;
    [SerializeField] float Bullet_power = 5f;
    Vector2 getDirection => targetTransform.position - transform.position;
    // Start is called before the first frame update
    void Start()
    {
        
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




    void Update()
    {
        LeftClick = Input.GetMouseButton(0);
        if (!LeftClick) RemoveClick = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GunArmRotation();

        if (LeftClick && RemoveClick)
        {
            GunShot();
            RemoveClick = false;
        }
    }
    void GunShot()
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

