using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using System.Collections.Generic;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {

        [SerializeField] float maxSpeed = 6f;
        Animator anim;
        NavMeshAgent navMeshAgent;
        Health health;

        void Start()
        {
            anim = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            UpdateAnimator();
            navMeshAgent.enabled = !(health.IsDead());
            
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            anim.SetFloat("Speed", speed);
        }

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;

            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}