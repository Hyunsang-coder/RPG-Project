using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;
        private void Start()
        {   
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }
        void Update()
        {
            if (InAttackRange(player) && fighter.CanAttack(player))
            {
                print(transform.name + "will chase the player");

                GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }

        }

        private bool InAttackRange(GameObject player)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;
        }
    }

}
