using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Activater : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    private void Start()
    {
        if (!Tutorial_Manager.Instance.hasShownTutorial)
        {
            Tutorial_Manager.Instance.hasShownTutorial = true;
            ShowTutorial();

            Time.timeScale = 0f;
        }
    }

    private void ShowTutorial()
    {
        // チュートリアルパネルを表示する処理
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        Time.timeScale = 1f;
    }
}
