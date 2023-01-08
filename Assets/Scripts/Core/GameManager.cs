using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{


    private void Start()
    {
        EventManager.s_Instance.StartListening("StartGame", StartGame);
    }

    void StartGame(Dictionary<string,object> message)
    {

    }
}
