using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public AudioClip shotAudio;
    public AudioClip explosion;
    public int health = 250;
    public GameObject projectile;
    public float projectileSpeed;
    public float destroyTime = 5f;
    public float playerSpeed = 10f;
    public float padding = 0.5f;
    public bool mobile = false;

    private float xMin = -3.5f;
    private float xMax = 3.5f;
    private float yMin = -3.5f;
    private float yMax = 3.5f;
    private ScoreKeeper scoreKeeper;
    

	// Use this for initialization
	void Start () {

        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        

        // Distance between camera and player.
        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 bottomleft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));;
        Vector3 topright = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        xMin = bottomleft.x + padding;
        xMax = topright.x - padding;
        yMin = bottomleft.y + padding;
        yMax = topright.y - padding * 3;
        if(mobile)
        {
            InvokeRepeating("Fire", 0.00001f, 0.4f);
        }

    }
	
	// Update is called once per frame
	void Update () {

        
        

        if (!mobile) {
            PlayerMovementKeyboard();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InvokeRepeating("Fire", 0.00001f, 0.4f);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                CancelInvoke("Fire");
            }
        }
        else
        {
            PlayerMovementMobile();
        }

        PlasmaGenerator();

        if (health <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy Projectile")
        {
            print("Player has been hit.");
            Destroy(collider.gameObject);
            health -= collider.gameObject.GetComponent<Projectile>().damage;
        }
        else if(collider.gameObject.tag == "Enemy")
        {
            print("Player hit by enemy ship.");
            int playerHealthBeforeImpact = health; 
            health -= collider.gameObject.GetComponent<Enemy>().health;
            collider.gameObject.GetComponent<Enemy>().health -= playerHealthBeforeImpact * 2;
        }
    }

    void Die()
    {
        Destroy(gameObject);
        // TODO explosion animation.
        AudioSource.PlayClipAtPoint(explosion, transform.position);
        Lose();
    }

    void Lose()
    {
        LevelManager manager = GameObject.FindObjectOfType<LevelManager>();
        manager.LoadLevel("Lose Screen"); 
    }

    void Fire()
    {
        Vector3 shotPos = new Vector3(transform.position.x, transform.position.y, 2);
        GameObject shot = Instantiate(projectile, shotPos, Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        Destroy(shot, destroyTime);

        AudioSource.PlayClipAtPoint(shotAudio, transform.position);
    }

    void PlasmaGenerator()
    {
        // Turn on particles when w or up arrow is pressed.
        // Turn off particles when w or up arrow is let go of.
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in particles)
            {
                particle.Stop();
            }
        }
    }

    void PlayerMovementKeyboard()
    {
        Vector3 playerPos = gameObject.transform.position;

        // Get player input for movement.
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerPos.x -= playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerPos.x += playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            playerPos.y -= playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            playerPos.y += playerSpeed * Time.deltaTime;
        }

        // Restrict player to game bounds.
        playerPos.x = Mathf.Clamp(playerPos.x, xMin, xMax);
        playerPos.y = Mathf.Clamp(playerPos.y, yMin, yMax);

        gameObject.transform.position = playerPos;
    }

    void PlayerMovementMobile()
    {
        Vector3 playerPos = gameObject.transform.position;
        /*
        playerPos.x = Input.mousePosition.x;
        playerPos.y = Input.mousePosition.y;
        */

        // playerPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

        playerPos.x += Input.acceleration.x * 0.5f;
        playerPos.y += Input.acceleration.z * 0.5f;

        playerPos.x = Mathf.Clamp(playerPos.x, xMin, xMax);
        playerPos.y = Mathf.Clamp(playerPos.y, yMin, yMax);

        gameObject.transform.position = playerPos;
        


    }
}
