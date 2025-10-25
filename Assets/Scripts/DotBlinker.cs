using UnityEngine;
using UnityEngine.UI;

public class DotBlinker : MonoBehaviour
{
    public float speed = 3f; // how fast the light travels
    public int trailLength = 1; // how many dots are lit at once

    private Image[] dots;
    private int activeIndex = 0;
    private float timer = 0f;

    void Start()
    {
        dots = GetComponentsInChildren<Image>();
    }

    void Update()
    {
        if (dots.Length == 0) return;

        timer += Time.deltaTime * speed;
        if (timer >= 1f)
        {
            timer = 0f;
            activeIndex = (activeIndex + 1) % dots.Length; // move to next dot
        }

        for (int i = 0; i < dots.Length; i++)
        {
            Color c = dots[i].color;
            // make current dot bright, others dim
            c.a = (i >= activeIndex && i < activeIndex + trailLength) ? 1f : 0.2f;
            dots[i].color = c;
        }
    }
}


