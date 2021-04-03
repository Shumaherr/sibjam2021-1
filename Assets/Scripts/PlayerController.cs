using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float verticalSpeed = 5.0f;
    [SerializeField] private float horizontalSpeed = 15.0f;
    private Camera _camera;

    [SerializeField] public Transform light;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.x += horizontalSpeed * Time.deltaTime;
        transformPosition.y += Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime;
        transform.position = transformPosition;
        if (_camera != null)
        {
            Vector3 worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (worldPoint - transform.position));
            //TODO constraints only +-45 grad
            light.rotation = targetRotation;
            
            light.Rotate(0, 0, 90);
        }
    }
}
