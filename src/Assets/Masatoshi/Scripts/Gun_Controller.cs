using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun_Controller : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] Transform BodyTransform;
    [SerializeField] Transform GunTransform;

    [SerializeField] GameObject Bullet_Player;

    Vector2 getDirection => targetTransform.position - transform.position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GunArmRotation()
    {
        // �^�[�Q�b�g�ւ̃x�N�g�����擾
        Vector2 direction = getDirection;

        if (BodyTransform.localScale.x >= 0f)
            direction *= -1;

        // �p�x���擾�i���W�A�� �� �x�j
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GunArmRotation();

        if (Input.GetMouseButtonDown(0)) GunShot();
    }

    void GunShot()
    {
        GameObject gameObject = Instantiate(Bullet_Player, GunTransform.position, Quaternion.identity);
        gameObject.GetComponent<Rigidbody2D>().velocity = getDirection.normalized;
    }
}
