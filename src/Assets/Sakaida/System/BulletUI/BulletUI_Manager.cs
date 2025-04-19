using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUI_Manager : MonoBehaviour
{

    public List <Animator> BulletAnim = new List <Animator> ();

    public int BulletNumber = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            ChangeBulletUI();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            BulletRelode();
        }
    }

    public void BulletRelode() 
    {
        foreach (var anim in BulletAnim)
        {
            AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);

            if (animState.IsName("”­ŽË"))
            {
                anim.Play("ƒŠƒ[ƒh", 0, 0);
            }
        }
    }

    public void ChangeBulletUI() 
    {

        int i = 0;

        foreach (var anim in BulletAnim) 
        {
            if (i > (5-BulletNumber)-1)
            {

            }
            else 
            {
                AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);

                if (!animState.IsName("”­ŽË"))
                {
                    anim.Play("”­ŽË", 0, 0);
                }
               
                
            }

            i++;
            
        }

    }

    
}
