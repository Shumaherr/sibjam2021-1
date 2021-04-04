using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundPrallax : MonoBehaviour
{
    [SerializeField] public GameObject bg_1;
    [SerializeField] public GameObject bg_2;

    [SerializeField] public Sprite[] main_bg;
    [SerializeField] public Sprite[] second_bg;

    private float _leftCameraBorder; //координаты левой границы камеры
    private float _bg_1_end = 0.0f, _bg_2_end = 0.0f;
    private SpriteRenderer _bg_1_renderer, _bg_2_renderer;
    private bool _mainLeft = true;

    private void intilizeBackground(bool left)
    {
        int bg_main_random = Random.Range(0, main_bg.Length);
        if (left)
        {
            _bg_1_renderer.sprite = main_bg[0];
            _bg_1_end = _mainLeft ? 0 : _bg_2_end;
            _bg_1_end += _bg_1_renderer.sprite.bounds.extents.x * 2.0f;
            bg_1.transform.position = new Vector3(_bg_2_end, 0.0f, 0.0f);

            _mainLeft = false;
        }
        else
        {
            _bg_2_renderer.sprite = main_bg[0];
            _bg_2_end = !_mainLeft ? 0 : _bg_1_end;
            _bg_2_end += _bg_2_renderer.sprite.bounds.extents.x * 2.0f;
            bg_2.transform.position = new Vector3(_bg_1_end, 0.0f, 0.0f);
            _mainLeft = true;
        }
    }


    private void Start()
    {
        _bg_1_renderer = bg_1.GetComponent<SpriteRenderer>();
        _bg_2_renderer = bg_2.GetComponent<SpriteRenderer>();

        intilizeBackground(true);
        _mainLeft = true;
        intilizeBackground(false);
    }

    private void Update()
    {
        _leftCameraBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).x;
    }

    private void FixedUpdate()
    {
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