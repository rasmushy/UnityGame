using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // @author rasmushy
    // get health data
    private HealthSystem healthSystem;
    // make slider for healthbar
    public Slider healthSlider;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        // use event to check is it necessery to update on this frame
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    // using event in Health System and adds that value to the slider.
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        healthSlider.value = healthSystem.GetHealthPercent();

    }

}
