using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float damage = 0f;           // how much damage it deals
    public float attackSpeed = 1f;      // attacks per second
    public float range = 1f;            // how far the attack reaches

    [HideInInspector] public Transform playerTransform; // assigned when equipped

    protected float lastAttackTime; // keeps track of attack cooldowns

    public virtual void Attack()
    {
        // Enforce attack cooldown
        if (Time.time - lastAttackTime < 1f / attackSpeed)
            return;

        lastAttackTime = Time.time;
        PerformAttack();
    }

    // Each weapon will define how it attacks
    protected abstract void PerformAttack();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
