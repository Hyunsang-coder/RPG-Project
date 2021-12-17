using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        private void Start()
        {
            
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }


        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to Load not set");
                yield break;
            }
            DontDestroyOnLoad(gameObject);                          //Coroutine 사용 안 하면 scene 불러 오기도 전에 print랑 destroy 실행 
            yield return SceneManager.LoadSceneAsync(sceneToLoad); //LoadScene은 return type이 void라서 아예 작동 안함. Async는 완료 시간 return 

            Portal otherPortal = GetOtherPortal(); // GetOtherPortal이 null이되면 단순 신만 로딩되고, 캐릭터 위치는 위치 업데이트 X
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;      //destination 같은 포탈로 이동 

                return portal;
            }

            return null;
        }   


        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

    }
}

