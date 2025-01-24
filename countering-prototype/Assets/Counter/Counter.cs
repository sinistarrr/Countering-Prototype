using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{

    [SerializeField] private GameObject spawnManager;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball")){
            spawnManager.GetComponent<SpawnManager>().BallIsInBasket(other.gameObject);
        }
    }
}
