using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SR_ComboManager : MonoBehaviour
{

    public float ComboTime = 1;
    float ComboCount = 0;

    public int Combo = 0;

    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] Slider ComboLimitSlider;
    [SerializeField] Animator animator;

    [SerializeField] AudioClip ComboFinishClip;
    [SerializeField] AudioClip ComboClip;

    SR_AudioManager audioManager => SR_AudioManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        { 
        plusCombo();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Combo > 0) 
        {
            ComboLimitSlider.value =  (ComboTime - ComboCount) / ComboTime;
            ComboCount += Time.deltaTime;
            if ( ComboCount > ComboTime) 
            { 

                ComboReset();

            }
        
        }

        ComboText.text = Combo.ToString();
        
    }

    void ComboReset() 
    {
        animator.Play("コンボ終了", 0, 0);
        ComboCount = 0;
        Combo = 0;
        audioManager.isPlaySE(ComboFinishClip);
    }
    void plusCombo() 
    {

        animator.Play("コンボ増加",0,0);
        ComboCount = 0;
        Combo++;
        audioManager.isPlaySE(ComboClip);
    }
}
