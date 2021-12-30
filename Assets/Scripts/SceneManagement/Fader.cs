using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvusGroup;

        private void Start()
        {
            canvusGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediately()
        {
            canvusGroup.alpha = 1;
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvusGroup.alpha < 1)
            {
                canvusGroup.alpha += Time.deltaTime / time;
                yield return null;
            }

        }

        public IEnumerator FadeOut(float time)
        {
            while (canvusGroup.alpha > 0)
            {
                canvusGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }

        }

    }

}
