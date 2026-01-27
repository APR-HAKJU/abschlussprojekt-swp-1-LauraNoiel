using UnityEngine;

public class light2 : MonoBehaviour
{
    private GameObject player;
    public float minDistance = 2f;
    public float maxDistance = 5f;
    public float moveSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject has the 'Player' tag.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Input.GetKey(KeyCode.Alpha3))
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }
    }
}
