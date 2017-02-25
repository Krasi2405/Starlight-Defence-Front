using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityImageDisplayer : MonoBehaviour {

    public GameObject targetObject;
    public string identifier = "identity";

    private AbilityCooldown cooldown;
    private Image image;

	void Start () {
	    image = gameObject.GetComponent<Image>();

        AbilityCooldown[] allCooldowns = targetObject.GetComponents<AbilityCooldown>();
        foreach (AbilityCooldown abilityCooldown in allCooldowns)
        {
            if (abilityCooldown.identifier == identifier)
            {
                cooldown = abilityCooldown;
            }
        }

        if (cooldown == null)
        {
            print("Ability image displayer could not find an object with identifier: " + identifier);
        }
    }
	

	void Update () {
        if(cooldown.canBeUsed)
        {
            // Change alpha of image.
            Color color = image.color;
            color.a = 250;
            image.color = color;
        }
        else
        {
            // Change alpha of image.
            Color color = image.color;
            color.a = 0;
            image.color = color;
        }
	}
}
