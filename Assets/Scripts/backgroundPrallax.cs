using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundPrallax : MonoBehaviour
{
    [SerializeField] public GameObject bg_1;
    [SerializeField] public GameObject bg_2;
    [SerializeField] public GameObject bg_1_2;
    [SerializeField] public GameObject bg_2_2;
    [SerializeField] public float xDeltaMult = -0.5f;

    [SerializeField] public Sprite[] main_bg;
    [SerializeField] public Sprite[] second_bg;

    private float _leftCameraBorder; //координаты левой границы камеры
    private float _lastCameraPosition;
    private float _bg_1_end = 0.0f, _bg_2_end = 0.0f;
    private SpriteRenderer _bg_1_renderer, _bg_2_renderer, _bg_1_2_renderer, _bg_2_2_renderer;
    private bool _mainLeft = true;

    private void intilizeBackground(bool left, bool first = false)
    {
        int bg_main_random = Random.Range(0, main_bg.Length);
        int bg_second_random = Random.Range(0, second_bg.Length);

        if(first)
        {
            if(left) 
            {
                _bg_1_renderer.sprite = main_bg[bg_main_random];
                _bg_1_2_renderer.sprite = second_bg[bg_second_random];

                _bg_1_end = _mainLeft ? 0 : _bg_2_end;
                _bg_1_end += _bg_1_renderer.sprite.bounds.extents.x * 3.0f + _leftCameraBorder;

                bg_1.transform.position = new Vector3(_leftCameraBorder + _bg_1_renderer.sprite.bounds.extents.x, 0.0f, 0.0f);
            } 
            else 
            {
                bg_2.transform.position = new Vector3(_bg_1_end, 0.0f, 0.0f);

                _bg_2_renderer.sprite = main_bg[bg_main_random];
                _bg_2_2_renderer.sprite = second_bg[bg_second_random];

                _bg_2_end = !_mainLeft ? 0 : _bg_1_end;
                _bg_2_end += _bg_2_renderer.sprite.bounds.extents.x * 2.0f;   
            }
        } 
        else
        { 
            if (left)
            {
                bg_1.transform.position = new Vector3(_bg_2_end, 0.0f, 0.0f);
                bg_1_2.transform.position = new Vector3(_bg_2_end, 0.0f, 0.0f);

                _bg_1_renderer.sprite = main_bg[bg_main_random];
                _bg_1_2_renderer.sprite = second_bg[bg_second_random];
                

                _bg_1_end = _mainLeft ? 0 : _bg_2_end;
                _bg_1_end += _bg_1_renderer.sprite.bounds.extents.x * 2.0f;

                _mainLeft = false;
            }
            else
            {
                bg_2.transform.position = new Vector3(_bg_1_end, 0.0f, 0.0f);
                bg_2_2.transform.position = new Vector3(_bg_1_end, 0.0f, 0.0f);

                _bg_2_renderer.sprite = main_bg[bg_main_random];
                _bg_2_2_renderer.sprite = second_bg[bg_second_random];

                _bg_2_end = !_mainLeft ? 0 : _bg_1_end;
                _bg_2_end += _bg_2_renderer.sprite.bounds.extents.x * 2.0f;
                

                _mainLeft = true;
            }
        }
    }


    private void Awake()
    {
        _bg_1_renderer = bg_1.GetComponent<SpriteRenderer>();
        _bg_1_2_renderer = bg_1_2.GetComponent<SpriteRenderer>();

        _bg_2_renderer = bg_2.GetComponent<SpriteRenderer>();
        _bg_2_2_renderer = bg_2_2.GetComponent<SpriteRenderer>();

        _leftCameraBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).x;
        _lastCameraPosition = _leftCameraBorder;

        intilizeBackground(true, true);
        intilizeBackground(false, true);
    }

    private void Update()
    {
        _leftCameraBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).x;
    }

    private void LateUpdate()
    {
        float deltaMovement = _leftCameraBorder - _lastCameraPosition;
        _lastCameraPosition = _leftCameraBorder;

        bg_1_2.transform.position += new Vector3(deltaMovement * xDeltaMult, 0.0f, 0.0f);
        bg_2_2.transform.position += new Vector3(deltaMovement * xDeltaMult, 0.0f, 0.0f);

        if (_leftCameraBorder > _bg_1_end)
        {
            intilizeBackground(true);
        }
        else if (_leftCameraBorder > _bg_2_end)
        {
            intilizeBackground(false);
        }
    }
}