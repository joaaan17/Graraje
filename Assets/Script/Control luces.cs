using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPC_Grupo_07 : MonoBehaviour
{
    public Color color;
    public Light[] luces; 
    public float intensity = 1f;
    public Material material, material2;
    public float rojo = 0.6f;
    public float verde = 0.6f;
    public float azul = 0.6f;
    public float velocidadParpadeo = 10.0f;

    void Start()
    {
        color = new Color(rojo, verde, azul);
        material.EnableKeyword("_EMISSION");
        material2.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (intensity < 3f)
                intensity += 1f;
            else
                intensity = 3f;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (intensity > 0f)
                intensity -= 1f;
            else
                intensity = 0f;
        }
        
        AplicarIntensidad(intensity);   
    }

    void AplicarIntensidad(float newIntensity)
    {
        

        for (int i=0; i < luces.Length; i++)  
        {
            if (luces[i] != null)
            {
                luces[i].intensity = newIntensity;
                luces[i].color = color;
                material.SetColor("_EmissionColor", color * newIntensity);
                
                if(newIntensity > 0){

                    float parpadeoIntensity = Mathf.Sin(Time.time * velocidadParpadeo);
                    luces[2].intensity = parpadeoIntensity;
                    luces[2].color = color;

                    material2.SetColor("_EmissionColor", color * parpadeoIntensity);

                }
                else{
                    luces[2].intensity = 0f;
                    luces[2].color = color;
                    material2.SetColor("_EmissionColor", color * newIntensity);
                }
            }
        }
    }
}