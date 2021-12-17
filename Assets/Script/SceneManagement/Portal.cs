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
            DontDestroyOnLoad(gameObject);                          //Coroutine ��� �� �ϸ� scene �ҷ� ���⵵ ���� print�� destroy ���� 
            yield return SceneManager.LoadSceneAsync(sceneToLoad); //LoadScene�� return type�� void�� �ƿ� �۵� ����. Async�� �Ϸ� �ð� return 

            Portal otherPortal = GetOtherPortal(); // GetOtherPortal�� null�̵Ǹ� �ܼ� �Ÿ� �ε��ǰ�, ĳ���� ��ġ�� ��ġ ������Ʈ X
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;      //destination ���� ��Ż�� �̵� 

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

