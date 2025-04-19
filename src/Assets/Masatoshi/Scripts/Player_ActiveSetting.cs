using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ActiveSetting : MonoBehaviour
{
    [SerializeField] GameObject PlayerObject;
    [SerializeField] float SetFalseCount = 1f;
    private void Start()
    {
        if (PlayerObject == null)
            PlayerObject = GameObject.FindWithTag("Player");
    }

    public void ActiveFalse() => StartCoroutine(WaitOneSecondCoroutine(SetFalseCount));
    private IEnumerator WaitOneSecondCoroutine(float waitTime)
    {
        Debug.Log("�ҋ@�J�n");
        
        yield return new WaitForSeconds(waitTime);

        Debug.Log("�o�߁I");

        PlayerObject.SetActive(false);
       
    }
}
