using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameflow_Manager : MonoBehaviour
{
    [SerializeField]
    DamageEventController damageEventController;
    [SerializeField]
    Player_ActiveSetting player_ActiveSetting;
    public void GameOver()
    {
        player_ActiveSetting.ActiveFalse();
        
        damageEventController.GameOver();

        SR_ScoreManager.instance.SendUnityRoom();

        Debug.Log("9w(^o^) < puge");
    }
}
