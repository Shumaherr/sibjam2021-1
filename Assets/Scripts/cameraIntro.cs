using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class cameraIntro : MonoBehaviour
{
    public new Light2D light;
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = GetComponent<Transform>();
        if (light == null) {
            light = GetComponent<Light2D>();
        }
    }
    void switchScene() {
        SceneManager.LoadScene("TestLeveL");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_cameraTransform.position.y > -48.1f)
        {
            light.intensity -= 0.0007f;
            _cameraTransform.position += new Vector3(0.0f, -0.15f, 0.0f);
        }
        else 
        {
            switchScene();
        }
    }
}
