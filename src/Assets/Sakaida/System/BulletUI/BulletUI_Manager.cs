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
            ChangeBulletUI(0);
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
            anim.Play("リロード", 0, 0);
            /*
            AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);

            if (animState.IsName("発射"))
            {
                anim.Play("リロード", 0, 0);
            }
            */
        }
    }

    public void ChangeBulletUI(int curentNumber) 
    {
        AnimatorStateInfo animState = BulletAnim[curentNumber].GetCurrentAnimatorStateInfo(0);

        if (!animState.IsName("発射"))
        {
            BulletAnim[curentNumber].Play("発射", 0, 0);
        }
        /*
        int i = 0;

        foreach (var anim in BulletAnim) 
        {
            if (i > (5-BulletNumber)-1)
            {

            }
            else 
            {
                AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);

                if (!animState.IsName("発射"))
                {
                    anim.Play("発射", 0, 0);
                }
               
                
            }

            i++;
            
        }*/

    }

    
}
