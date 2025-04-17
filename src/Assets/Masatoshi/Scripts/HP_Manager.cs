using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] HP_UIobjects;

    static int HP = 3;
    static GameObject[] HP_UI;

    private void Start()
    {
        HP = 3;
        HP_UI = HP_UIobjects;
        Debug.Log(HP_UI);
    }

    public static void TakeDamege()
    {
        if (HP < 0) return;
        HP--;
        HP_UI[HP].SetActive(false);
    }
}
