using UnityEngine;
using UnityEngine.Events;

public class TemporizadorTormenta : MonoBehaviour
{
    public float duracionTormenta = 15f; // Duración de la tormenta en segundos
    private float tiempoRestante;

    // Evento para suscribir las funciones que se ejecutarán al finalizar el contador
    public UnityEvent onTormentaFinalizada;

    private void Start()
    {
        // Iniciar el temporizador
        tiempoRestante = duracionTormenta;
        onTormentaFinalizada.AddListener(GetComponent<GestorTormenta>().MoverTormenta); // Añadir MoverTormenta al evento
    }

    private void Update()
    {
        // Restamos el tiempo transcurrido desde el último frame
        tiempoRestante -= Time.deltaTime;

        // Verificamos si el temporizador ha alcanzado cero
        if (tiempoRestante <= 0)
        {
            // Invocamos el evento
            onTormentaFinalizada.Invoke();

            // Restablecemos el temporizador
            tiempoRestante = duracionTormenta;
        }
    }

    // Método público para resetear el temporizador manualmente si fuera necesario
    public void ResetearTemporizador()
    {
        tiempoRestante = duracionTormenta;
    }
}
