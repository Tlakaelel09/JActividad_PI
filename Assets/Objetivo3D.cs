using UnityEngine;

public class Objetivo3D : MonoBehaviour
{
    public int puntos = 10;

    void Update()
    {
        transform.Rotate(Vector3.up * 90f * Time.deltaTime);
        float nuevaY = 1f + Mathf.Sin(Time.time * 3f) * 0.2f;
        transform.localPosition = new Vector3(transform.localPosition.x, nuevaY, transform.localPosition.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GestorPuntaje3D.instancia != null)
            {
                GestorPuntaje3D.instancia.SumarPuntos(puntos);
            }
            Destroy(gameObject);
        }
    }
}