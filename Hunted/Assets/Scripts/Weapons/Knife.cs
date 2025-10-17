using UnityEngine;
using System.Collections.Generic;

public class Knife : Weapon
{
    public LayerMask enemyLayer; // set to NPC in inspector
    private HashSet<NPCKill> hitNPC = new HashSet<NPCKill>();

    private void Start()
    {
        // Default Knife stats
        damage = 82f;
        attackSpeed = 2f; // slower attacks
        range = .5f;       // medium reach
    }

    protected override void PerformAttack()
    {
        if (playerTransform == null)
        {
            return;
        }

        // reset list right before swing
        hitNPC.Clear();

        // Detect enemies within range
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerTransform.position, range, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Hitbox"))
            {
                NPCKill npc = hit.GetComponent<NPCKill>();
                if (npc != null && !hitNPC.Contains(npc))
                {
                    Debug.Log("damage: " + damage);
                    npc.TakeDamage(damage);
                    hitNPC.Add(npc);    // mark as hit to not deal double damage
                }
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}