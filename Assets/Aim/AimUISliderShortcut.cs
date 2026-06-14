using UnityEngine;
using UnityEngine.UI;

public class AimUISliderShortcut : MonoBehaviour
{
    public enum ControlDirection
    {
        LeftRight,
        UpDown
    }

    [Header("Target")]
    [SerializeField] private Slider slider;

    [Header("Shortcut Settings")]
    [SerializeField] private ControlDirection controlDirection = ControlDirection.LeftRight;

    [Tooltip("1•b‚ ‚½‚è‚Ì•Ï‰»—Ê")]
    [SerializeField] private float speed = 10f;

    private void Update()
    {
        if (slider == null)
            return;

        float delta = speed * Time.deltaTime;

        switch (controlDirection)
        {
            case ControlDirection.LeftRight:
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    slider.value -= delta;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    slider.value += delta;
                }
                break;

            case ControlDirection.UpDown:
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    slider.value -= delta;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    slider.value += delta;
                }
                break;
        }

        // Slider‚Ì”ÍˆÍ“à‚ÉŽû‚ß‚é
        slider.value = Mathf.Clamp(
            slider.value,
            slider.minValue,
            slider.maxValue);
    }
}