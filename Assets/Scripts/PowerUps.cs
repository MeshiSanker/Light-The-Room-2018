using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
    
    [SerializeField]
    private int _powerUpID;
    //0= Speed
    //1= Light
    //2= Time


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            OldGuy oldMan = GameObject.FindObjectOfType<OldGuy>();
            CountDown clock = GameObject.FindObjectOfType<CountDown>();
            if (player != null)
            {
                if (_powerUpID == 0)
                {
                    player.SpeedUpOn();
                }
                else if (_powerUpID == 1)
                {
                    oldMan.ShieldOn();
                }
                else if (_powerUpID == 2)
                {
                    clock.PuausePower();
                }

            }
            Destroy(this.gameObject);
        }
    }

}

