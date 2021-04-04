using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float verticalSpeed = 5.0f;
    [SerializeField] private float horizontalSpeed = 5.0f;
    [SerializeField] private float sprintMult = 2.0f;
    [SerializeField] private float fixedSpeed = 15.0f;
    [SerializeField] public Transform light;
    [SerializeField] public float rotationLock = 45.0f;
    [SerializeField] public Transform boneTransform;

    private Camera _camera;
    private float _horizontal = 0f;
    private float _vertical = 0f;
    private Vector3 _difference;
    private Rigidbody2D _rb;
    private Transform _pickpoint;
  

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _pickpoint = GameObject.Find("Pickpoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
            GameManager.Instance.playerInfo.PlayerStatus = PlayerStat.Sprint;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            GameManager.Instance.playerInfo.PlayerStatus = PlayerStat.Swim;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameManager.Instance.isPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameManager.Instance.isPressed = false;
        }
        _difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _difference.Normalize();
    }

    private void FixedUpdate() {
        //movement
        float newX, newY;
        if (GameManager.Instance.isStopped)
        {
            newX = GameManager.Instance.IsSprinting()
                ? sprintMult * fixedSpeed * 0.1f * Time.fixedDeltaTime + _horizontal * horizontalSpeed * Time.fixedDeltaTime
                : fixedSpeed * Time.fixedDeltaTime + _horizontal * horizontalSpeed * Time.fixedDeltaTime;
            newY = _vertical * verticalSpeed *0.1f * Time.fixedDeltaTime;
        }
        else
        {
            newX = GameManager.Instance.IsSprinting()
                ? sprintMult * fixedSpeed * Time.fixedDeltaTime + _horizontal * horizontalSpeed * Time.fixedDeltaTime
                : fixedSpeed * Time.fixedDeltaTime + _horizontal * horizontalSpeed * Time.fixedDeltaTime;
            newY = _vertical * verticalSpeed * Time.fixedDeltaTime;
        }

        _rb.velocity = new Vector2(newX, newY);
        //light rotation
        float rotationZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
        if(System.Math.Abs(rotationZ) <= rotationLock) {
            boneTransform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            light.rotation = Quaternion.Euler(0f, 0f, rotationZ);   
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                other.GetComponent<SmallEnemyBeh>().isFolowing = false;
                GameManager.Instance.isStopped = true;
                fixedSpeed *= 0.7f;
                break;
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                GameManager.Instance.isStopped = false;
                break;
            
        }
    }

}
