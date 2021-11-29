using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnDefeat : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.Listen(eEvent.PlayerLose, OnPlayerLose);
        EventManager.Listen(eEvent.LoadScene, OnLoadScene);
        Debug.Log("Awake");
    }
    
    void OnDestroy()
    {
        EventManager.Remove(eEvent.PlayerLose, OnPlayerLose);
        EventManager.Remove(eEvent.LoadScene, OnLoadScene);
        Debug.Log("Destroy");

    }

    private void OnLoadScene()
    {
        gameObject.SetActive(false);
    }

    private void OnPlayerLose()
    {
        gameObject.SetActive(true);
        Debug.Log("Player Lose");

    }
}
