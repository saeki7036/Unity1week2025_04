using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] Transform player_arm_transform;
    [SerializeField] Rigidbody2D rigidbody_;
    [SerializeField] BoxCollider2D boxCollider_;
    [SerializeField] float JumpPower_x = 7f;
    [SerializeField] float JumpPower_y = 10f;
    // Start is called before the first frame update
    void Start()
    {
        CurrentBranch = null;
        BeforeBranch = null;
        OnBranch = false;
    }

    bool OnBranch;

    bool Winp;
    bool Ainp;
    bool Dinp;
    bool Sinp;
    bool Spaceinp;

    GameObject CurrentBranch;
    GameObject BeforeBranch;

    void ReleaseBranch()
    {
        BeforeBranch = CurrentBranch;
        CurrentBranch = null;
    }

    void InputKey()
    {
        Winp = Input.GetKey(KeyCode.W);
        Ainp = Input.GetKey(KeyCode.A);
        Dinp = Input.GetKey(KeyCode.D);
        Sinp = Input.GetKey(KeyCode.S);

        Spaceinp = Input.GetKey(KeyCode.Space);//Down
    }

    void MoveStop() => rigidbody_.velocity = Vector2.zero;
    void GravityStop()=> rigidbody_.gravityScale = 0.0f;
    void GravityEnable() => rigidbody_.gravityScale = 1.0f;
    void Jump()
    {
        GravityEnable();

        float strongInpValue = 1.0f;
        float weekInpValue = 0.3f;

        Vector2 jumpVector = new()
        {
            y = JumpPower_y * 
            (Winp ? strongInpValue : weekInpValue),
            x = JumpPower_x * 
            ((Ainp ? -strongInpValue : 0.0f) + (Dinp ? strongInpValue : 0.0f))
        };

        if(!Winp && Sinp)
            jumpVector.y = -weekInpValue;

        rigidbody_.AddForce(jumpVector, ForceMode2D.Impulse);

        ReleaseBranch();
        OnBranch = false;
    }

    private void Update()
    {
        InputKey();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (OnBranch == true && Spaceinp) Jump();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        
        if (BeforeBranch == collision.gameObject || Spaceinp)
            return;

        MoveStop();
        GravityStop();
        OnBranch = true;
        CurrentBranch = collision.gameObject;    
    }
}
