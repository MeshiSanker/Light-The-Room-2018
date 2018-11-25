using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public UnityEngine.UI.Button m_StartButton, m_QuitButton;
    public UnityEngine.UI.Text m_Score;

    // Use this for initialization
    void Start () {
        m_StartButton.onClick.AddListener(StartGame);
        //m_StartButton.onClick.AddListener(OpenAboutPanel);
        m_StartButton.onClick.AddListener(QuitGame);

        if (m_Score != null) {
            if (GlobalScore.IsScoreSetOnce())
            {
                m_Score.text = "Score: " + GlobalScore.Score;
            } else
            {
                m_Score.text = "";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame() {
        // set scene to start game
        print("starT");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
