using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] float heightRatio = 0.7f; //0.9 이상 headshot
        [SerializeField] float projectileSpeed =1;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 5f;
        [SerializeField] GameObject[] destroyOnhit = null;
        [SerializeField] float lifeAfterImpact = 2f;

        float damage = 0;

        Health target = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * Time.deltaTime*projectileSpeed);

        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()         //캡슐콜라이더 기준 상단 80% 위치로 화살이 꽂히도록...
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;

            Vector3 targetPosition = new Vector3(target.transform.position.x, targetCapsule.height * heightRatio, target.transform.position.z);
            return targetPosition;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(damage);
            projectileSpeed = 0;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject effect in destroyOnhit)
            {
                Destroy(effect);
            }

            Destroy(gameObject, lifeAfterImpact);
        }

    }

}
