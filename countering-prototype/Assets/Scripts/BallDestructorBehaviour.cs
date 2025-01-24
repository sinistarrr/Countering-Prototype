using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestructorBehaviour : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball")){
            spawnManager.GetComponent<SpawnManager>().DestroyBall(other.gameObject);
        }
        
    }
}
