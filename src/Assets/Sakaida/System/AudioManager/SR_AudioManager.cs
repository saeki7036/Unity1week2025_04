using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_AudioManager : MonoBehaviour
{
    public static SR_AudioManager instance;
    [SerializeField] GameObject AudioPlayObj;
    [SerializeField] AudioClip BGM;
    AudioSource BgmSource;
    SR_AudioPlay audioPlay;
    void Start()
    {
        BgmSource = GetComponent<AudioSource>();
        if (BGM != null)
        {
            BgmSource.clip = BGM;
            BgmSource.Play();
        }

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void isPlaySE(AudioClip Clip)
    {
        GameObject CL_AudioPlay = Instantiate(AudioPlayObj);

        SR_AudioPlay audio = CL_AudioPlay.GetComponent<SR_AudioPlay>();

        audio.isCL_PlaySE(Clip);



        //CL_AudioPlay.isStatic = true;
        CL_AudioPlay.SetActive(true);

    }
}
