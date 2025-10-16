using UnityEngine;

public class Axe : Weapon
{
    public LayerMask enemyLayer; // set to NPC in inspector

    private void Start()
    {
        // Default Axe stats
        damage = 8f;
        attackSpeed = 0.8f; // slower attacks
        range = 2f;       // medium reach
    }

    protected override void PerformAttack()
    {
        if (playerTransform == null)
        {
            return;
        }

        // Detect enemies within range
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerTransform.position, range, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            NPCKill npc = hit.GetComponent<NPCKill>();
            if (npc != null)
            {
                Debug.Log("damage: " + damage);
                npc.TakeDamage(damage);
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}