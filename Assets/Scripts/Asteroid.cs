using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject _explosionPrefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotateObject();
    }

    private void rotateObject()
    {
        // Rotate the object on Z axis
        transform.Rotate(0, 0, 0.25f, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            // Laser destroyed me!
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(other.gameObject);

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            GameObject.Find("SpawnManager").GetComponent<SpawnManager>().startSpawning();

            Destroy(this.gameObject, 0.25f);
        }
    }
}
