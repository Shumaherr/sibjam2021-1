using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prallax : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] float prallaxEffect = 5f;

    private float _lenght;
    private float _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (camera.transform.position.x * prallaxEffect);
        float temp = (camera.transform.position.x * (1 - prallaxEffect));
        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);

        if (temp > distance + _lenght) {
            _startPosition += _lenght;
        }
        else if (temp < distance - _lenght) {
            _startPosition -= _lenght;
        }

    }
}
