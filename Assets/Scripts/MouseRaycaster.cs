using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
    public Camera sourceCamera;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = new Ray();
            ray = sourceCamera.ScreenPointToRay(mousePos);
            RaycastHit hitInfo;
            float lineMaxLength = 2f;
            int layerMask = 1 << 2;
            layerMask = ~layerMask;

            if (Physics.Raycast(ray, out hitInfo, lineMaxLength, layerMask))
            {
                GameObject hitObject = hitInfo.collider.gameObject;
                if (hitObject.tag == "Virus")
                {
                    hitObject.GetComponent<Virus>().Deactivate();
                }
            }

            Debug.DrawLine(ray.origin, hitInfo.point * 5, Color.red);
        }
    }
}
