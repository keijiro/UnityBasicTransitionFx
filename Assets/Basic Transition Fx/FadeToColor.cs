using UnityEngine;
using System.Collections;

public class FadeToColor : MonoBehaviour
{
    public Color color;
    public bool fadeIn;
    public float duration = 1.0f;

    Material material;
    float opacity;
    float delta;

    public void BeginFadeOut ()
    {
        delta = 1.0f / duration;
        enabled = true;
    }

    public void BeginFadeOut (float duration)
    {
        delta = 1.0f / duration;
        enabled = true;
    }
    
    public void BeginFadeOut (float time, Color color)
    {
        this.color = color;
        BeginFadeOut (time);
    }

    public void BeginFadeIn ()
    {
        delta = -1.0f / duration;
        enabled = true;
    }
    
    public void BeginFadeIn (float duration)
    {
        delta = -1.0f / duration;
        enabled = true;
    }
    
    public void BeginFadeIn (float time, Color color)
    {
        this.color = color;
        BeginFadeIn (time);
    }

    void Awake ()
    {
        material = new Material (Shader.Find ("Hidden/FadeToColor"));

        if (fadeIn)
        {
            opacity = 1.0f;
            delta = -1.0f / duration;
        }
        else
        {
            opacity = 0.0f;
            enabled = false;
        }
    }

    void Update ()
    {
        opacity = Mathf.Clamp01 (opacity + delta * Time.deltaTime);
        if (opacity == 0.0f) enabled = false;
    }

    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        material.color = color;
        material.SetFloat ("_Opacity", opacity);
        Graphics.Blit (source, destination, material);
    }
}
