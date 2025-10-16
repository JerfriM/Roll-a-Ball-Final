using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JugadorController : MonoBehaviour
{
    // VARIABLES P�BLICAS Y CONEXIONES
    public float velocidad = 10f;

    // UI
    public Text textoContador;
    public Text textoGanar;

    // Gesti�n Central
    public GameManager gameManager;

    // Audio
    public AudioSource MusicaAmbiente; // Fuente de Audio del objeto AudioAmbiental
    public AudioClip SonidoMonedaCubo; // El clip de sonido de la moneda

    // VARIABLES PRIVADAS
    private Rigidbody rb;
    private AudioSource audioJugador; // AudioSource del propio Jugador
    private int contador;
    private const int TOTAL_COLECCIONABLES = 12;

    void Start()
    {
        // 1. CAPTURA DE COMPONENTES
        rb = GetComponent<Rigidbody>();
        audioJugador = GetComponent<AudioSource>(); // Captura el AudioSource del jugador

        // 2. INICIALIZACI�N
        contador = 0;
        setTextoContador();
        textoGanar.text = "";
    }

    void FixedUpdate()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);
        rb.AddForce(movimiento * velocidad);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable") || other.gameObject.CompareTag("Anillo"))
        {
            // L�GICA DE AUDIO (DIFERENCIACI�N POR TAG)
            if (other.gameObject.CompareTag("Anillo"))
            {
                // ANILLO: Detiene la m�sica de fondo
                if (MusicaAmbiente != null)
                {
                    MusicaAmbiente.Stop();
                }
            }
            else // Es un CUBO (Tag "Coleccionable")
            {
                // CUBO: Reproduce el sonido de la moneda
                if (audioJugador != null && SonidoMonedaCubo != null)
                {
                    audioJugador.PlayOneShot(SonidoMonedaCubo);
                }

                // NUEVA L�GICA: Inicia la m�sica si est� detenida
                if (MusicaAmbiente != null && !MusicaAmbiente.isPlaying)
                {
                    MusicaAmbiente.Play();
                }
            }

            // L�gica de Puntuaci�n
            other.gameObject.SetActive(false);
            contador = contador + 1;
            setTextoContador();
        }
    }

    void setTextoContador()
    {
        textoContador.text = "Contador: " + contador.ToString();

        if (contador >= TOTAL_COLECCIONABLES)
        {
            if (gameManager != null)
            {
                gameManager.GanarJuego();
            }
            else
            {
                textoGanar.text = "�Ganaste!";
            }
        }
    }
}