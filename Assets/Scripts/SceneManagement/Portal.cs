using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;
using UnityEngine.AI;

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
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;
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

            DontDestroyOnLoad(gameObject);    //Coroutine ��� �� �ϸ� scene �ҷ� ���⵵ ���� print�� destroy ���� 

            Fader fader = FindObjectOfType<Fader>();
            
            yield return fader.FadeIn(fadeInTime);
            
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad); //LoadScene�� return type�� void�� �ƿ� �۵� ����. Async�� �Ϸ� �ð� return 

            wrapper.Load();

            Portal otherPortal = GetOtherPortal(); // GetOtherPortal�� null�̵Ǹ� �ܼ� �Ÿ� �ε��ǰ�, ĳ���� ��ġ�� ��ġ ������Ʈ X
            UpdatePlayer(otherPortal);
            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeOut(fadeOutTime);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;      //�ٸ� �ſ��� destination�� ������ ��Ż�� �̵� 

                return portal;
            }

            return null;
        }   


        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

    }
}

