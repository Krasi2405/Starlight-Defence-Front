using UnityEngine;
using System.Collections;

public class ShieldAbility : MonoBehaviour {

    public GameObject shieldSprite;
    public float duration = 5f;
    public string identifier = "identity";

    private AbilityCooldown cooldown;

    void Start () {
        AbilityCooldown[] allCooldowns = GetComponents<AbilityCooldown>();
        foreach (AbilityCooldown abilityCooldown in allCooldowns)
        {
            if (abilityCooldown.identifier == identifier)
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
	    if(cooldown.canBeUsed && Input.GetKeyDown(KeyCode.E))
        {
            GameObject shield = Instantiate(shieldSprite, gameObject.transform.position, Quaternion.identity) as GameObject;
            shield.transform.parent = gameObject.transform;
            cooldown.Reset();
            Destroy(shield, duration);
        }
	}

    


}
