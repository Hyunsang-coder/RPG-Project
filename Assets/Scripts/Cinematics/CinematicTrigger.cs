using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        private void Update()
        {
            SkipCinematic();
        }

        static bool introCinematicPlayed = false;
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

        public object CaptureState() 
        {
            return introCinematicPlayed;
        }

        public void RestoreState(object state) 
        {
            bool CinematicPlayed = (bool)state;
            introCinematicPlayed = CinematicPlayed;
        }

    }
}

