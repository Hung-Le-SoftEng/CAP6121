using UnityEngine;
public class EnemySpawning : MonoBehaviour
{

    public GameObject Enemy;

    public float EnemyOriginRadius = 30f;
    public float EnemyOriginDeviation = 5f;

    public float bulletAccuracy = 50f;
    public float bulletSpeed = 5f;

    public float spawningRate = 3f;
    public float spawningDeviation = 5f;

    public bool Spawning = false;
    private float timer = 5f;

    // Use this for initialization
    void Start()
    {
        timer = spawningRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy == null)
        {
            return;
        }

        if (Spawning)
        {
            // randomly generate position vector where the bullet will be spawn
            //Vector3 origin = new Vector3(Random.Range(transform.position.x - 30f, transform.position.x + 30f),
            //                            Random.Range(transform.position.y, transform.position.y + 30f),
            //                            Random.Range(transform.position.z - 30f, transform.position.z + 30f));

            Vector3 origin = transform.forward * (Random.Range(20f, 30f)) +
                             transform.right * (Random.Range(-20f, 20f)) +
                             transform.up * (Random.Range(0f, 0.5f));


            origin = origin.normalized * (Random.Range(EnemyOriginRadius - EnemyOriginDeviation, EnemyOriginRadius + EnemyOriginDeviation));
            origin += transform.position;


            Vector3 targetDirection = new Vector3();
                // very accurate, direct at middle of player with randomize up and down
                targetDirection = (transform.position + transform.up * Random.Range(0f, .5f)) - origin;



            GameObject gameObj = (GameObject)Instantiate(Enemy, origin, Quaternion.LookRotation(targetDirection));
            //gameObj.transform.rotation = Quaternion.LookRotation(targetDirection);

            Rigidbody rb = gameObj.GetComponent<Rigidbody>();
            //rb.velocity = targetDirection.normalized * bulletSpeed;

            Spawning = false;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Spawning = true;
            timer = spawningRate + Random.Range(-spawningDeviation, spawningDeviation);

        }
    }
}
