using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    Text txt;
	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        InvokeRepeating("UpdateCounter", 0.0f, 1.0f);
        GlobalScore.Score = 0;
        if (txt != null)
        {
            txt.text = "Score: " + GlobalScore.Score;
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateCounter()
    {
        // Count all burning candles
        Candle[] candles = GameObject.FindObjectsOfType<Candle>().Where(c => c.IsLightOn()).ToArray();
        GlobalScore.Score += candles.Length;
        if (txt != null)
        {
            txt.text = "Score: " + GlobalScore.Score;
        }
    }

}
