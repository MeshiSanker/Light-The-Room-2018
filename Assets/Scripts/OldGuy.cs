using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OldGuy : MonoBehaviour {

    // our "shield" power up
    public bool canDestroy = true;

    Candle destCandle = null;

    private AudioSource footesteps;


    /// <summary>
    /// Helper to calculate a distance on a path. Taken from
    /// https://forum.unity.com/threads/getting-the-distance-in-nav-mesh.315846/
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static float GetPathLength(UnityEngine.AI.NavMeshPath path)
    {
        float lng = 0.0f;

        if ((path.status != UnityEngine.AI.NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
        {
            for (int i = 1; i < path.corners.Length; ++i)
            {
                lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return lng;
    }


    void Start()
    {
        footesteps = GameObject.Find("OldManFootsteps").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update ()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        float velocity = agent.velocity.magnitude;
        if (velocity > 2.0f)
        {
            footesteps.UnPause();
        }
        else
        {
            footesteps.Pause();
        }


        if (destCandle != null)
        {
            // Check if we've reached the destination
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (canDestroy != false)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            destCandle.LightOff();
                            destCandle = null;
                        }
                    }
                }
            }
        }



        // Get all candles that are burning
        Candle[] candles = GameObject.FindObjectsOfType<Candle>().Where(c => c.IsLightOn()).ToArray();
        // For each candle calc distance and figure out the minimum  
        float minDist = float.PositiveInfinity;
        Candle minCandle = null;
        foreach(Candle c in candles)
        {
            UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
            agent.CalculatePath(c.transform.position, path);

            // only of candle is reachable
            if (path.status != UnityEngine.AI.NavMeshPathStatus.PathComplete)
            {
                print("Error: a candle is not reachable by AI");
            }
            else
            {
                float dist = GetPathLength(path);
                if (dist < minDist)
                {
                    minDist = dist;
                    minCandle = c;
                }
            }
        }

        // Set agents destination
        if (minCandle)
        {
            agent.destination = minCandle.transform.position;
            destCandle = minCandle;
        }
    }

    public void ShieldOn()
    {
        canDestroy = false;
        StartCoroutine(SpeedUPPowerDownRutin());
    }

    public IEnumerator SpeedUPPowerDownRutin()
    {
        yield return new WaitForSeconds(5.0f);
        canDestroy = true;
    }


    public void Taunt()
    {
        int randomNum = Random.Range(0, 3);
        AudioSource[] acs = GetComponents<AudioSource>();
        acs[randomNum].Play();
    }
}
