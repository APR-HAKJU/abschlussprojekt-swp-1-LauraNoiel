using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public GameObject redCube;
    public GameObject blueCube;
    public GameObject yellowCube;
    private static int sequence = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject zahlObject = GameObject.FindWithTag("zahl");

        // set object with tag "zahl" to font size 0 or deactivate it
        if (zahlObject != null)
        {
            zahlObject.GetComponent<TextMeshProUGUI>().text = "";
            Debug.Log("Deactivated object with tag 'zahl' at start.");
        }
        else
        {
            Debug.Log("No object with tag 'zahl' found at start.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Mouse clicked, casting ray.");
            RaycastHit[] hits = Physics.RaycastAll(ray);
            Debug.Log("Number of hits: " + hits.Length);

            if (hits.Length > 0)
            {
                // Sort by distance if needed, but RaycastAll already sorts by distance
                RaycastHit hit = hits[1]; // Closest hit
                Debug.Log("Hit object: " + hit.transform.name);
                hit.transform.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);

                //determine color by object name
                string color = hit.transform.name.ToLower();
                Debug.Log("Hit object tag (color): " + color);
                // Only process clicks on colored cubes
                if (color == "yellow" || color == "red" || color == "blue")
                {
                    Debug.Log("Clicked cube color: " + color);
                    Debug.Log("Current sequence state: " + sequence);

                    // check if the clicked cube is in the correct sequence
                    // the sequence is yellow -> red -> blue
                    if (color == "yellow" && sequence == 0)
                    {
                        
                        Debug.Log("Yellow cube clicked first.");
                        sequence = 1;
                    }
                    else if (color == "red" && sequence == 1)
                    {
                        Debug.Log("Red cube clicked second.");
                        sequence = 2;
                    }
                    else if (color == "blue" && sequence == 2)
                    {
                        sequence = 3;
                        Debug.Log("Correct sequence completed!");
                    }
                    else
                    {
                        sequence = 0;
                        Debug.Log("Wrong order! Start over.");
                    }

                    if (sequence == 3)
                    {
                        // Reset sequence after successful completion
                        Debug.Log("You have clicked the cubes in the correct order: yellow, red, blue.");
                        // set object with tag "zahl" to active
                        GameObject zahlObject = GameObject.FindWithTag("zahl");
                        if (zahlObject != null)
                        {
                            zahlObject.GetComponent<TextMeshProUGUI>().text = "3";
                            Debug.Log("Activated object with tag 'zahl'.");
                        }
                        else
                        {
                            Debug.Log("No object with tag 'zahl' found.");
                        }
                        sequence = 0;
                    }
                }
            }

        }

    }

    void OnMouseDown()
    {
        
    }
}
