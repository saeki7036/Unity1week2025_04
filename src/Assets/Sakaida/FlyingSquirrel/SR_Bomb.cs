using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Bomb : MonoBehaviour
{

    [SerializeField]Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip BombClip;

    [SerializeField] GameObject Attack;

    public float Target_Ypos = 0;

    public bool Bomb = false;

    public float BombTime = 1;
    float BombCount = 0;

    SR_AudioManager audioManager => SR_AudioManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position.y < Target_Ypos) 
        {
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            Bomb = true;
            animator.Play("”š”­", 0, 0);
        }
        if (Bomb) 
        {
            BombCount += Time.deltaTime;
            if (BombCount > BombTime) 
            {
                audioManager.isPlaySE(BombClip);
                Instantiate(Attack,transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
