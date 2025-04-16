using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_AudioPlay : MonoBehaviour
{
    [SerializeField] AudioSource Asource;
    void Update()
    {
        if (Asource != null)
        {
            if (!Asource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
        else { Debug.Log("�����˂�"); }
    }
    public void isCL_PlaySE(AudioClip Clip)
    {
        Debug.Log(Clip);
        Asource.clip = Clip;
        Asource.Play();
    }
}
