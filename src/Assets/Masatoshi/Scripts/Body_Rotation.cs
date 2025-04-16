using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Body_Rotation : MonoBehaviour
{
    [SerializeField] Transform targetTransform;       

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 localScale_ = transform.localScale;
        float direction = targetTransform.position.x - this.transform.position.x;
       
        if((direction< 0 && localScale_.x < 0) ||
            direction >= 0 && localScale_.x >= 0)
        localScale_.x *=  -1;

        transform.localScale = localScale_;
    }
}
