using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class Enemy : MonoBehaviour
{
    private float _min = -4f, _max = 4f;

    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    // Update is called once per frame
    void Create()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        //move down at 4 meters per second
        
        if (transform.position.y <= -3f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 12, 0);
        }

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
   
    }

    private float generateRandomXPosition(float min, float max)
    {
        return Random.Range(min, max);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is player
        string tag = other.tag;
        
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.damage();
            }
            Destroy(this.gameObject);

        }
        else if (other.tag == "Laser")
        {

            Debug.Log("Laser hit! Destroying laser..");
            Laser laser = other.GetComponent<Laser>();
            
            if (laser)
            {
                Destroy(laser.gameObject);
            }

            if (_player != null)
            {
                _player.addKill(10);
            }
            Destroy(this.gameObject);
        }

        
    }
}
