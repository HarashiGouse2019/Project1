using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isPaused = false;
    private KeyCode pauseKey = KeyCode.Escape;
    
    // Start is called before the first frame update
    
    

    // Update is called once per frame
    void Update()
    {
        OnPauseKeyPress();
        
    }
    void TogglePause(bool Pause)
    {
        isPaused = Pause;
        Debug.Log(isPaused);
    }
    void OnPauseKeyPress()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            switch (isPaused)
            {
                case true:
                    TogglePause(false);
                    return;
                case false:
                    TogglePause(true);
                    return;
            }
        }
    }
}
