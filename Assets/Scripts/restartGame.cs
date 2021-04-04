using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class restartGame : MonoBehaviour
{
    public new Light2D light;
    private Transform _monstrTransform;
    // Start is called before the first frame update
    void switchScene() {
        //SceneManager.LoadScene("TestLeveL");
    }

    void Start()
    {
        _monstrTransform = GetComponent<Transform>();
        if (light == null) {
            light = GetComponent<Light2D>();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        light.intensity -= 0.005f;
        if(light.intensity <= 0.0f)
        {
            switchScene();
        }
    }
}
