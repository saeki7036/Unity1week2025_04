using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SR_ScoreManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] Animator animator;
    [SerializeField] GameObject ScorePlusSpawnPoint;
    [SerializeField] GameObject ScorePlas;
    [SerializeField] GameObject Cancvas;
    [SerializeField]SR_ComboManager comboManager;

    GameObject DellScore;

    float ScoreChangeCount = 0;
    float ScoreMast = 0;
    bool ScoreChange = false;

    public float Score = 0;

    public static SR_ScoreManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            KillEnemy(100);
        }   
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (ScoreChange) 
        {

            ScoreChangeCount += Time.deltaTime;
            if (ScoreChangeCount > 1) 
            {
                ScoreMast = 0;
                ChangeScore();
                ScoreChange = false;
                ScoreChangeCount = 0;
            }
        
        }
    }

    public void ChangeScore() 
    {
        animator.Play("ëùâ¡", 0, 0);
    ScoreText.text = Score.ToString("F0");
    }

    public void KillEnemy(float AddScore) 
    {
        if (DellScore != null) 
        { 
        Destroy(DellScore);
        }

        ScoreChange = true;
        ScoreChangeCount = 0;

        float AllScore = AddScore * (((float)comboManager.Combo/100) +1);
        Score += AllScore;//* ((comboManager.Combo) / 100)
        comboManager.plusCombo();

        GameObject CL_ScorePlus = Instantiate(ScorePlas, ScorePlusSpawnPoint.transform.position, Quaternion.identity);
        CL_ScorePlus.transform.parent = ScorePlusSpawnPoint.transform;

        DellScore = CL_ScorePlus;

        ScoreMast += AllScore;

        SR_ScorePlusTexts ScorePlusText = CL_ScorePlus.GetComponent<SR_ScorePlusTexts>();
        ScorePlusText.Text.text = ScoreMast.ToString("F0");
        Destroy(CL_ScorePlus ,2);
    }
}
