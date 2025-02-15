using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirGancho : MonoBehaviour
{

    public Transform objetivo;
    //public float velocidad;

void Update()
{
    // Obten la posición actual del objeto
    Vector3 pos = transform.position;

    // Obten la posición del objetivo
    Vector3 posfinal = objetivo.position; 

    // Ajusta este valor al deseado, interpretando 'velocidad' como unidades por segundo
    float velocidadEnUnidadesPorSegundo = 3.0f; // Puedes ajustar este valor según necesites

    // Calcula la distancia a moverse durante este frame
    float paso = velocidadEnUnidadesPorSegundo * Time.deltaTime;

    // Interpola linealmente entre la posición actual y la posición final
    // Usando 'paso' dividido por la distancia total como tasa de interpolación
    Vector3 posIntermedia = Vector3.Lerp(pos, posfinal, paso / Vector3.Distance(pos, posfinal));

    // Actualiza la posición del transform para mover el objeto
    transform.position = posIntermedia;
}

}
