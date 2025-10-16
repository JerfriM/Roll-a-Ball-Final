using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para la corrutina (espera de tiempo)

public class GameManager : MonoBehaviour
{
    // VARIABLES P�BLICAS Y CONEXIONES (A ENLAZAR EN EL INSPECTOR)
    public Text textoTimer;
    public Text textoGanar;
    public float tiempoLimite = 60f; // Tiempo inicial del temporizador

    // Referencia al JugadorController (para apagar la m�sica, etc.)
    public JugadorController jugadorController;

    // VARIABLES PRIVADAS
    private bool juegoTerminado = false;

    void Start()
    {
        // 1. Inicializaci�n
        // Restaura la velocidad del tiempo (clave si vienes de una derrota)
        Time.timeScale = 1f;

        // Limpia el texto de victoria/derrota al iniciar
        if (textoGanar != null)
        {
            textoGanar.text = "";
        }
    }

    void Update()
    {
        if (juegoTerminado) return; // Si ya termin�, no hagas nada

        tiempoLimite -= Time.deltaTime; // Resta el tiempo desde el �ltimo frame

        // 2. Actualizaci�n del Temporizador
        if (textoTimer != null)
        {
            // Muestra el tiempo redondeado al entero superior ("0" elimina decimales)
            textoTimer.text = "Tiempo: " + Mathf.Ceil(tiempoLimite).ToString("0");
        }

        // 3. Condici�n de Derrota por Tiempo
        if (tiempoLimite <= 0)
        {
            PerderJuego("�Se acab� el tiempo!");
        }
    }

    public void GanarJuego()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        // 1. Detiene la m�sica (si no lo hizo el anillo)
        if (jugadorController != null && jugadorController.MusicaAmbiente != null)
        {
            jugadorController.MusicaAmbiente.Stop();
        }

        // 2. L�gica de Transici�n al Siguiente Nivel
        int nivelActual = SceneManager.GetActiveScene().buildIndex;
        int proximoNivel = nivelActual + 1;

        // El total de escenas en el Build Settings (incluye Men�, Opciones, Nivel1...Nivel16)
        int totalEscenas = SceneManager.sceneCountInBuildSettings;

        // Comprueba si el pr�ximo �ndice est� dentro del total de escenas (es decir, existe un nivel siguiente)
        if (proximoNivel < totalEscenas)
        {
            // Carga el siguiente nivel inmediatamente
            SceneManager.LoadScene(proximoNivel);
        }
        else
        {
            // Si no hay m�s niveles, el juego ha terminado.
            if (textoGanar != null)
            {
                textoGanar.text = "�JUEGO TERMINADO! �FELICIDADES!";
            }
            // Espera 10 segundos y regresa al men� principal
            StartCoroutine(VolverAlMenu(10f));
        }
    }

    public void PerderJuego(string mensaje)
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        if (textoGanar != null)
        {
            textoGanar.text = mensaje;
        }

        // Congela el juego
        Time.timeScale = 0f;

        // Detiene la m�sica
        if (jugadorController != null && jugadorController.MusicaAmbiente != null)
        {
            jugadorController.MusicaAmbiente.Stop();
        }

        // Espera 10 segundos y regresa al men� principal
        StartCoroutine(VolverAlMenu(10f));
    }

    // Corrutina para esperar antes de volver al men�
    IEnumerator VolverAlMenu(float segundos)
    {
        // Usa WaitForSecondsRealtime para esperar 10 segundos aunque el juego est� congelado (Time.timeScale = 0f)
        yield return new WaitForSecondsRealtime(segundos);

        // Restablece el tiempo para que el men� funcione correctamente
        Time.timeScale = 1f;

        // Carga la escena del men� principal (�ndice 0)
        SceneManager.LoadScene("MenuPrincipal");
    }
}