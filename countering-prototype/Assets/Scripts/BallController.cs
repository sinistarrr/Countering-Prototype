using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class BallController : MonoBehaviour
{
    [SerializeField] private float force = 200.0f;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioSource ballAudioSource;
    [SerializeField] private AudioSource launcherAudioSource;
    private Image circleImage;
    private Text circleText;
    private GameObject launcher;
    private bool isLaunchable = false;
    private bool launcherIsOnCooldown = false;
    private float cooldownTime = 2.0f;
    private float zConstraint = 10.0f;
    private float forceMinRange;
    private float forceMaxRange = 400.0f;
    private bool destroyState = false;
    // Start is called before the first frame update
    void Start()
    {
        launcher = GameObject.FindWithTag("Launcher");
        circleImage = GameObject.FindWithTag("Progress Bar Launcher Image").GetComponent<Image>();
        circleText = GameObject.FindWithTag("Progress Bar Launcher Text").GetComponent<Text>();;
        transform.position = new Vector3(transform.position.x, transform.position.y, zConstraint);
        forceMinRange = force;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space)){
            launcherAudioSource.Play();
            force = forceMinRange;
            StartCoroutine(BuildingForce());
        }
        if(Input.GetKeyUp(KeyCode.Space) && isLaunchable && !launcherIsOnCooldown){
            Debug.Log("launcher.transform.localEulerAngles.y = " + launcher.transform.localEulerAngles.y);
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, launcher.transform.localEulerAngles.y / 90, 0) * force, ForceMode.Impulse);
            isLaunchable = false;
            launcherIsOnCooldown = true;
            StartCoroutine(LauncherCooldown(cooldownTime));
            Debug.Log("Force applied");
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ballAudioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)], 0.1f);
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

    private IEnumerator BuildingForce()
    {
        float forceAdd = 1.0f;
        float fillAmountAdd = forceAdd / (forceMaxRange - forceMinRange);
            
        circleImage.fillAmount = 0;
        circleText.text = 0 + " %";
        while(force < forceMaxRange && isLaunchable){
            circleImage.fillAmount += fillAmountAdd;
            circleText.text = Mathf.RoundToInt(circleImage.fillAmount * 100) + " %";
            force += forceAdd;
            yield return new WaitForSeconds(0.01f);
        }

    }

    public void DestroyBall(){
        destroyState = true;
    }

    public bool GetDestroyState(){
        return destroyState;
    }


}
