using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnPlayerDefeat : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.Listen(eEvent.PlayerLose, PlayerLose);
    }

    void OnDestroy()
    {
        EventManager.Remove(eEvent.PlayerLose, PlayerLose);
    }

    private void PlayerLose()
    {
        gameObject.SetActive(false);
    }
}
