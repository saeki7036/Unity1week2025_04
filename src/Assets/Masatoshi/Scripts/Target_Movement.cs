using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Movement : MonoBehaviour
{
    [SerializeField] Camera cam;
    
    Vector2 objectHalfSize = new Vector2(0.5f, 0.5f); // オブジェクトの半分のサイズ（BoxColliderなどと一致させると良い）

    private void Start()
    {
        objectHalfSize = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
        Debug.Log(objectHalfSize);
    }

    void FixedUpdate()
    {
        // マウス位置（スクリーン座標）を取得
        Vector3 mouseScreenPos = Input.mousePosition;

        // スクリーン座標 → ワールド座標に変換
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        // ビューポートの最小・最大ワールド座標を計算
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        // 画面端で止まるようにクランプ（オブジェクトサイズを考慮）
        float clampedX = Mathf.Clamp(mouseWorldPos.x, min.x + objectHalfSize.x, max.x - objectHalfSize.x);
        float clampedY = Mathf.Clamp(mouseWorldPos.y, min.y + objectHalfSize.y, max.y - objectHalfSize.y);

        transform.position = new Vector3(clampedX, clampedY, 0f);
    }
}
