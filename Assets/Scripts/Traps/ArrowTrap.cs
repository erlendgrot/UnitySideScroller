
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows; // Will contain all projectiles the trap can fire

    [Header ("Sound")]
    [SerializeField] private AudioClip arrowSound;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0; // Resets the cooldownTimer
        SoundManager.instance.PlaySound(arrowSound);
        //pool arrows
        arrows[FindArrow()].transform.position = firePoint.position; //Make one of the fireballs have the same position as the firepoint
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    // Checks if one of the fireballs are active, i.e. that we can fire a fireball 
    // Polling
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
            
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCoolDown)
        {
            Attack();
        }
    }
}
