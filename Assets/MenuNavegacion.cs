using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavegacion : MonoBehaviour
{
    public void IrAlJuego()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
