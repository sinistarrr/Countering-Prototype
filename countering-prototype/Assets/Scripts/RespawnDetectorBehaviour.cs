using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDetectorBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If ball enters the box respawner trigger zone, it'll spawn a new ball
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball")){
            spawnManager.GetComponent<SpawnManager>().SpawnBall(new Vector3(12, 40, 11));
        }
        
    }
}
