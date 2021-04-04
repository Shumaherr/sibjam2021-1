using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isCollide;
    void Start()
    {
        isCollide = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.isPressed && other.gameObject.tag == "Enemy")
        {
            GameManager.Instance.playerInfo.Energy += 10;
            GameManager.Instance.playerInfo.Oxygen += 10;
            Destroy(other.gameObject);
        }
    }
}
