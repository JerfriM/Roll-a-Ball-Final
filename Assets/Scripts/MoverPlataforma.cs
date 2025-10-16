using UnityEngine;

public class MoverPlataforma : MonoBehaviour
{
    // Variables públicas para ajustar el movimiento en el Inspector
    public float velocidad = 2f; // Rapidez del movimiento (cuanto más alto, más rápido va y viene)
    public float distancia = 5f; // Distancia máxima desde el punto de inicio

    private Vector3 posicionInicial; // Guardaremos la posición original del objeto

    void Start()
    {
        // Guardamos la posición exacta donde colocaste el objeto en la escena
        posicionInicial = transform.position;
    }

    void Update()
    {
        // 1. Calculamos la nueva posición usando la función Seno (Crea un bucle de ida y vuelta)
        // Time.time crece continuamente. Sin(Time.time) oscila entre -1 y 1.
        float movimientoOscilatorio = Mathf.Sin(Time.time * velocidad) * distancia;

        // 2. Definimos la nueva posición Vector3
        Vector3 nuevaPosicion = posicionInicial;

        // Aplicamos el movimiento en el eje X (Puedes cambiar a Y o Z si quieres otro movimiento)
        nuevaPosicion.x += movimientoOscilatorio;

        // 3. Movemos el objeto a esa posición
        transform.position = nuevaPosicion;
    }
}