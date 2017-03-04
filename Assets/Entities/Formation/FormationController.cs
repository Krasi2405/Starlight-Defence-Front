using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

    public GameObject enemyPrefab;
    public float speed = 2f;
    public float width = 10f;
    public float height = 5f;
    public float spawnDelay = 0.5f;

    public bool respawn = false;

    private float xMin = -3.5f;
    private float xMax = 3.5f;
    private float padding = 0.5f;

    

    // Use this for initialization
    void Start () {
        SpawnUntilFull();

        // Set game space boundaries.
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 bottomleft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)); ;
        Vector3 topright = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        padding = width / 2;

        xMin = bottomleft.x + padding;
        xMax = topright.x - padding;

    }
    
    void Update () {
        Move();
        if(AllMembersDead())
        {
            if (respawn)
            {
                SpawnUntilFull();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        

	}

    void Move()
    {
        Vector3 formationPos = gameObject.transform.position;

        // Reverse direction if formation is outside of space
        // And formation is still going outside the map.
        if ((formationPos.x <= xMin && speed < 0) || (formationPos.x >= xMax && speed > 0))
        {
            speed *= -1;
        }

        // Move Horizontally
        formationPos.x += speed * Time.deltaTime;

        // Move Down
        // formationPos.y -= Mathf.Abs(speed) / 10 * Time.deltaTime;
        gameObject.transform.position = formationPos;
    }

    Transform NextFreePosition()
    {
        foreach(Transform childPosition in transform)
        {
            if(childPosition.childCount == 0)
            {
                return childPosition;
            }
        }
        return null;
    }
    
    bool AllMembersDead()
    {
        // Checks position by position whether there are enemies.
        // If there is at least 1 enemy it returns True
        // Else returns False

        foreach(Transform childPosition in transform)
        {
            if(childPosition.childCount >= 1)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnUntilFull()
    {
        Transform nextFreePos = NextFreePosition();
        if (nextFreePos)
        {
            GameObject enemy = Instantiate(enemyPrefab, nextFreePos.position, Quaternion.identity) as GameObject;
            // Set enemy transform parent to the enemy spwaner.
            enemy.transform.parent = nextFreePos;
        }

        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

}
