using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;
        [Range(0.1f, 5f)]
        [SerializeField] float zoomSensitivity = 1;
        Vector3 camOffset = new Vector3(0, 0, 0);
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 0.1f);
            ScrollToZoom();
        }

        private void ScrollToZoom()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                GetComponentInChildren<Camera>().fieldOfView -= zoomSensitivity;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                GetComponentInChildren<Camera>().fieldOfView += zoomSensitivity;
            }
        }
    }
}
