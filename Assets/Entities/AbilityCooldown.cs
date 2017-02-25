using UnityEngine;
using System.Collections;

public class AbilityCooldown : MonoBehaviour {

    // TODO NEW ABILITY SYSTEM IDEA
    // Make a ability cooldown script
    // It has static float cooldown and string identifier.
    // Connect each ability script with an ability cooldown script.
    // Easy to display the cooldown everywhere.
    // If there are multiple abilities on an object use identifier.

    public float cooldown = 8;

    public float remainingCooldown { get; private set; }
    public bool canBeUsed { get; private set; }
    public string identifier = "identity";

    void Start () {
        remainingCooldown = cooldown;
        canBeUsed = false;
	}
	

	void Update () {
        if (remainingCooldown > 0)
        {
            remainingCooldown -= Time.deltaTime;
            canBeUsed = false;
        }
        else
        {
            remainingCooldown = 0;
            canBeUsed = true;
        }
    }

    public void Reset()
    {
        remainingCooldown = cooldown;
    }
}
