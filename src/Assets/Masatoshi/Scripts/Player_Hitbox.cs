using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�_���[�W���菈��:"+ collision.gameObject);
       
        if (collision.gameObject.layer == LayerMask.NameToLayer("FallOut"))
        {
            HP_Manager.TakeFallDamege();
        }
        else
        {
            Destroy(collision.gameObject);
            HP_Manager.TakeDamege();
        }
    }
}
        
