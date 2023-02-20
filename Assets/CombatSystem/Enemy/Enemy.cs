using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class Enemy : MonoBehaviour
    {
        float health = 10f;
        float speed = 2f;

        private void Update()
        {
            float localTimeScale = TurnManager.instance.currentGameState == TurnManager.GameStates.playersTurn ? 0.1f : 1f;
            
            transform.LookAt(new Vector3(0, 0, 0));
            transform.position += transform.forward * speed * Time.deltaTime * localTimeScale;

        }


        public void AttackMe(float damage, float knockbackAmount = 0f)
        {
            if (knockbackAmount > 0f)
            {
                Knockback(knockbackAmount);
            }

            health -= damage;
            if (health <= 0f)
            {
                Destroy(this.gameObject);//btw, change this. not only do i want dead enemies to still be able to fly backwards. but just immediatly vanishing isnt exciting
            }

            //Destroy(this.gameObject);
        }

        public void Knockback(float distance)
        {
            StartCoroutine(KnockbackAnimation(transform.position, transform.position -= transform.forward * distance));
        }

        IEnumerator KnockbackAnimation(Vector3 startingPosition, Vector3 newPosition)
        {
            for (float i = 0f; i <= 1f; i += Time.deltaTime * 5f)
            {
                transform.position = Vector3.Lerp(startingPosition, newPosition, i);

                //raycast to check if its collided with a barrel
                RaycastHit[] hit = Physics.SphereCastAll(transform.position, 1f, Vector3.forward);
                for (int x = 0; x < hit.Length; x++)
                {
                    Debug.Log(hit[x].transform.name);
                    if (hit[x].transform == this.transform)
                        continue;

                    if (hit[x].collider.tag == "ExplosiveBarrel")
                    {
                        hit[x].transform.GetComponent<ExplosiveBarrel>().Explode();
                    }
                    /*if (hit[x].collider.tag == "Enemy")
                    {
                        hit[x].transform.GetComponent<Enemy>().AttackMe(1f, 3f);
                    }*/
                }
                
                /*RaycastHit[] hit;
                Ray ray = new Ray();
                ray.direction = -transform.forward;
                ray.origin = transform.position;
                hit = Physics.RaycastAll(ray, 0.5f);
                for (int x = 0; x < hit.Length; x++)
                {
                    if (hit[x].collider.tag == "ExplosiveBarrel")
                    {
                        hit[x].transform.GetComponent<ExplosiveBarrel>().Explode();
                    }
                }*/

                yield return new WaitForEndOfFrame();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, 1.2f);
        }
    }
}

