using UnityEngine;

public class objekt_erscheint : MonoBehaviour
{
    private bool activated = false;
    private Renderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("shadowEntered: " + figur.shadowEntered);
        if (!activated && figur.shadowEntered)
        {
            renderer.enabled = true;
            activated = true;
        }
    }
}
