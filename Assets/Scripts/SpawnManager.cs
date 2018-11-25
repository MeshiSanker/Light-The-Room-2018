using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] powerUps;

    // Use this for initialization
    void Start () {
        StartCoroutine(PowerUpsSpawnRurine());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartSpawnRutin()
    {
        StartCoroutine(PowerUpsSpawnRurine());
    }

    IEnumerator PowerUpsSpawnRurine()
    {

        Vector3[] powerUpLocations = { new Vector3(-10.0f, 1.0f, 6.2f), new Vector3(5.5f, 0.8f, 7.5f), new Vector3(4.5f, 1.0f, -2.0f) };

        while (true)
        {
            int randomNum = Random.Range(0, 3);
            
            Instantiate(powerUps[randomNum], powerUpLocations[randomNum] , Quaternion.identity);
           
            yield return new WaitForSeconds(10.0f);
        }
        
    }
}
