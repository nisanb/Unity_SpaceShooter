using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class Enemy : MonoBehaviour
{
    private float _min = -4f, _max = 4f;

    [SerializeField]
    private float _speed = 4.0f;

    Animator m_Animator;

    private Player _player;
    // Update is called once per frame
    void Create()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is null!");
        }

        m_Animator = GetComponent<Animator>();
        if (m_Animator == null)
        {
            Debug.LogError("Animator is null!");
        }
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
    private void destroy()
    {
        GameObject.Find("Audio_Manager").GetComponent<AudioManager>().playExplosionSound();
        this.m_Animator.SetTrigger("OnEnemyDeath");
        this.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(this.gameObject, 2.8f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is player
        string tag = other.tag;

        m_Animator = GetComponent<Animator>();
        if (m_Animator == null)
        {
            Debug.LogError("Animator is null!");
        }

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.damage();
            }


            destroy();
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
            destroy();

        }

        
    }
}
