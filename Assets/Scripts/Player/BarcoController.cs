using UnityEngine;

public class BarcoController : MonoBehaviour
{
    public float velocidad = 5f;
    private float velocidadBase;
    public float consumoAguaPorSegundo = 1f;
    public float aguaMaxima = 100f; // Agrega un valor máximo para el agua
    private float aguaActual;
    private bool estaEnTormenta = false;
    
    public BarraAguaUI barraAguaUI;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidadBase = velocidad;
        aguaActual = aguaMaxima; // Inicializa el agua actual al máximo
    }

    private void Start()
    {
        // Inicializa el agua actual al máximo directamente, sin necesidad de llamar a otro método.
        aguaActual = aguaMaxima;

        // Si necesitas realizar alguna configuración inicial de la barra de UI, puedes hacerlo aquí.
        if (barraAguaUI != null && barraAguaUI.sliderAgua != null)
        {
            barraAguaUI.sliderAgua.maxValue = aguaMaxima;
            barraAguaUI.sliderAgua.value = aguaActual;
        }
        else
        {
            Debug.LogError("BarraAguaUI no está asignado en el Inspector, o falta el componente Slider.");
        }
    }

    private void Update()
    {
        MoverBarco();
        ConsumirAgua();
    }

    private void MoverBarco()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movimiento = new Vector2(inputX, inputY);

        // Solo mueve el barco si hay agua disponible
        if (aguaActual > 0)
        {
            rb.velocity = movimiento * velocidad;

            // Flip the sprite by checking the direction of the X input
            if (inputX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // No flip, original orientation
            }
            else if (inputX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Flip on X axis
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ConsumirAgua()
    {
        if (rb.velocity.magnitude > 0)
        {
            aguaActual -= consumoAguaPorSegundo * Time.deltaTime;
        }
    }

    public float GetAguaActual()
    {
        return aguaActual;
    }

    
    // Este método es llamado desde BarraAguaUI
    public void EstablecerNivelAgua(float nivel)
    {
        aguaActual = nivel;
        // Actualizar el slider de la UI si es necesario
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tormenta"))
        {
            estaEnTormenta = true;
            velocidad = velocidadBase * 0.5f; // Reduce la velocidad a la mitad
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tormenta"))
        {
            estaEnTormenta = false;
            velocidad = velocidadBase; // Restaura la velocidad normal
        }
    }
}
