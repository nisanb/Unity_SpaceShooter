using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;


    //0 - triple shot
    //1 - speed
    //2 - shield
    [SerializeField]
    private int _type;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -3f) //END OF SCREEN
        {
            Destroy(this.gameObject);
        }

}

    //ontriggercollision
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.tag == "Player")
        {
            Debug.Log("Player collected PowerUp!");
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                switch (_type)
                {
                    case 0:
                        Debug.Log("Collected triple shot powerup");
                        player.activateTripleShot();
                        break;
                    case 1:
                        Debug.Log("Collected speed powerup");
                        player.activateSpeedBoost();
                        break;
                    case 2:
                        Debug.Log("Collected shield powerup");
                        player.activateShield();
                        break;
                    default:
                        Debug.Log("Collected N/A powerup");
                        break;
                }
            }
            

            Destroy(this.gameObject);
        }
    }
    //should only be collectable by player (use tags)
}
