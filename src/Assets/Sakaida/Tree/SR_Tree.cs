using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Tree : MonoBehaviour
{

    public bool Right = true;
    public bool Left = true;

    public bool SquirrelHole = false;

    public SpriteRenderer TreeImage;
    public SpriteRenderer RightBranceImage;
    public SpriteRenderer LeftBranceImage;

    [SerializeField] GameObject RightBranch;
    [SerializeField] GameObject LeftBranch;
    [SerializeField] GameObject SquirrelHoleObject;
    public GameObject MovePoint;

    // Start is called before the first frame update
    void Start()
    {
        SetBranch();
        
        
    }

    public void SetBranch() 
    {
        if (Right)
        {
            RightBranch.SetActive(true);
        }
        else 
        {
            RightBranch.SetActive(false);
        }

        if (Left)
        {
            LeftBranch.SetActive(true);
        }
        else 
        {
            LeftBranch.SetActive(false);
        }

        if (SquirrelHole)
        {
            SquirrelHoleObject.SetActive(true);
        }
        else 
        {
            SquirrelHoleObject.SetActive(false);
        }
    }
}
