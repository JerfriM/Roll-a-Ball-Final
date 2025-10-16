using UnityEngine;

public class MoverPlataforma : MonoBehaviour
{
    // Variables p�blicas para ajustar el movimiento en el Inspector
    public float velocidad = 2f; // Rapidez del movimiento (cuanto m�s alto, m�s r�pido va y viene)
    public float distancia = 5f; // Distancia m�xima desde el punto de inicio

    private Vector3 posicionInicial; // Guardaremos la posici�n original del objeto

    void Start()
    {
        // Guardamos la posici�n exacta donde colocaste el objeto en la escena
        posicionInicial = transform.position;
    }

    void Update()
    {
        // 1. Calculamos la nueva posici�n usando la funci�n Seno (Crea un bucle de ida y vuelta)
        // Time.time crece continuamente. Sin(Time.time) oscila entre -1 y 1.
        float movimientoOscilatorio = Mathf.Sin(Time.time * velocidad) * distancia;

        // 2. Definimos la nueva posici�n Vector3
        Vector3 nuevaPosicion = posicionInicial;

        // Aplicamos el movimiento en el eje X (Puedes cambiar a Y o Z si quieres otro movimiento)
        nuevaPosicion.x += movimientoOscilatorio;

        // 3. Movemos el objeto a esa posici�n
        transform.position = nuevaPosicion;
    }
}