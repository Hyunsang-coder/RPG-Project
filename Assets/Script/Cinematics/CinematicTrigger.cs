using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private void Update()
        {
            SkipCinematic();
        }

        bool introCinematicPlayed = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !introCinematicPlayed)
            {
                GetComponent<PlayableDirector>().Play();
                introCinematicPlayed = true;
            }
            
        }

        void SkipCinematic()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<PlayableDirector>().Stop();
                introCinematicPlayed = true;
            }
            
        }

    }
}

