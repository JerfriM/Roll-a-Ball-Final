using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para la carga de escenas

public class MenuManager : MonoBehaviour
{
    // Funci�n para iniciar el juego (carga el primer nivel)
    public void IniciarJuego()
    {
        // Carga la escena "Nivel1" (�ndice 2 en el Build Settings)
        SceneManager.LoadScene("Nivel1");
    }

    // Funci�n para mostrar la escena de opciones
    public void MostrarOpciones()
    {
        // Carga la escena "Opciones"
        SceneManager.LoadScene("Opciones");
    }

    // Funci�n para volver al men� principal desde Opciones o Nivel Ganado/Perdido
    public void VolverAlMenuPrincipal()
    {
        // Carga la escena "MenuPrincipal" (�ndice 0)
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Funci�n para salir de la aplicaci�n
    public void SalirDelJuego()
    {
        // Cierra la aplicaci�n (solo funciona en el juego compilado)
        Application.Quit();

        // Cierra el juego en el editor de Unity (Solo para pruebas)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}