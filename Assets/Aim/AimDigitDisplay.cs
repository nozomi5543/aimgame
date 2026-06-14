using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AimDigitDisplay : MonoBehaviour
{
    [Header("Digits")]
    [SerializeField]
    private Image[] digitImages;

    [Header("Sprites 0-9")]
    [SerializeField]
    private Sprite[] digitSprites;

    [SerializeField]
    private int digitCount = 4;

    [Header("Blink Effect")]
    [SerializeField]
    private bool blinkOnChange = true;

    [SerializeField]
    private float blinkInterval = 0.03f;

    [SerializeField]
    private int blinkCount = 2;

    private string currentText = "";

    public void SetNumber(int value)
    {
        string newText = value.ToString($"D{digitCount}");

        for (int i = 0; i < digitCount; i++)
        {
            int digit = newText[i] - '0';

            digitImages[i].sprite = digitSprites[digit];

            bool changed =
                currentText.Length == digitCount &&
                currentText[i] != newText[i];

            if (blinkOnChange && changed)
            {
                StartCoroutine(BlinkDigit(i));
            }
        }

        currentText = newText;
    }

    private IEnumerator BlinkDigit(int index)
    {
        Image image = digitImages[index];

        for (int i = 0; i < blinkCount; i++)
        {
            image.enabled = false;
            yield return new WaitForSeconds(blinkInterval);

            image.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
        }

        image.enabled = true;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (digitImages != null)
        {
            digitCount = digitImages.Length;
        }
    }
#endif
}