using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 10f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(Respawn());
            }

        }

        IEnumerator Respawn()
        {
            ShowPickup(false);
            yield return new WaitForSeconds(respawnTime);
            ShowPickup(true);
        }

        private void ShowPickup(bool isShown)
        {
            GetComponent<CapsuleCollider>().enabled = isShown;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(isShown);
            }
        }

    }
}

