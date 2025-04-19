using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
    [SerializeField] string titleSceneName = "Title_Scene";

    /// <summary>
    /// 指定のシーンに遷移
    /// </summary>
    public void ChangeScene(string sceneName) => SceneManager.LoadSceneAsync(sceneName);

    /// <summary>
    /// 同じシーンに再度遷移させる。
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
    /// メインゲームに遷移させる。
    /// </summary>
    public void ChangeSceneMainGame(string sceneName) 
    {
        if (IsChange) return;

        StartCoroutine(WaitOneSecondCoroutine(waitTime, sceneName));
    }

    private IEnumerator WaitOneSecondCoroutine(float waitTime,string sceneName)
    {
        Debug.Log("待機開始");
        IsChange = true;

        yield return new WaitForSeconds(waitTime);

        Debug.Log("経過！");
        IsChange = false;

        SceneManager.LoadSceneAsync(sceneName);
    }
}
