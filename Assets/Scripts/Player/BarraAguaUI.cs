using UnityEngine;
using UnityEngine.UI;

public class BarraAguaUI : MonoBehaviour
{
    public BarcoController barcoController; // Referencia al controlador del barco
    public Slider sliderAgua; // Referencia al Slider de la UI

    private void Start()
    {
        // Configurar el Slider con el rango correcto
        sliderAgua.minValue = 0f;
        sliderAgua.maxValue = barcoController.aguaMaxima;
        sliderAgua.value = sliderAgua.maxValue; // Establecer el valor inicial al máximo
    }
    
    private void Update()
    {
        if (barcoController != null && sliderAgua != null)
        {
            // Actualizar el Slider de la UI para que coincida con el nivel de agua actual del barco
            sliderAgua.value = barcoController.GetAguaActual() / barcoController.aguaMaxima;
        }
    }
}
