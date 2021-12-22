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
        [SerializeField] float heightRatio = 0.7f; //0.9 �̻� headshot
        [SerializeField] float projectileSpeed =1;

        Health target = null;
        private void Update()
        {
            if (target == null) return;
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime*projectileSpeed);

        }

        public void SetTarget(Health target)
        {
            this.target = target;
        }

        private Vector3 GetAimLocation()         //ĸ���ݶ��̴� ���� ��� 80% ��ġ�� ȭ���� ��������...
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;

            Vector3 targetPosition = new Vector3(target.transform.position.x, targetCapsule.height * heightRatio, target.transform.position.z);
            return targetPosition;
        }
    }

}
