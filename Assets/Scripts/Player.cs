using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //bonuses  
    //public bool doubleLight = false;
    public bool isSpeedUp = false;
    //public bool shieldOn = false; 

    [SerializeField]
    private float _speed = 5.0f;

    private AudioSource footesteps;

    //private UIManager _UIManager;

    private SpawnManager _spawnManager;

    OldGuy oldGuy;
    bool meetingLastPass;
    
    //private AudioSource _audioSource;

    // Use this for initialization
    void Start() {
        transform.position = new Vector3(0.5f, 1.0f, 7.5f);

        footesteps = GameObject.Find("BoiFootsteps").GetComponent<AudioSource>();
        oldGuy = GameObject.FindObjectOfType<OldGuy>();
    }

    // Update is called once per frame
    void Update()
    {
         Movement();
    }


    private void Movement()
    {
        Vector3 direction;

        float speedUpFactor = isSpeedUp ? 1.5f : 1.0f;

        float horizontalInput = Input.GetAxis("Horizontal");
        direction = Vector3.right * _speed * horizontalInput * Time.deltaTime * speedUpFactor;

        float varticalInput = Input.GetAxis("Vertical");
        direction = direction + Vector3.forward * _speed * varticalInput * Time.deltaTime * speedUpFactor;


        if (direction == Vector3.zero) {
            footesteps.Pause();
            
        } else {
            footesteps.UnPause();

            //transform.LookAt(transform.position +  direction);
            transform.rotation = Quaternion.LookRotation(-1.0f * direction.normalized);
            transform.Translate(-1.0f * direction, Space.World);
          }
        
        // Limit Z (why?)
        if (transform.position.z > 10.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 10.0f);
        }


        // Handle meeting with old guy - play sound
        if ((transform.position - oldGuy.transform.position).magnitude < 1.8 )
        {
            if (meetingLastPass == false)
            {
                meetingLastPass = true;
                AudioSource ac = GetComponent<AudioSource>();
                ac.Play();
            }
        } else  {
            meetingLastPass = false;
        }
    }

    public void SpeedUpOn()
    {
       isSpeedUp = true;
       StartCoroutine(SpeedUPPowerDownRutin());
    }

    public IEnumerator SpeedUPPowerDownRutin()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedUp = false;
    }

}
