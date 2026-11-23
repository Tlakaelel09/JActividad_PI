using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento3D : MonoBehaviour
{
    public float velocidad = 6f;
    public float fuerzaSalto = 6f;

    [Header("SuperSpeed UX (4.6)")]
    public float multiplicadorSuperSpeed = 1.8f;
    [Range(0.5f, 3f)] public float duracionSuperSpeed = 2.5f;

    private Rigidbody rb;
    private TrailRenderer estela;
    private bool enSuelo = true;
    private bool enSuperSpeed = false;
    private Coroutine rutinaSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        estela = GetComponent<TrailRenderer>();

        if (estela != null)
        {
            estela.emitting = false;
        }
    }

    void FixedUpdate()
    {
        float moverX = Input.GetAxis("Horizontal");
        float moverZ = Input.GetAxis("Vertical");

        // Controles táctiles para Android
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            moverZ = 1f;
            moverX = Mathf.Clamp(toque.deltaPosition.x * 0.15f, -1f, 1f);
        }

        float velocidadActual = enSuperSpeed ? velocidad * multiplicadorSuperSpeed : velocidad;
        Vector3 movimiento = new Vector3(moverX * velocidadActual, rb.linearVelocity.y, moverZ * velocidadActual);
        rb.linearVelocity = movimiento;
    }

    void Update()
    {

        if (Camera.main != null)
        {
            float fovObjetivo = enSuperSpeed ? 75f : 60f;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovObjetivo, Time.deltaTime * 5f);
        }

        bool saltoTactil = false;
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            if (toque.deltaPosition.y > 25f || Input.touchCount == 2)
            {
                saltoTactil = true;
            }
        }

        if ((Input.GetKeyDown(KeyCode.Space) || saltoTactil) && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        }

        if (transform.position.y < -3f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ActivarSuperSpeed()
    {
        if (rutinaSpeed != null) StopCoroutine(rutinaSpeed);
        rutinaSpeed = StartCoroutine(RutinaSuperSpeed());
    }

    IEnumerator RutinaSuperSpeed()
    {
        enSuperSpeed = true;
        if (estela != null) estela.emitting = true;

        yield return new WaitForSeconds(Mathf.Min(duracionSuperSpeed, 3f));

        enSuperSpeed = false;
        if (estela != null) estela.emitting = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        enSuelo = true;
    }
}