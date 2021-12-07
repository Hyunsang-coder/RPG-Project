using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movement;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        GameObject player;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player");

        }
        void Update()
        {
            
            if (DistanceToPlayer() <= chaseDistance)
            {
                print(transform.name + "will chase the player");

                GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
                GetComponent<Fighter>().Attack(player);
                //GetComponent<Mover>().StartMoveAction(player.transform.position);
            }
            else
            {
                GetComponent<NavMeshAgent>().isStopped = true;
            }

        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }

}
