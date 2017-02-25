using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public int health = 250;

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy Projectile")
        {
            print("Shield has been hit.");
            Destroy(collider.gameObject);
            health -= collider.gameObject.GetComponent<Projectile>().damage;
        }
        else if (collider.gameObject.tag == "Enemy")
        {
            print("Shield hit by enemy ship.");
            int playerHealthBeforeImpact = health;
            health -= collider.gameObject.GetComponent<Enemy>().health;
            collider.gameObject.GetComponent<Enemy>().health -= playerHealthBeforeImpact * 2;
        }
    }
}
