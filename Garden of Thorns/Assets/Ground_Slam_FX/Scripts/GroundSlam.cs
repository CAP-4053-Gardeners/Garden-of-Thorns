using UnityEngine;
using System.Collections;

public class GroundSlam : MonoBehaviour
{

    public GameObject groundSlamFX;
    public GameObject groundShatterAnim;
    public GameObject cameraShake;

    public AudioSource groundSlamAudio;

    private Animation animQuake;

    void Start()
    {

        groundSlamFX.SetActive(false);
        
        animQuake = groundShatterAnim.GetComponent<Animation>();
        StartCoroutine("StartGroundSlam");
    }


    IEnumerator StartGroundSlam()
    {
        groundSlamAudio.Play();

        yield return new WaitForSeconds(0.1f);

        animQuake["Quake"].speed = 1;    
        groundShatterAnim.GetComponent<Animation>().Play();


        groundSlamFX.SetActive(true);
        
    }


}
