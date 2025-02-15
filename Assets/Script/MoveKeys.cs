using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 1;
    public float turnSpeed = 20;
    public float valTrans = 0;
    public float valRot = 0;
    public GameObject motor1, motor2, motor3, motor4, ground;

    public float longitudTirantes;
    private float minPlataforma = 1.6f, maxPlataforma = 4f, alturaPlataforma, anguloTirantes = 0f;

    private Vector3 escalaTirantes;
    //private Vector3 escalaTirantes = new Vector3(1,1,1);


    void Start()
    {
            escalaTirantes = motor1.transform.localScale;
            escalaTirantes = new Vector3(longitudTirantes, motor1.transform.localScale.y, motor1.transform.localScale.z);
            motor1.transform.localScale = escalaTirantes;
            motor2.transform.localScale = escalaTirantes;
            motor3.transform.localScale = escalaTirantes;
            motor4.transform.localScale = escalaTirantes;
            Debug.Log("Longitud Tirantes" + longitudTirantes);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = Vector3.zero;
        Quaternion rotation1 = Quaternion.identity;
        Quaternion rotation2 = Quaternion.identity;

        if (Input.GetKey(KeyCode.K) && ground.transform.localPosition.y < maxPlataforma)
        {
             //Option 1: set the rotation using a control variable 
            moveVector = (Vector3.up * Time.deltaTime * speed);
            ground.transform.localPosition += moveVector;

            alturaPlataforma = ground.transform.position.y;
            anguloTirantes = Mathf.Rad2Deg * Mathf.Asin(alturaPlataforma / longitudTirantes);

            motor1.transform.localRotation = Quaternion.Euler(0, 0, anguloTirantes);
            motor2.transform.localRotation = Quaternion.Euler(0, 0, anguloTirantes);
            motor3.transform.localRotation = Quaternion.Euler(0, 0, -anguloTirantes);
            motor4.transform.localRotation = Quaternion.Euler(0, 0, -anguloTirantes);

        }

        if (Input.GetKey(KeyCode.L) && ground.transform.localPosition.y > minPlataforma)
        {
            moveVector = (Vector3.down * Time.deltaTime * speed);
            ground.transform.localPosition += moveVector;

            alturaPlataforma = ground.transform.localPosition.y;

            // Calcular el ángulo de rotación de los tirantes
            alturaPlataforma = ground.transform.position.y;
            anguloTirantes = Mathf.Rad2Deg * Mathf.Asin(alturaPlataforma / longitudTirantes);

            // Rotar los tirantes para coincidir con la nueva posición de la plataforma
            motor1.transform.localRotation = Quaternion.Euler(0, 0, anguloTirantes);
            motor2.transform.localRotation = Quaternion.Euler(0, 0, anguloTirantes);
            motor3.transform.localRotation = Quaternion.Euler(0, 0, -anguloTirantes);
            motor4.transform.localRotation = Quaternion.Euler(0, 0, -anguloTirantes);
            
        }
    }
}
