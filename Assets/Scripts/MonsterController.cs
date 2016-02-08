using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {


    private NavMeshAgent navAgent;
    public Transform enemy;
    Animator animator;

    public float speed = 2f;
    public float runSpeed = 4.5f;
    float originalSpeed = 2f;
    public float rotationSpeed = 120f;
    public float dampTime = 3f;

    public AudioSource myaudiosource;
    public Transform target;
    public Transform chest;
    public Transform shield;
    public Transform weapon;
    public Transform leftHandPos;
    public Transform rightHandPos;
    public Transform chestPosShield;
    public Transform chestPosWeapon;

    public AudioClip equip1Sound;
    public AudioClip equip2Sound;
    public AudioClip holster1Sound;
    public AudioClip holster2Sound;

    public bool grounded = true;

    public GameObject bullet;
    public float bulletSpeed = 10f;
    public bool shoot = false;
    public bool attack = false;


	// Use this for initialization
	void Start () {
        navAgent = transform.GetComponent<NavMeshAgent>();

        navAgent.speed = speed;
        navAgent.angularSpeed = rotationSpeed;

        animator = transform.GetComponent<Animator>();
        originalSpeed = speed;
    }
	
	// Update is called once per frame
	void Update () {

        if (enemy == null) {
            enemy = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (enemy != null) {
            navAgent.SetDestination(enemy.position);
            animator.SetBool("grounded", grounded);
            animator.SetFloat("speed", navAgent.velocity.magnitude, dampTime, 0.2f);
        }

        if (shoot) {
            GameObject gameObj = (GameObject) Instantiate(bullet, transform.position + transform.forward, transform.rotation);

            Rigidbody rb = gameObj.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * bulletSpeed;

            shoot = false;
        }

        if (attack) {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
		    if (currentState.length == 0)
			{
				int attackrandom = Random.Range(0,4);
				animator.SetFloat("random",attackrandom);
				animator.SetBool("attack",true);
   				
			}
        }

	}
}
