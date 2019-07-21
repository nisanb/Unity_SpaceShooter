using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioSource _lasershot, _powerup, _explosion;
    void Start()
    {
        
    }

    private void playSound(AudioSource obj)
    {
        AudioSource explosion = Instantiate(obj);
        explosion.transform.parent = this.transform;
        Destroy(explosion.gameObject, 2f);
    }

    public void playExplosionSound()
    {
        playSound(_explosion);
    }

    public void playPowerupSound()
    {
        playSound(_powerup);
    }

    public void playLaserShotSound()
    {
        playSound(_lasershot);
    }
}
