using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer;


        void Start()
        {   
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }
        void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRange(player) && fighter.CanAttack(player))
            {
                print(transform.name + "will chase the player");

                GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
                fighter.Attack(player);
            }
            //else if ()
            //{
            //    //suspicion state;
            //}
            else
            {
                mover.StartMoveAction(guardPosition);
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private bool InAttackRange(GameObject player)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;
        }

        private void OnDrawGizmosSelected()  //OnDrawGizmos() ¿Í À¯»ç 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}
