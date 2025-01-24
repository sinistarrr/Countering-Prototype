using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float time = 8f;
    [SerializeField] private int numberOfBalls = 10;
    [SerializeField] private Text counterText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText(numberOfBalls);
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
    }

    public void DestroyBall(GameObject ball){
        BallController ballController = ball.GetComponent<BallController>();
        if(numberOfBalls > 0 && !ballController.GetDestroyState()){
            UpdateBallCount(numberOfBalls - 1);
            ballController.DestroyBall();
            StartCoroutine(DestroyBallAfterSeconds(ball, 9));
        }
    }

    public void BallIsInBasket(GameObject ball){
        BallController ballController = ball.GetComponent<BallController>();

        if(!ballController.GetDestroyState()){
            if(numberOfBalls <= 0){
                Instantiate(ballPrefab, new Vector3(12, 40, 11), ballPrefab.transform.rotation);
            }
            UpdateBallCount(numberOfBalls + 10);
            ballController.DestroyBall();
            StartCoroutine(DestroyBallAfterSeconds(ball, 9));
        }
    }

    private void UpdateBallCount(int number){
        numberOfBalls = number;
        UpdateText(numberOfBalls);
    }
    private IEnumerator DestroyBallAfterSeconds(GameObject ball, float seconds)
    {
        // suspend execution for some seconds
        yield return new WaitForSeconds(seconds);
        Destroy(ball);
    }
    
    private void UpdateText(int number){
        counterText.text = "Count : " + number;
    }


}
