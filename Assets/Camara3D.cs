using UnityEngine;

public class Camara3D : MonoBehaviour
{
    public Transform jugador;
    public float distanciaZ = -8f;
    public float alturaFijaY = 6f;
    public float suavizado = 5f;

    void LateUpdate()
    {
        if (jugador == null) return;

        Vector3 posicionDeseada = new Vector3(
            jugador.position.x,
            alturaFijaY,
            jugador.position.z + distanciaZ
        );

        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
    }
}
