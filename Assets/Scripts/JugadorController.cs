using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JugadorController : MonoBehaviour
{
    // VARIABLES PÚBLICAS Y CONEXIONES
    public float velocidad = 10f;

    // UI
    public Text textoContador;
    public Text textoGanar;

    // Gestión Central
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

        // 2. INICIALIZACIÓN
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
            // LÓGICA DE AUDIO (DIFERENCIACIÓN POR TAG)
            if (other.gameObject.CompareTag("Anillo"))
            {
                // ANILLO: Detiene la música de fondo
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

                // NUEVA LÓGICA: Inicia la música si está detenida
                if (MusicaAmbiente != null && !MusicaAmbiente.isPlaying)
                {
                    MusicaAmbiente.Play();
                }
            }

            // Lógica de Puntuación
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
                textoGanar.text = "¡Ganaste!";
            }
        }
    }
}