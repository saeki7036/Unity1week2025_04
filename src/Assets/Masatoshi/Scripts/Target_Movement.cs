using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Movement : MonoBehaviour
{
    [SerializeField] Camera cam;
    
    Vector2 objectHalfSize = new Vector2(0.5f, 0.5f); // �I�u�W�F�N�g�̔����̃T�C�Y�iBoxCollider�Ȃǂƈ�v������Ɨǂ��j

    private void Start()
    {
        objectHalfSize = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
        Debug.Log(objectHalfSize);
    }

    void FixedUpdate()
    {
        // �}�E�X�ʒu�i�X�N���[�����W�j���擾
        Vector3 mouseScreenPos = Input.mousePosition;

        // �X�N���[�����W �� ���[���h���W�ɕϊ�
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        // �r���[�|�[�g�̍ŏ��E�ő像�[���h���W���v�Z
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        // ��ʒ[�Ŏ~�܂�悤�ɃN�����v�i�I�u�W�F�N�g�T�C�Y���l���j
        float clampedX = Mathf.Clamp(mouseWorldPos.x, min.x + objectHalfSize.x, max.x - objectHalfSize.x);
        float clampedY = Mathf.Clamp(mouseWorldPos.y, min.y + objectHalfSize.y, max.y - objectHalfSize.y);

        transform.position = new Vector3(clampedX, clampedY, 0f);
    }
}
