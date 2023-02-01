using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] public GameObject target;

    private Vector3 previousPosition;

    [SerializeField] private float distanceFromCamera;

    [SerializeField] private float scrollSpeed;
    [SerializeField] private float speed;

    public void Start()
    {
        cam.transform.Translate(new Vector3(0, 0, distanceFromCamera));
    }


    void Update()
    {

        Transform camTransform = Camera.main.transform;
        Vector3 camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z);
        Vector3 direction = (transform.position - camPosition).normalized;

        CameraRotation();


        if (Input.GetKey(KeyCode.A))
            transform.position -= Camera.main.transform.right * Time.deltaTime * speed;
        else if (Input.GetKey(KeyCode.D))
            transform.position += Camera.main.transform.right * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.W))
            transform.localPosition += direction * Time.deltaTime * speed;
        else if (Input.GetKey(KeyCode.S))
            transform.localPosition -= direction * Time.deltaTime * speed;



    }


    public void CameraRotation()
    {
        float oldDistance = distanceFromCamera;

        distanceFromCamera += Input.mouseScrollDelta.y * scrollSpeed;

        if (oldDistance != distanceFromCamera)
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = target.transform.position;

            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Translate(new Vector3(0, 0, distanceFromCamera));

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }


        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }



        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = target.transform.position;

            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Translate(new Vector3(0, 0, distanceFromCamera));

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
    
