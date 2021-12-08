using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // PlayerController�� �ᱹ mouse control�� ���� Ŭ���� (���콺 Ŭ�� + Raycast)  

        Health health; 
        void Start()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;  // InteractWithCombat�� true�̸� Update �Լ����� ��������
            if (InteractWithMovement()) return; // InteractWithCombat�� false���� InteractWithMovement ���� 
            print("Nowhere to move"); // ���� �� �Լ��� false���� print!
        }

        bool InteractWithCombat()
        { 
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());  // hit ��� collider �ʿ�!!
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }


        bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        
    }
}