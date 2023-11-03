using UnityEngine;
using Gentleland.StemapunkUI.DemoAndExample; // Añade esta línea si ClockSlider está dentro de este espacio de nombres

public class GestorTormenta : MonoBehaviour
{
    public Transform esquinaInferiorIzquierda; // Asigna un Transform que indique la esquina inferior izquierda del área
    public Transform esquinaSuperiorDerecha; // Asigna un Transform que indique la esquina superior derecha del área
    public GameObject tormentaPrefab; // El prefab de la tormenta de arena
    public ClockSlider clockSlider; // Referencia al ClockSlider

    private GameObject tormentaActual;
    private float tiempoHastaSiguienteTormenta = 15f; // Tiempo inicial hasta la próxima tormenta
    private float tiempoInicial = 15f; // Tiempo total antes de que la tormenta se mueva

    private void Start()
    {
        // Inicializamos la tormenta en una posición aleatoria
        MoverTormenta();
    }

    private void Update()
    {
        // Actualizamos la cuenta atrás
        tiempoHastaSiguienteTormenta -= Time.deltaTime;

        // Actualizamos el ClockSlider
        if (clockSlider != null)
        {
            clockSlider.Value = tiempoHastaSiguienteTormenta / tiempoInicial;
        }

        // Si el contador llega a cero, movemos la tormenta y reseteamos el contador
        if (tiempoHastaSiguienteTormenta <= 0)
        {
            MoverTormenta();
            tiempoHastaSiguienteTormenta = tiempoInicial; // Resetear el contador para la próxima tormenta
        }
    }
    
    // Método para mover la tormenta a una nueva posición aleatoria
    public void MoverTormenta()
    {
        // Si ya existe una tormenta, la destruimos
        if (tormentaActual != null)
        {
            Destroy(tormentaActual);
        }

        // Calculamos una nueva posición aleatoria dentro del área designada
        float nuevaPosX = Random.Range(esquinaInferiorIzquierda.position.x, esquinaSuperiorDerecha.position.x);
        float nuevaPosY = Random.Range(esquinaInferiorIzquierda.position.y, esquinaSuperiorDerecha.position.y);

        // Creamos una nueva tormenta en la nueva posición
        Vector3 nuevaPosicion = new Vector3(nuevaPosX, nuevaPosY, 0);
        tormentaActual = Instantiate(tormentaPrefab, nuevaPosicion, Quaternion.identity);

        // Si tu tormenta tiene algún método de inicialización, puedes llamarlo aquí
        // Ejemplo: tormentaActual.GetComponent<TuScriptTormenta>().Inicializar();
    }
}
