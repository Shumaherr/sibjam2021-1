using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rb;

    private PolygonCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(speed * Time.fixedDeltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                GameManager.Instance.playerInfo.PlayerStatus = PlayerStat.Die;
                break;
            case "Enemy":
                other.GetComponent<SmallEnemyBeh>().isFolowing = false;
                other.transform.parent = transform;
                break;
            
        }
    }
}
