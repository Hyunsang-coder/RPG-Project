using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // PlayerController는 결국 mouse control에 대한 클래스 (마우스 클릭 + Raycast)  

        Health health; 
        void Start()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;  // InteractWithCombat이 true이면 Update 함수에서 빠져나옴
            if (InteractWithMovement()) return; // InteractWithCombat이 false여야 InteractWithMovement 실행 
            print("Nowhere to move"); // 위의 두 함수가 false여야 print!
        }

        bool InteractWithCombat()
        { 
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());  // hit 대상에 collider 필요!!
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