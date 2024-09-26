using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    private Image image;
    public float dino; // tick //

    private float timer;

    private Color[] rainbow = new Color[]
    {
        Color.red,
        new Color(1f, 0.5f, 0),
        Color.yellow,
        Color.green,
        new Color(0f, 0.5f, 1f),
        Color.blue,
        new Color(0.5f, 0f, 0.5f)
    };


    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if(timer > dino)
        {
            int randomIndex = Random.Range(0, rainbow.Length);
            image.color = rainbow[randomIndex];
            timer = 0.0f;
        }

        
    }

    private void FixedUpdate()
    {
        
    }
}
