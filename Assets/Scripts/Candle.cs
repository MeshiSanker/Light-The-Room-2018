using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Candle : MonoBehaviour
{

    [SerializeField]
    public float playerLightingRadius;
    bool isLightOn = false;

    GameObject normalLight;
    GameObject shieldLight;

    AudioSource[] audios;
    OldGuy oldGuy;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        normalLight = transform.Find("NormalLight").gameObject;
        shieldLight = transform.Find("ShieldLight").gameObject;
        normalLight.SetActive(false);
        shieldLight.SetActive(false);

        audios = GetComponents<AudioSource>();
        oldGuy = GameObject.FindObjectOfType<OldGuy>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        

        float pDist = Vector3.Distance(transform.position, player.transform.position);
        
        if ((pDist < playerLightingRadius) && Input.GetKeyDown(KeyCode.Space))
        {
            LightOn();
        }

        if (isLightOn)
        {
            OldGuy oldGuy = GameObject.FindObjectOfType<OldGuy>();


            if (oldGuy.canDestroy)
            {
                normalLight.SetActive(true);
                shieldLight.SetActive(false);
            }
            else
            {
                normalLight.SetActive(false);
                shieldLight.SetActive(true);
            }
        }
        else
        {
            normalLight.SetActive(false);
            shieldLight.SetActive(false);
        }
        //if (Vector3.Distance(light1.transform.position, oldMan.transform.position) <= radus)
        //        LightOff();

    }

    public void LightOn()
    {
        if (isLightOn == false && IsInvoking("_lightOn") == false)
        {
            audios[1].Play();
            Invoke("_lightOn", 0.3f); // due to sfx delay
        }
    }

    public void LightOff()
    {
        if (isLightOn && IsInvoking("_lightOff") == false)
        {
            audios[0].Play();
            Invoke("_lightOff", 0.3f); // due to sfx delay
        }
    }


    void _lightOn()
    {
        isLightOn = true;
    }

    void _lightOff()
    {
        isLightOn = false;

        // Old guy has a chance to taunt 30%
        if (Random.Range(0, 100) > 70)
        {
            Invoke("_askForTaunt", Random.Range(0.5f, 1.0f));
        }
    }
    private void _askForTaunt()
    {
        oldGuy.Taunt();
    }

    public bool IsLightOn()
    {
        return isLightOn;
    }

    private void OnMouseDown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float pDist = Vector3.Distance(transform.position, player.transform.position);
        //print(pDist);

        if (pDist < playerLightingRadius)
        {
            LightOn();
        }
    }

}