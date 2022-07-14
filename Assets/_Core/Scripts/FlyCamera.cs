using System;
using _Core.Scripts.WorldGeneratorCore;
using UnityEngine;

namespace _Core.Scripts
{
    public class FlyCamera : MonoBehaviour
    {
        public Camera cam;
        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                transform.position + (cam.transform.forward * UnityEngine.Input.GetAxis("Vertical")) + (transform.right * UnityEngine.Input.GetAxis("Horizontal")), Time.deltaTime * 10f);
            transform.Rotate(new Vector3(-UnityEngine.Input.GetAxis("Mouse Y"),0,0));
            cam.transform.Rotate((new Vector3(0,UnityEngine.Input.GetAxis("Mouse X"),0)));

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Terrain")
                    {
                        hit.transform.GetComponent<Chunk>().PlaceTerrain(hit.point);
                    }
                }
            }
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Terrain")
                    {
                        hit.transform.GetComponent<Chunk>().RemoveTerrain(hit.point);
                    }
                }
            }
        }
    }
}