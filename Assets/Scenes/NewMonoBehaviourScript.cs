using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private static int sequence = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Der rote Würfel wurde angeklickt");
    }
}
