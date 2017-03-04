using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public AudioClip shotAudio;
    public AudioClip explosion;
    public int health = 50;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float destroyTime = 5f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 100;
    public Vector3[] shotPositions = { new Vector3(0, 0, 0) };

    private ScoreKeeper scoreKeeper;

    // Use this for initialization
    void Start () {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
	
	// Update is called once per frame
	void Update () {
        float probability = Time.deltaTime * shotsPerSecond;
        if(Random.value < probability)
        {
            Fire();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Fire()
    {
        foreach (Vector3 shotPosition in shotPositions) {
            Vector3 shotPos = new Vector3(transform.position.x + shotPosition.x, transform.position.y + shotPosition.y, 2);
            GameObject shot = Instantiate(projectile, shotPos, Quaternion.identity) as GameObject;
            shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
            Destroy(shot, destroyTime);
        }
        AudioSource.PlayClipAtPoint(shotAudio, transform.position);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player Projectile")
        {
            Destroy(collider.gameObject);
            health -= collider.gameObject.GetComponent<Projectile>().damage;
        }

        

    }

    void Die()
    {
        Destroy(gameObject);
        // TODO explosion animation.
        scoreKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(explosion, transform.position);
    }
}
