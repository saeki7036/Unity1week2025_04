using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Title_AnimController : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    [SerializeField] string animText_Junp = "_Junp";

    [SerializeField] GameObject imageObject;
    [SerializeField] Image fedeImage;
    [SerializeField] float fadeDuration = 3.0f; // フェードインにかかる時間
    
    float waitTime = 1f;
   
    private void Start()
    {
        imageObject.SetActive(false);
    }
    public void TitleAnim_Play()
    {
        for (int i = 0; i < animators.Length; i++)
            animators[i].Play(i.ToString() + animText_Junp, 0, 0);

        imageObject.SetActive(true);

        StartCoroutine(WaitOneSecond());
    }

    private IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        Color color = fedeImage.color;
        float time = 0f;

        while (color.a < 1f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Clamp01(time / fadeDuration);
            fedeImage.color = color;
            yield return null;
        }
    }
}

