using UnityEngine;

public class GestorVisual3D : MonoBehaviour
{
    public static GestorVisual3D instancia;

    [Header("Referencias de Escena")]
    public Camera camaraPrincipal;
    public Light luzDireccional;
    public Renderer rendererJugador;

    [Header("Fondo Dinámico (Editable en Inspector)")]
    public Color[] coloresFondo = new Color[]
    {
        new Color(0.12f, 0.08f, 0.22f),
        new Color(0.08f, 0.20f, 0.30f),
        new Color(0.25f, 0.12f, 0.10f),
        new Color(0.08f, 0.22f, 0.15f)
    };
    public float velocidadTransicion = 2f;

    [Header("Propiedades Visuales del Personaje")]
    public Color colorJugador = new Color(1f, 0.33f, 0f);
    [Range(0f, 1f)] public float metalico = 0.4f;
    [Range(0f, 1f)] public float suavidad = 0.8f;

    [Header("Iluminación Ambiental")]
    [Range(0.5f, 2.5f)] public float intensidadLuz = 1.2f;
    public Color colorLuz = Color.white;

    private int indiceFondo = 0;
    private Material materialJugador;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        if (camaraPrincipal != null)
        {
            camaraPrincipal.clearFlags = CameraClearFlags.SolidColor;
            if (coloresFondo.Length > 0)
                camaraPrincipal.backgroundColor = coloresFondo[0];
        }

        if (rendererJugador != null)
        {
            materialJugador = rendererJugador.material;
        }

        AplicarPropiedadesVisuales();
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 20f;
        RenderSettings.fogEndDistance = 55f;
    }

    void Update()
    {
        // Transición suave del color de fondo
        if (camaraPrincipal != null && coloresFondo.Length > 0)
        {
            camaraPrincipal.backgroundColor = Color.Lerp(
                camaraPrincipal.backgroundColor,
                coloresFondo[indiceFondo],
                Time.deltaTime * velocidadTransicion
            );
            RenderSettings.fogColor = camaraPrincipal.backgroundColor;
        }

        if (Input.GetKeyDown(KeyCode.F)) CambiarFondo();
        if (Input.GetKeyDown(KeyCode.C)) CambiarColorJugadorAleatorio();
    }

    void OnValidate()
    {
        if (Application.isPlaying)
        {
            AplicarPropiedadesVisuales();
        }
    }

    public void AplicarPropiedadesVisuales()
    {

        if (materialJugador != null)
        {
            materialJugador.SetColor("_BaseColor", colorJugador);
            materialJugador.SetFloat("_Metallic", metalico);
            materialJugador.SetFloat("_Smoothness", suavidad);
        }


        if (luzDireccional != null)
        {
            luzDireccional.intensity = intensidadLuz;
            luzDireccional.color = colorLuz;
        }
    }

    public void CambiarFondo()
    {
        if (coloresFondo.Length == 0) return;
        indiceFondo = (indiceFondo + 1) % coloresFondo.Length;
    }

    public void CambiarColorJugadorAleatorio()
    {
        colorJugador = Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.7f, 1f);
        AplicarPropiedadesVisuales();
    }
}
