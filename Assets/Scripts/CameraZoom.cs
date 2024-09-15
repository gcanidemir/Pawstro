using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float zoom;
    [SerializeField] private Camera cam;
    [SerializeField] private float CamClose;
    [SerializeField] private float CamFurther;
    // Start is called before the first frame update
    void Start()
    {  
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        float cameraZoom = CamFurther;

        float cameraZoomDifference = cameraZoom - cam.orthographicSize;
        float cameraZoomSpeed = 1f;

        cam.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        float cameraZoom = CamClose;

        cam.orthographicSize = CamClose;
    }

}
