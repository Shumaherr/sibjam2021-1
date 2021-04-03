using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float verticalSpeed = 5.0f;
    [SerializeField] private float horizontalSpeed = 5.0f;
    [SerializeField] private float fixedSpeed = 15.0f;
    [SerializeField] public Transform light;
    [SerializeField] public float rotationLock = 45.0f;

    private Camera _camera;
    private float _horizontal = 0f;
    private float _vertical = 0f;
    private Vector3 _difference;
    private Rigidbody2D _rb;

  

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _difference.Normalize();
    }

    private void FixedUpdate() {
        //movement
        _rb.velocity = new Vector2(fixedSpeed * Time.fixedDeltaTime + _horizontal * horizontalSpeed * Time.fixedDeltaTime, _vertical * verticalSpeed * Time.fixedDeltaTime);
        //light rotation
        float rotation_z = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
        if(System.Math.Abs(rotation_z) <= rotationLock) {
            light.rotation = Quaternion.Euler(0f, 0f, rotation_z);
        }
    }

}
