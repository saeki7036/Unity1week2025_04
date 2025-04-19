using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageEventController : MonoBehaviour
{

    [SerializeField]Animator animator;
    [SerializeField] Animator CameraAnim;
    [SerializeField] TextMeshProUGUI AllScoreText;
    [SerializeField] SR_ScoreManager scoreManager;
    [SerializeField] EnemySpawnController enemySpawnController;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            GameOver();
        }
    }

    // Update is called once per frame
    public void Damage() 
    {
        animator.Play("ダメージ", 0, 0);
        CameraAnim.Play("敵撃破",0,0);

    }
    public void GameOver() 
    { 
        AllScoreText.text = scoreManager.Score.ToString();
        animator.Play("ゲームオーバー", 0, 0);
        CameraAnim.Play("ゲームオーバー", 0, 0);
        enemySpawnController.mode = EnemySpawnController.Mode.Stay;
        foreach (GameObject enemy in enemySpawnController.Enemys) 
        { 
        Destroy(enemy);
        }
    }
}
