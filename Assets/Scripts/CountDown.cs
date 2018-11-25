using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float time = 90.0f;//Seconds Overall
    public Text countdown; //UI Text Object
    bool canRun = true;
    private GameManager _GameManager;
    GameObject ambientSound;
    AudioSource audio1;
    GameObject ambientSound2;
    AudioSource audio2;


    void Start()
    {
        ambientSound = GameObject.Find("Ambient Sound");
        audio1 = ambientSound.GetComponent<AudioSource>();

        ambientSound2 = GameObject.Find("ClockPauseMusic");
        audio2 = ambientSound2.GetComponent<AudioSource>();
        StartCoundownTimer();
    }

    void StartCoundownTimer()
    {
        if (countdown != null)
        {
            time = 90.0f;
            countdown.text = "Time Left: 01:30:000";
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    void UpdateTimer()
    {
        if (countdown != null && canRun == true)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            string fraction = ((time * 100) % 100).ToString("000");
            countdown.text = "Time Left: " + minutes + ":" + seconds + ":" + fraction;
        }

    }
    public void PuausePower()
    {
        canRun = false;
        // need to stop music

        audio1.Pause();
        audio2.Play();

        StartCoroutine(PausePowerDownRutin());
    }

    public IEnumerator PausePowerDownRutin()
    {
        yield return new WaitForSeconds(10.0f);
        audio1.Play();
        audio2.Pause();
        canRun = true;
    }

    public float GetTime()
    {
        return time;
    }
}