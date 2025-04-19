using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] HP_UIobjects;

    [SerializeField] GameObject gameflowObject;
    static int HP = 3;
    static GameObject[] HP_UI;
    static Gameflow_Manager gameflow;
    private void Start()
    {
        HP = 3;
        HP_UI = HP_UIobjects;
        Debug.Log(HP_UI.Length);
        gameflow = gameflowObject.GetComponent<Gameflow_Manager>();
    }

    public static void TakeDamege()
    {
        if (HP <= 0) return;

        HP--;
        HP_UI[HP].SetActive(false);

        if (HP <= 0) SetGameover();
    }

    public static void TakeFallDamege()
    {
        if (HP <= 0) return;

        foreach (GameObject uiObj in HP_UI)
        uiObj.SetActive(false);

        SetGameover();
    }

    static void SetGameover() =>gameflow.GameOver();
}
