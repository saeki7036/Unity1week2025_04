using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
    [SerializeField] string titleSceneName = "Title_Scene";

    /// <summary>
    /// �w��̃V�[���ɑJ��
    /// </summary>
    public void ChangeScene(string sceneName) => SceneManager.LoadSceneAsync(sceneName);

    /// <summary>
    /// �����V�[���ɍēx�J�ڂ�����B
    /// </summary>
    public void SceneRerode() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

    [SerializeField] float waitTime = 3.0f;
    bool IsChange;

    private void Start()
    {
        IsChange = false;
        if (SceneManager.GetActiveScene().name == titleSceneName)
            Time.timeScale = 1.0f;
    }

    /// <summary>
    /// ���C���Q�[���ɑJ�ڂ�����B
    /// </summary>
    public void ChangeSceneMainGame(string sceneName) 
    {
        if (IsChange) return;

        StartCoroutine(WaitOneSecondCoroutine(waitTime, sceneName));
    }

    private IEnumerator WaitOneSecondCoroutine(float waitTime,string sceneName)
    {
        Debug.Log("�ҋ@�J�n");
        IsChange = true;

        yield return new WaitForSeconds(waitTime);

        Debug.Log("�o�߁I");
        IsChange = false;

        SceneManager.LoadSceneAsync(sceneName);
    }
}
