using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        Debug.Log("��e����");
        HP_Manager.TakeDamege();
        Destroy(collision.gameObject);
    }
}
