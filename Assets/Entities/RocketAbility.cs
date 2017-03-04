using UnityEngine;
using System.Collections;

public class RocketAbility : MonoBehaviour {

    public GameObject rocketSprite;
    public Vector3[] rocketPositions;
    public float projectileSpeed = 10f;
    public float destroyTime = 5f;
    public string identifier = "identity";
    public AudioClip launchRocketSound;
    public bool mobile = false;

    private AbilityCooldown cooldown;
    
    void Start()
    {
        AbilityCooldown[] allCooldowns = GetComponents<AbilityCooldown>();
        foreach(AbilityCooldown abilityCooldown in allCooldowns)
        {
            if(abilityCooldown.identifier == identifier)
            {
                cooldown = abilityCooldown;
                break;
            }
        }

        if (cooldown == null)
        {
            print("An ability could not find an object with identifier: " + identifier);
        }
    }

	void Update () {
        if (!mobile)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && cooldown.canBeUsed)
            {
                ActivateAbility();
                cooldown.Reset();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2 && cooldown.canBeUsed)
            {
                ActivateAbility();
                cooldown.Reset();
            }
            
        }
	}

    void ActivateAbility()
    {
        foreach(Vector3 position in rocketPositions)
        {
            AudioSource.PlayClipAtPoint(launchRocketSound, transform.position);
            Fire(position);
        }
    }

    void Fire(Vector3 position)
    {
        Vector3 shipPosition = gameObject.transform.position;
        Vector3 shotPos = new Vector3(shipPosition.x + position.x, shipPosition.y + position.y, 0);
        GameObject shot = Instantiate(rocketSprite, shotPos, Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);

        Destroy(shot, destroyTime);
    }

}
