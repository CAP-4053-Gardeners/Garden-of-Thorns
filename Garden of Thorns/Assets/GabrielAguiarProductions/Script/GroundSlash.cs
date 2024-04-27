using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    public float speed;
    public float slowDownRate;
    public float detectingDistance;
    public float destroyDelay = 5;
    public int damage;
    public GameObject shieldImpactVFX;

    private Rigidbody rb;
    private bool stopped;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            StartCoroutine(SlowDown());
        }
        else
            Debug.Log("No Rigidbody");

        Destroy(gameObject, destroyDelay);
    }

    private void FixedUpdate()
    {
        if (!stopped)
        {
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectingDistance))
            {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance), Color.red);
        }
    }

    IEnumerator SlowDown ()
    {
        float t = 1;
        while (t > 0)
        {
            rb.velocity = Vector3.Lerp(Vector3.zero, rb.velocity, t);
            t -= slowDownRate;
            yield return new WaitForSeconds(0.1f);
        }

        stopped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Friendly>())
        {
            other.GetComponent<Friendly>().TakeDamage(damage);
            if (other.GetComponent<ShieldEffect>())
            {
                Instantiate(shieldImpactVFX, transform.position, Quaternion.identity).transform.localScale = new Vector3(4,4,4);
                gameObject.GetComponent<AudioSource>().Stop();
                Destroy(gameObject);
            }
        }
    }
}
