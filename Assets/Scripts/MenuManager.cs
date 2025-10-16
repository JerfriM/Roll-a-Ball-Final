using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para la carga de escenas

public class MenuManager : MonoBehaviour
{
    // Función para iniciar el juego (carga el primer nivel)
    public void IniciarJuego()
    {
        // Carga la escena "Nivel1" (índice 2 en el Build Settings)
        SceneManager.LoadScene("Nivel1");
    }

    // Función para mostrar la escena de opciones
    public void MostrarOpciones()
    {
        // Carga la escena "Opciones"
        SceneManager.LoadScene("Opciones");
    }

    // Función para volver al menú principal desde Opciones o Nivel Ganado/Perdido
    public void VolverAlMenuPrincipal()
    {
        // Carga la escena "MenuPrincipal" (índice 0)
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Función para salir de la aplicación
    public void SalirDelJuego()
    {
        // Cierra la aplicación (solo funciona en el juego compilado)
        Application.Quit();

        // Cierra el juego en el editor de Unity (Solo para pruebas)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}