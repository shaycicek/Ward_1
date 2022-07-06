using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float startSpeed;
    public float currentSpeed;
    public float speedModifier;
    float slowSpeed;
    bool slowed;
    public FixedJoystick variableJoystick;
    public Rigidbody rb;
    public Character character;
    public Transform mainCharacter;
    public List<Interactable> collidedMineralList = new List<Interactable>();

    private void Awake()
    {
        character = GetComponent<Character>();
        startSpeed = character.movementSpeed;
        slowSpeed = startSpeed/3;
        speedModifier = 1;
        
    }
    private void Start()
    {
        currentSpeed = startSpeed;
        slowed = false;        
    }

    private void OnDisable()
    {
        
    }
    public void FixedUpdate()
    {
        Attack();
        if(variableJoystick.Vertical !=0 && variableJoystick.Horizontal!=0)
        {
            character.isMoving = true;
            Vector3 move = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            //rb.MovePosition(transform.position + move * Time.fixedDeltaTime * currentSpeed);
            rb.velocity = (move * currentSpeed);
            Vector3 lookTarget = transform.position + move.normalized;            
            transform.LookAt(lookTarget);
            Vector3 direction = lookTarget - transform.position;
            if(!character.isAttacking)
            {
                character.model.transform.LookAt(lookTarget);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            character.isMoving = false;
        }
        //Quaternion rot = Quaternion.LookRotation(direction, transform.up);
    }

    public void SetSpeed()
    {
        if (slowed)
        {
            currentSpeed = (slowSpeed) * (speedModifier);
        }
        else
        {
            currentSpeed = (startSpeed) * (speedModifier);
        }
    }
    public void EnterSlow()
    {
        slowed = true;
        currentSpeed = (slowSpeed) * (speedModifier);
        //currentSpeed = slowSpeed;
    }

    public void ExitSlow()
    {
        slowed = false;
        currentSpeed = (startSpeed) * (speedModifier);
        //currentSpeed = startSpeed;
    }

    public void Attack()
    {
         //BU KISIM SORULACAK?
        if (Physics.CheckSphere(transform.position, character.myWeapon.range, character.enemy))
        {
            character.isAttacking = true;
            if (character.FindClosestEnemy(transform.position, character.myWeapon.range)!= null)
            {
                mainCharacter.transform.LookAt(new Vector3(character.FindClosestEnemy(transform.position, character.myWeapon.range).position.x,
character.FindClosestEnemy(transform.position, character.myWeapon.range).position.y,
character.FindClosestEnemy(transform.position, character.myWeapon.range).position.z));
                mainCharacter.transform.Rotate(-mainCharacter.transform.rotation.x + 5.0f, mainCharacter.rotation.y + 53.0f, 0.0f); // ?? Deðiþecek
                character.Attack();
            }

        } else
        {
            character.isAttacking = false;
        }        
    }



    public void SpeedSkillModifier(float speed)
    {
        this.speedModifier *= speed;
    }

    public void AddMineralList(Interactable interactable)
    {
        if (collidedMineralList.Count <= 0)
        {
            //Slowed = true;            
            EnterSlow();
        }
        collidedMineralList.Add(interactable);
    }

    public void RemoveFromMineralList(Interactable interactable)
    {
        collidedMineralList.Remove(interactable);
        if (collidedMineralList.Count <= 0)
        {
            ExitSlow();
        }
        
    }

    public void Aim()
    {

    }

    public void SkillUsed()
    {

    }
}
