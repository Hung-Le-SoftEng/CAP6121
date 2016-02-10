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

    public float attackFreq = 1f;
    public float attackPower = 20f;

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


    public float stunnedTimer = 0f;

    public float attackTimer = 0f;

	// Use this for initialization
	void Start () {
        navAgent = transform.GetComponent<NavMeshAgent>();

        navAgent.speed = speed;
        navAgent.angularSpeed = rotationSpeed;

        animator = transform.GetComponent<Animator>();
        originalSpeed = speed;

        //StartCoroutine(GrabWeapon());
        //animator.SetBool("grabweapon", true);

    }

    IEnumerator GrabWeapon() {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("grabweapon", true);
        yield return new WaitForSeconds(2);
    }
	
	// Update is called once per frame
	void Update () {

        if (enemy == null) {
            enemy = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // if monster is stunned, force monster to stay in place
        if (stunnedTimer > 0) {
            stunnedTimer -= Time.deltaTime;
            navAgent.SetDestination(transform.position);
            animator.SetFloat("speed", 0, 0, 0.2f);
            
            return;
        }

        if (enemy != null) {
            
            navAgent.SetDestination(enemy.position);
            animator.SetFloat("speed", navAgent.velocity.magnitude, dampTime, 0.2f);
            animator.SetBool("grounded", grounded);
        }

        if (shoot) {
            GameObject gameObj = (GameObject) Instantiate(bullet, transform.position + transform.forward/3, transform.rotation);

            Rigidbody rb = gameObj.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * bulletSpeed;

            shoot = false;
        }


        // target within range, attack
        Vector3 targetVector = enemy.position - transform.position;
        //Debug.Log((targetVector).magnitude);
        //Debug.Log((targetVector).sqrMagnitude);
        if ((targetVector).sqrMagnitude < 2) {
            attackTimer += Time.deltaTime;
            // cheating, lock on to target
            Vector3 lookDir = new Vector3(targetVector.x, 0, targetVector.z);
            transform.rotation = Quaternion.LookRotation(lookDir);

        }


        if (attack || attackTimer > 1 / attackFreq) {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
		    //if (currentState.length == 0)
			//{
				int attackrandom = Random.Range(0,4);
				animator.SetFloat("random",attackrandom);
				animator.SetBool("attack",true);
                attack = false;
			//}

            // cheating, not actually hit the target but health decrease anyway
                if (enemy.tag == "Player") {
                    enemy.GetComponent<PlayerHealth>().TakeDamage(attackPower);
                }
                
                attackTimer -= 1 / attackFreq;
        }
        else {
            animator.SetBool("attack", false);
        }
	}


    void TakeDamage() {

        ScoreManager.score += 20;
        Destroy(gameObject);

    }



}
