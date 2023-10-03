using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint; //Position from where the projectile is fired
    [SerializeField] private GameObject[] fireballs; //Array of all projectiles
    [SerializeField] private AudioClip fireballSound;
    private Animator anim; //To connect with the animator in Unity

    private PlayerMovement playerMovement; //Object of the playermovement class created in playermovement
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        //To assign the references
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //Check if we can attack
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack"); // To activate the attack animation
        cooldownTimer = 0; //Reset the cooldownTimer

        //polling fireball
        fireballs[FindFireball()].transform.position = firePoint.position; //Make one of the fireballs have the same position as the firepoint
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x)); //Get the component of the projectile and send it in the direction the player is facing
    }
    
    // Checks if one of the fireballs are active, i.e. that we can fire a fireball
    // This is known as polling
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
            
        }
        return 0;
    }
}
