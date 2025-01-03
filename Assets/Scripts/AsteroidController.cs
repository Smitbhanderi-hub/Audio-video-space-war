using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float moveSpeed = 20f;
    private Rigidbody rb;
    private Vector3 randomRotation;
    private float removePositionZ;
   // private AudioSource explosionAudio;


    public ParticleSystem explosion;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //explosionAudio = GetComponent<AudioSource>();
        randomRotation = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
        removePositionZ = Camera.main.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < removePositionZ)
        {
            Destroy(gameObject);
        }

        Vector3 movementVector = new Vector3(0f, 0f, -moveSpeed * Time.deltaTime);
        rb.velocity = movementVector;

        transform.Rotate(randomRotation * Time.deltaTime);
    }
    public void DestroyAsteroid()
    {
        //if (explosionAudio != null)
        //{
        //explosionAudio.Play();
        //}
        
        //destroy game object with a delay
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().OnAsteroidImpact();
            DestroyAsteroid();
        }
    }
}
