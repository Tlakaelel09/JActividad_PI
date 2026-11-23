using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPart : MonoBehaviour
{
    private bool esMovil;
    private float velocidadMovimiento;
    private float inicioX;
    private float rango = 2.5f;

    void Start()
    {
        inicioX = transform.localPosition.x;
        esMovil = Random.value < 0.4f;
        velocidadMovimiento = Random.Range(1.5f, 3f);
    }

    void Update()
    {
        if (esMovil)
        {
            float nuevaX = inicioX + Mathf.Sin(Time.time * velocidadMovimiento) * rango;
            transform.localPosition = new Vector3(nuevaX, transform.localPosition.y, transform.localPosition.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}