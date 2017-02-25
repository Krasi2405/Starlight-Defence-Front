using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityTextDisplayer : MonoBehaviour {

    public GameObject targetObject;
    public string identifier = "identity";

    private AbilityCooldown cooldown;
    private Text text;

	// Use this for initialization
	void Start () {
        AbilityCooldown[] allCooldowns = targetObject.GetComponents<AbilityCooldown>();
        foreach(AbilityCooldown abilityCooldown in allCooldowns)
        {
            if(abilityCooldown.identifier == identifier)
            {
                cooldown = abilityCooldown;
            }
        }

        if(cooldown == null)
        {
            print("Ability text displayer could not find an object with identifier: " + identifier);
        }

        text = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        int cooldownRemaining = (int)Mathf.Round(cooldown.remainingCooldown);
        if (cooldownRemaining > 0)
        {
            text.text = cooldownRemaining.ToString();
        }
        else
        {
            text.text = "";
        }
	}
}
