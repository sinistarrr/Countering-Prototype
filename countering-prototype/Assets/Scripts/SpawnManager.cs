using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float time = 8f;
    
    [SerializeField] private int numberOfBalls = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Mathf.Approximately(time, Time.timeScale)){
            Time.timeScale = time;
        }
    }

    public void SpawnBall(Vector3 position){
        if(numberOfBalls > 0){
            Instantiate(ballPrefab, position, ballPrefab.transform.rotation);
        }
        else{
            Debug.Log("Game Over!");
        }
    }

    public void DestroyBall(GameObject ball){
        DecreaseBallCount();
        StartCoroutine(DestroyBallAfterSeconds(ball, 9));
    }

    private void DecreaseBallCount(){
        numberOfBalls -= 1;
    }

    private IEnumerator DestroyBallAfterSeconds(GameObject ball, float seconds)
    {
        // suspend execution for some seconds
        yield return new WaitForSeconds(seconds);
        Destroy(ball);
    }
}
