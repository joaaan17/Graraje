using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CrearGeometria : MonoBehaviour
{
        private Vector2 originalTiling;

    public enum formas 
    {
        Triangulo,
        Tira,
        Plano,
        Cubo,
        Cilindro,
        Esfera
    };
    public formas forma;
    public int particiones = 20;
    public float tamX = 1;
    public float tamY = 1;
    public float tamZ = 1;
    public List<Vector3> vertices;
    public List<Vector3> normals;
    public List<Vector2> textCoords;
    public List<int> triangles;
    public float frecuencia = 0.5f;
    public float amplitud = 0.1f;

    
    // Función generica para crear la geometría, se invoca desde el CustomInspector
    public void crear() 
    {
        Debug.Log("Crear geometría: " + forma);
        switch (forma) {
            // Triangulo
            case formas.Triangulo: 
                crearTriangulo(); 
                break;

            case formas.Tira:
                crearTira();
                break;

            case formas.Plano:
                crearPlano();
                break;

            case formas.Esfera:
                crearEsfera();
                break;

            case formas.Cubo:
                crearCubo();
                break;

            case formas.Cilindro:
                CrearCilindro();
                break;
        }
    }

void CrearCilindro()
    {
        float radio = 0.5f;

        // Crear las listas para añadir vertices, normales, coordenadas de textura e indices de los triángulos
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();

        float paso = 1.0f / particiones; //Distancia a la que se van a crear los vertices

        int currIndex = 0;
        int tamTira = particiones + 1;
        float ruidoPerlin = 0;
        
        for (int i=0; i<=particiones; i++) {
            float u = paso * i;
            

            for (int j=0; j<=particiones; j++) {
                float v = paso * j;

                ruidoPerlin = Mathf.PerlinNoise(i*frecuencia, j*frecuencia);
                ruidoPerlin = (ruidoPerlin - 0.5f) * amplitud;

                if(i == particiones)
                 ruidoPerlin = 0; // CAMBIAR POR LA FORMULA LINEA 88!!!

                float amp = 1  - Mathf.Abs(0.5f - j) * 2;
                
                ruidoPerlin *= amp;   

            
                Vector3 aux = new Vector3(
                (radio + ruidoPerlin) * Mathf.Cos(2 * Mathf.PI * u),
                v,
                (radio  + ruidoPerlin) * Mathf.Sin(2 * Mathf.PI * u)
                );

                vertices.Add(aux);
                Vector3 aux2 = new Vector3(radio*Mathf.Cos(2*Mathf.PI*u),0, radio*Mathf.Sin(2*Mathf.PI*u));
                normals.Add(aux2.normalized);   
                textCoords.Add(new Vector2(u,v/2));

                if (i>0 && j>0) {
                    triangles.Add(currIndex);
                    triangles.Add(currIndex - tamTira - 1);
                    triangles.Add(currIndex - tamTira);


                    triangles.Add(currIndex);              
                    triangles.Add(currIndex - 1);
                    triangles.Add(currIndex - tamTira - 1);
                }

                // Incrementar el contador de vertices
                currIndex ++;
            }
        }

        //Tapa superior
        int index = currIndex;
        vertices.Add(new Vector3(0,1,0));
        normals.Add(new Vector3(0,1,0));
        textCoords.Add(new Vector2(0.25f, 0.75f));
        currIndex++;
        
        
        for(int i=0; i<=particiones; i++){
             float v = paso * i;
             float angle = 2*Mathf.PI*v;

            float x =  radio*Mathf.Cos(angle);
            float z = radio*Mathf.Sin(angle);

                Vector3 aux = new Vector3(
                    radio*Mathf.Cos(angle),
                    1,
                    radio*Mathf.Sin(angle)
                );

                vertices.Add(aux);
                normals.Add(new Vector3 (0, 1, 0));
                float Utext = 0.25f + Mathf.Cos(angle)* 0.25f; 
                float Vtext = 0.75f + Mathf.Sin(angle)* 0.25f; 
                textCoords.Add(new Vector2(Utext, Vtext));
                
                if (i>0){
                    triangles.Add(index);
                    triangles.Add(currIndex);
                    triangles.Add(currIndex-1);
                }
                currIndex++;
            }
            
       //Tapa inferior

        int index2 = currIndex;
        vertices.Add(new Vector3(0,0,0));
        normals.Add(new Vector3(0,-1,0));
        textCoords.Add(new Vector2(0.75f, 0.75f));
        currIndex++;
        
        for(int i=0; i<=particiones; i++){
            float v = paso * i;
            float angle=2*Mathf.PI*v;

            float x =  radio*Mathf.Cos(angle);
            float z = radio*Mathf.Sin(angle);

                Vector3 aux = new Vector3(
                    radio*Mathf.Cos(angle),
                    0,
                    radio*Mathf.Sin(angle)
                );

                vertices.Add(aux);
                normals.Add(new Vector3 (0,-1,0));
                float Utext = 0.75f + Mathf.Cos(angle)* 0.25f; 
                float Vtext = 0.75f + Mathf.Sin(angle)* 0.25f;
                textCoords.Add(new Vector2(Utext, Vtext));
                
                if (i>0){
                    triangles.Add(index2);
                    triangles.Add(currIndex - 1);
                    triangles.Add(currIndex);

                }
                currIndex++;
            }
            crearMesh();
        }

 


    void crearCubo()
    {

        //C1
        vertices = new List<Vector3>();
        vertices.Add(new Vector3(0, 0, 0)); //0
        vertices.Add(new Vector3(0, 0, tamZ)); //1
        vertices.Add(new Vector3(tamX, 0, 0)); //2
        vertices.Add(new Vector3(tamX, 0, tamZ)); //3
        // Crear las normales de cada vertice
        normals = new List<Vector3>();
        normals.Add(new Vector3(0, -1, 0));
        normals.Add(new Vector3(0, -1, 0));
        normals.Add(new Vector3(0, -1, 0));
        normals.Add(new Vector3(0, -1, 0));

        textCoords = new List<Vector2>();
        textCoords.Add(new Vector2(0, 0));
        textCoords.Add(new Vector2(0, tamZ));
        textCoords.Add(new Vector2(tamX, 0));
        textCoords.Add(new Vector2(tamX ,tamZ));

        //C2
        vertices.Add(new Vector3(0, 0, 0)); //4
        vertices.Add(new Vector3(0, tamY, 0)); //5
        vertices.Add(new Vector3(tamX, 0, 0)); //6
        vertices.Add(new Vector3(tamX, tamY, 0)); //7

        normals.Add(new Vector3(0, 0, -tamZ));
        normals.Add(new Vector3(0, 0, -tamZ));
        normals.Add(new Vector3(0, 0, -tamZ));
        normals.Add(new Vector3(0, 0, -tamZ));

        textCoords.Add(new Vector2(0, 0));
        textCoords.Add(new Vector2(0, tamY));
        textCoords.Add(new Vector2(tamX, 0));
        textCoords.Add(new Vector2(tamX, tamY));

        //C3
        vertices.Add(new Vector3(tamX, 0, 0)); //8
        vertices.Add(new Vector3(tamX, tamY, 0)); //9
        vertices.Add(new Vector3(tamX, 0, tamZ)); //10
        vertices.Add(new Vector3(tamX, tamY, tamZ)); //11

        normals.Add(new Vector3(1, 0, 0));
        normals.Add(new Vector3(1, 0, 0));
        normals.Add(new Vector3(1, 0, 0));
        normals.Add(new Vector3(1, 0, 0));

        textCoords.Add(new Vector2(0, 0));
        textCoords.Add(new Vector2(0, tamY));
        textCoords.Add(new Vector2(tamZ, 0));
        textCoords.Add(new Vector2(tamZ, tamY));
        //C4
        vertices.Add(new Vector3(tamX, 0, tamZ)); //12
        vertices.Add(new Vector3(tamX, tamY, tamZ)); //13
        vertices.Add(new Vector3(0, 0, tamZ)); //15
        vertices.Add(new Vector3(0, tamY, tamZ)); //14
        

        normals.Add(new Vector3(0, 0, 1));
        normals.Add(new Vector3(0, 0, 1));
        normals.Add(new Vector3(0, 0, 1));
        normals.Add(new Vector3(0, 0, 1));

        textCoords.Add(new Vector2(0, 0));
        textCoords.Add(new Vector2(0, tamY));
        textCoords.Add(new Vector2(tamX, 0));
        textCoords.Add(new Vector2(tamX, tamY));

        //C5
        vertices.Add(new Vector3(0, 0, tamZ)); //16
        vertices.Add(new Vector3(0, tamY, tamZ)); //17
        vertices.Add(new Vector3(0, 0, 0)); //18
        vertices.Add(new Vector3(0, tamY, 0)); //19
        

        normals.Add(new Vector3(-1, 0, 0));
        normals.Add(new Vector3(-1, 0, 0));
        normals.Add(new Vector3(-1, 0, 0));
        normals.Add(new Vector3(-1, 0, 0));

        textCoords.Add(new Vector2(0, 0));
        textCoords.Add(new Vector2(0, tamY));
        textCoords.Add(new Vector2(tamZ, 0));
        textCoords.Add(new Vector2(tamZ, tamY));

        //C6
        vertices.Add(new Vector3(0, tamY, 0)); //20
        vertices.Add(new Vector3(tamX, tamY, 0)); //21
        vertices.Add(new Vector3(tamX, tamY, tamZ)); //22
        vertices.Add(new Vector3(0, tamY, tamZ)); //23
        
        

        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 1, 0));

        textCoords.Add(new Vector2(0, 0));
        textCoords.Add(new Vector2(tamZ, 0));
        textCoords.Add(new Vector2(0, tamX));
        textCoords.Add(new Vector2(tamZ,tamX));
        // textCoords.Add(new Vector2(0, 0));
        //textCoords.Add(new Vector2(0, tamZ));
        //textCoords.Add(new Vector2(tamX, 0));
        //textCoords.Add(new Vector2(tamX ,tamZ));
        // Crear los triángulos, a partir de los indices de los vertices en el vector de vertices
        // Cuidado! orientación mano izquierda, vertices en sentido horario
        triangles = new List<int>();

        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);

        triangles.Add(3);
        triangles.Add(1);
        triangles.Add(2);

        triangles.Add(4);
        triangles.Add(5);
        triangles.Add(6);

        triangles.Add(5);
        triangles.Add(7);
        triangles.Add(6);

        triangles.Add(8);
        triangles.Add(9);
        triangles.Add(10);

        triangles.Add(10);
        triangles.Add(9);
        triangles.Add(11);

        triangles.Add(12);
        triangles.Add(13);
        triangles.Add(14);

        triangles.Add(13);
        triangles.Add(15);
        triangles.Add(14);
    
        triangles.Add(17);
        triangles.Add(18);
        triangles.Add(16);

        triangles.Add(19);
        triangles.Add(18);
        triangles.Add(17);

        triangles.Add(20);
        triangles.Add(22);
        triangles.Add(21);

        triangles.Add(23);
        triangles.Add(22);
        triangles.Add(20);

  
        // Crear una nueva malla de triángulos a partir de los datos anteriores
        crearMesh();
    }
    void crearTriangulo() 
    {
        // Crear los vertices
        vertices = new List<Vector3>();
        vertices.Add(new Vector3(0, 0, 0));
        vertices.Add(new Vector3(0, 0, 1));
        vertices.Add(new Vector3(1, 0, 0));

        // Crear las normales de cada vertice
        normals = new List<Vector3>();
        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 1, 0));

        // Crear las coordenadas de textura de cada vertice
        textCoords = new List<Vector2>();
        textCoords.Add(new Vector2(0, 0));
        textCoords.Add(new Vector2(0, 1));
        textCoords.Add(new Vector2(1, 0));

        // Crear los triángulos, a partir de los indices de los vertices en el vector de vertices
        // Cuidado! orientación mano izquierda, vertices en sentido horario
        triangles = new List<int>();
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

        // Crear una nueva malla de triángulos a partir de los datos anteriores
        crearMesh();
    }

    void crearTira() 
    {
        // Crear las listas para añadir vertices, normales, coordenadas de textura e indices de los triángulos
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();

        // Inicializar el contador de vertices
        int tamTira = particiones + 1;
        float tamParticion = 1.0f/particiones;

        /*
        Añadir los dos primeros vertices de la tira, después
        por cada iteración del bucle se añaden 2 vertices y
        se crean dos triangulos (T1 y T2) con los dos vertices 
        anteriores.

        1 - - 3 - - 5 - - 
        |T2 / |   / |
        |  /  |  /  |
        | / T1| /   |
        0 - - 2 - - 4 - - 

        */

        // - Vertice inferior
        vertices.Add(new Vector3(0,0,0));
        normals.Add(new Vector3(0,1,0));
        textCoords.Add(new Vector2(0,0));
        
        // - Vertice superior
        vertices.Add(new Vector3(0,0,tamParticion));
        normals.Add(new Vector3(0,1,0));
        textCoords.Add(new Vector2(0,1));

        for (int i=1; i<=particiones; i++) 
        {
            float X = tamParticion * i;
            
            // - Vertice inferior
            vertices.Add(new Vector3(X,0,0));
            normals.Add(new Vector3(0,1,0));
            textCoords.Add(new Vector2(X,0));

            // - Vertice superior
            vertices.Add(new Vector3(X,0,tamParticion));
            normals.Add(new Vector3(0,1,0));
            textCoords.Add(new Vector2(X,1));
            
            int currIndex = vertices.Count-1;

            // - Triangulo 1
            triangles.Add(currIndex);       //3
            triangles.Add(currIndex-1);     //2
            triangles.Add(currIndex-3);     //0

            // - Triangulo 2
            triangles.Add(currIndex);       //3
            triangles.Add(currIndex-3);     //0
            triangles.Add(currIndex-2);     //1
        }

        // Crear la mesh con los vectores calculados
        crearMesh();
    }

    void crearPlano() 
    {
        // Crear las listas para añadir vertices, normales, coordenadas de textura e indices de los triángulos
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();

        // Inicializar el contador de vertices
        int currIndex = 0;
        int tamTira = particiones + 1;

        // Calcular el tamaño de cada celda, 
        // al ser un cuadrado es igual en X y en Z
        float tamParticion = 1.0f/particiones;

        for (int i=0; i<=particiones; i++) {
            
            float Z = tamParticion * i;             // Calculo de la coordenada Z (filas)

            for (int j=0; j<=particiones; j++) {
                
                float X = tamParticion * j;          // Calculo de la coordenada X (columna)
                
                vertices.Add(new Vector3(X,0,Z));
                normals.Add(new Vector3(0,1,0));     // En el plano XZ el vector normal es el vector Y
                textCoords.Add(new Vector2(X,Z));

                if ((i>0) && (j>0)) {

                    // A partir de la primera fila y la primera columna, 
                    // cada vertice da lugar a 2 nuevos triángulos (sentido horario)
                    /*
                            currIndex - 1  -->              O --- X   <-- currIndex
                                                            | 1 / |
                                                            |  /  |
                                                            | / 2 |
                            currIndex - tamTira - 1  -->    O --- O   <-- currIndex - tamTira
                    
                    */

                    // Triángulo 1
                    triangles.Add(currIndex); 
                    triangles.Add(currIndex - tamTira - 1); 
                    triangles.Add(currIndex - 1); 
                    
                    // Triángulo 2
                    triangles.Add(currIndex); 
                    triangles.Add(currIndex - tamTira); 
                    triangles.Add(currIndex - tamTira - 1);
                }
                
                // Incrementar el contador de vertices
                currIndex ++;
            }
        }

        // Crear una nueva malla de triángulos a partir de los datos anteriores
        crearMesh();
    }

    void crearEsfera() 
    {

        // Ecuación de la esfera por gajos
        // [U,V] en el rango [0,1]
        // x = radio * cos(2*PI*U) * sin(PI*V)
        // y = radio * cos(PI*V)
        // z = radio * sin(2*PI*U) * sin(PI*V)

        float radio = 1;

        // Crear las listas para añadir vertices, normales, coordenadas de textura e indices de los triángulos
        vertices = new List<Vector3>();
        normals = new List<Vector3>();
        textCoords = new List<Vector2>();
        triangles = new List<int>();

        float paso = 1.0f / particiones;

        int currIndex = 0;
        int tamTira = particiones + 1;

        for (int i=0; i<=particiones; i++) {
            float u = paso * i;
            for (int j=0; j<=particiones; j++) {
                float v = paso * j;

                Vector3 aux = new Vector3(
                    Mathf.Cos(2*Mathf.PI*u) * Mathf.Sin(Mathf.PI * v),
                    Mathf.Cos(Mathf.PI*v),
                    Mathf.Sin(2*Mathf.PI*u) * Mathf.Sin(Mathf.PI * v)
                );

                vertices.Add(radio * aux);
                normals.Add(aux.normalized);    // Para una esfera centrada en 0, la normal coincide con las coordenadas del vertice
                textCoords.Add(new Vector2(u,1.0f-v));

                if (i>0 && j>0) {
                    triangles.Add(currIndex);
                    triangles.Add(currIndex - tamTira);
                    triangles.Add(currIndex - tamTira - 1);

                    triangles.Add(currIndex);
                    triangles.Add(currIndex - tamTira - 1);
                    triangles.Add(currIndex - 1);
                }

                // Incrementar el contador de vertices
                currIndex ++;
            }
        }

        // Crear una nueva malla de triángulos a partir de los datos anteriores
        crearMesh();
    }

    // Función para crear la mesh a partir de los vectores calculados en las funciones anteriores
    public void crearMesh() {

        // Crear la mesh con toda la información
        Mesh m = new Mesh();
        m.vertices = vertices.ToArray();
        m.normals = normals.ToArray();
        m.uv = textCoords.ToArray();
        m.triangles = triangles.ToArray();

        // Llamada obligatoria para recalcular la información 
        // de la malla a partir de los vectores asignados
        m.RecalculateBounds();  

        // Asignar la malla al componente MeshFilter del Gameobject
        GetComponent<MeshFilter>().sharedMesh = m;
    }
}