using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float force = 20.0f;
    private GameObject launcher;
    private bool isLaunchable = false;
    private bool launcherIsOnCooldown = false;
    private float cooldownTime = 2.0f;
    private float zConstraint = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        launcher = GameObject.FindWithTag("Launcher");
        transform.position = new Vector3(transform.position.x, transform.position.y, zConstraint);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.Space)){
        //     // Debug.Log("Launcher is building energy");
        // }
        if(Input.GetKeyUp(KeyCode.Space) && isLaunchable && !launcherIsOnCooldown){
            // // Calculate Angle Between the collision point and the player
            // Vector3 dir = collision.contacts[0].point - transform.position;
            // // We then get the opposite (-Vector3) and normalize it
            // dir = -dir.normalized;
            // // And finally we add force in the direction of dir and multiply it by force. 
            // // This will push back the player
            Vector3 dir = launcher.transform.right;
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, launcher.transform.rotation.y / 90, 0) * force, ForceMode.Impulse);
            isLaunchable = false;
            launcherIsOnCooldown = true;
            StartCoroutine(LauncherCooldown(cooldownTime));
            Debug.Log("Force applied");
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Launcher") && !isLaunchable)
        {
            Debug.Log("Collision Detected, launchable value updated");
            isLaunchable = true;
        }
    }

    private IEnumerator LauncherCooldown(float seconds)
    {
        // suspend execution for some seconds
        Debug.Log("Beginning of Cooldown");
        yield return new WaitForSeconds(seconds);
        launcherIsOnCooldown = false;
        Debug.Log("End of Cooldown");

    }
}
