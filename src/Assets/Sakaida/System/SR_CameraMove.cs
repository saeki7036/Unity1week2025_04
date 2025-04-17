using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_CameraMove : MonoBehaviour
{

    [SerializeField] float CameraMove_X=3;
    [SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Player.transform.position.x > -CameraMove_X && Player.transform.position.x < CameraMove_X) 
        { 
        transform.position = new Vector3(Player.transform.position.x,0,-10);
        }

    }
}
