using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FadeToColor : MonoBehaviour
{
    public Color color;
    public bool fadeIn;
    public float duration = 0.01f;

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

    void SetUpMaterial ()
    {
        if (material == null)
        {
            material = new Material (Shader.Find ("Hidden/FadeToColor"));
            material.hideFlags = HideFlags.DontSave;
            opacity = (fadeIn && Application.isPlaying) ? 1.0f : 0.0f;
        }
    }

    void Start ()
    {
        if (Application.isPlaying)
        {
            if (fadeIn)
                delta = -1.0f / duration;
            else
                enabled = false;
        }
    }

    void Update ()
    {
        SetUpMaterial ();

        if (Application.isPlaying)
        {
            opacity = Mathf.Clamp01 (opacity + delta * Time.deltaTime);
            if (opacity == 0.0f)
                enabled = false;
        }
    }

    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        SetUpMaterial ();

        material.color = color;
        material.SetFloat ("_Opacity", opacity);

        Graphics.Blit (source, destination, material);
    }
}
