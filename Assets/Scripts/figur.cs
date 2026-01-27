using UnityEngine;
using TMPro;

public class figur : MonoBehaviour
{
    private Light[] lights;
    private bool wasInShadow = false;
    public static bool IsInShadow { get; private set; }
    public TextMeshPro displayText;
    public static bool shadowEntered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lights = FindObjectsOfType<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isInLight = false;
        foreach (Light light in lights)
        {
            float distance = Vector3.Distance(transform.position, light.transform.position);
            if (distance < 7f)
            {
                isInLight = true;
                break;
            }
        }
        
        IsInShadow = !isInLight;
        
        if (!isInLight && !wasInShadow)
        {
            Debug.Log("Figur steht im Schatten");
            shadowEntered = true;
            if (displayText != null)
            {
                displayText.text = "2";
                displayText.fontSize = 1.0f;
                displayText.gameObject.SetActive(true);
            }
            wasInShadow = true;
        }
        else if (isInLight && wasInShadow)
        {
            wasInShadow = false;
        }
    }
}
