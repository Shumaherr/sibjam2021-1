using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyBeh : MonoBehaviour
{
    
    [SerializeField] private float speed;

    public bool isFolowing;
    // Start is called before the first frame update
    void Start()
    {
        isFolowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFolowing) return;
        
        var position = GameManager.Instance.PlayerInstance.position;
        transform.position =
            Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
       // transform.LookAt(position);
    }
}
