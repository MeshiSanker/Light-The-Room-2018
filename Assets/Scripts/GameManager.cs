using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private CountDown _CountDown;
    private float time;
    void Start()
    {
        _CountDown = GameObject.FindObjectOfType<CountDown>();
    }

    // Update is called once per frame
    void Update()
    {
        time = _CountDown.time;
      //  print(time);

        if (time <= 0.0f)
        {
            print("GAME OVER");
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

    }

}


