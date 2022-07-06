
using UnityEngine;


public class Character : MonoBehaviour
{
    //Anim State Variables
    public Animator animator;
    public bool isDead;
    public bool isAttacking;
    public bool isMoving;
    //Char Variables
    public float damageFactor; // Damage Çarpaný
    public Weapon myWeapon;
    public float movementSpeed;
    public float attackRange;
    public float sightRange;
    public GameObject model;
    public float attackRateFactor;
    
    //
    public LayerMask player;
    public LayerMask enemy;
    public LayerMask ally;
    //
    public float minRange;
    float timer;

    private void Start()
    {
        timer = 0;
        // game manager yada inventory      
    }

    public void Attack ()
    {
        timer += Time.deltaTime;
        if (timer>=5/(attackRateFactor*myWeapon.AttackRate) && Physics.CheckSphere(transform.position, myWeapon.range, enemy)) {
            
            //mainCharacter.LookAt(FindClosestEnemy(transform.position, myWeapon.range).position);
            myWeapon.InitializeWeapon(this, enemy, damageFactor);
            myWeapon.Attack();
            timer = 0;
        }
    }

    public void Move(Vector2 movementInput)
    {
                //Yapýlacak!!!!
    }

    // defaultMove fonksiyonuyla karakterin verdiðimiz Vector3 pozisyonuna direkt olarak gitmesini saðlayacagýz.
    public void DefaultMove(Vector3 vector)  
    {
        //Yapýlacak!!
    }

    public void Interact(Interactable interactable)
    {

    }

    public void LookAtDirection(/*içine bir þey gelecek mi ?*/ )
    {
    }

    public void SetWeapon(Weapon weapon)
    {
        myWeapon = weapon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            Interact(other.GetComponent<Interactable>());            
        }
    }

    public Transform FindClosestEnemy(Vector3 center , float radius)
    {        
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, enemy);
        foreach (var hitCollider in hitColliders)
        {
            if (!hitCollider.gameObject.GetComponent<Character>().isDead)
            {
                Vector3 directionToTarget = hitCollider.gameObject.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = hitCollider.gameObject.transform;
                }
            }            
        }
        return bestTarget;
    }
    
    public void Initialize(float damageFactor , float movementSpeed , float attackRange , float sightRange)
    {
        this.damageFactor = damageFactor;
        this.movementSpeed = movementSpeed;
        this.attackRange = attackRange;
        this.sightRange = sightRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, minRange);
    }
}
