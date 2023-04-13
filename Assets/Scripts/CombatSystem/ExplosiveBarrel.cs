using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class ExplosiveBarrel : MonoBehaviour
    {
        float damageValue;
        float explosionRadius;

        [SerializeField] GameObject ExplosionParticleEffect;
        public void SetMyValues(float damage, float radius)
        {
            damageValue = damage;
            explosionRadius = radius;
        }

        public void Explode()
        {
            //raycast in a circle around me for all enemies and the player. damage all of them
            RaycastHit[] hit = Physics.SphereCastAll(this.transform.position, explosionRadius, Vector3.forward);
            for (int i = 0; i < hit.Length; i++)
            {
                Enemy targettedEnemy = hit[i].collider.transform.GetComponent<Enemy>();
                if (targettedEnemy != null)
                {
                    targettedEnemy.AttackMe(damageValue);
                }
            }

            Instantiate(ExplosionParticleEffect, this.transform.position, Quaternion.identity);//the particle effect needs some way of killing itself. 
            Destroy(this.gameObject);
        }
    }
}

