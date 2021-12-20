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
        [SerializeField] Transform target = null;
        [SerializeField] float projectileSpeed =1;
        private void Update()
        {
            if (target == null) return;
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime*projectileSpeed);

        }

        private Vector3 GetAimLocation()         //ĸ���ݶ��̴� ���� ��� 80% ��ġ�� ȭ���� ��������...
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.position;

            Vector3 targetPosition = new Vector3(target.position.x, targetCapsule.height * heightRatio, target.position.z);
            return targetPosition;
        }
    }

}
