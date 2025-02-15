using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverGrua : MonoBehaviour
{
    public float speed = 1;
    public float turnSpeed = 20;
    public float valTrans = 0;
    public float valRot = 0;
    public GameObject eje, gancho, cables, cable1, cable2, pivotcable1, pivotcable2, pivoteLerp;
    private float maxEje = 7.2f, minEje = -6.9f, minGancho = -0.32f, maxGancho = 0.31f, maxBGancho=-13f, maxSGancho = -5f, escaladoIni, escaladoIni2, escaladoActual, cambioEscalado, distanciaActual, distanciaIni;
    private Vector3 escalaCables = new Vector3(1,1,1);

    void Start()
    {
        distanciaIni = Vector3.Distance(pivotcable1.transform.position, cable1.transform.position);    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = Vector3.zero;
        Vector3 posicionCables = Vector3.zero;
        Vector3 posicionPivot = Vector3.zero;

        Quaternion rotation = Quaternion.identity;


        //Mover gancho 
        if (Input.GetKey(KeyCode.N) && gancho.transform.localPosition.x < maxGancho)
        {
            moveVector += (Vector3.right * Time.deltaTime * speed);
            gancho.transform.localPosition += moveVector;
        }

        if (Input.GetKey(KeyCode.M) && gancho.transform.localPosition.x > minGancho)
        {
            moveVector += (Vector3.right * Time.deltaTime * speed);
            gancho.transform.localPosition -= moveVector;
        }

        //Mover Eje de la grua
        if (Input.GetKey(KeyCode.H) && eje.transform.position.x < maxEje)
        {
            moveVector += (Vector3.right * Time.deltaTime * speed);
            eje.transform.localPosition += moveVector;
        }

        if (Input.GetKey(KeyCode.J) && eje.transform.position.x > minEje)
        {
            moveVector += (Vector3.right * Time.deltaTime * speed);
            eje.transform.localPosition -= moveVector;
        }

        //Subir - Bajar Gancho
        if (Input.GetKey(KeyCode.F) && pivoteLerp.transform.localPosition.y > maxBGancho)
        {
            posicionCables -= new Vector3(0, 3*0.02f, 0);
            pivoteLerp.transform.localPosition += posicionCables;
        }

        if (Input.GetKey(KeyCode.G) && pivoteLerp.transform.localPosition.y < maxSGancho)
        {
            posicionCables += new Vector3(0, 3*0.02f, 0);
            pivoteLerp.transform.localPosition += posicionCables;
        }

        distanciaActual = Vector3.Distance(pivotcable1.transform.position, cable1.transform.position);
        
        cambioEscalado = distanciaActual / distanciaIni;

        Vector3 escalaCables = new Vector3(1, 1, cambioEscalado);

        cable1.transform.localScale = escalaCables;
        cable2.transform.localScale = escalaCables;

        cable1.transform.LookAt(pivotcable1.transform);
        cable2.transform.LookAt(pivotcable2.transform);
    }
}
