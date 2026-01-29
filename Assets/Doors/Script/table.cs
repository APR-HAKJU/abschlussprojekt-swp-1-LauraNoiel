using UnityEngine;

// Dieses Script zeigt genau einmal die Zahl "1" an, sobald ein Objekt
// mit dem angegebenen Tag (standard: "Ball") auf dem Tisch landet.
public class table : MonoBehaviour
{
    [Tooltip("Position der Zahl relativ zur Tisch-Transform (lokal)")]
    public Vector3 textOffset = new Vector3(0f, 1.1f, 0f);

    [Tooltip("Welches Tag muss das Objekt haben, damit die 1 erscheint. Leer = beliebiges Objekt")]
    public string targetTag = "Ball";

    [Tooltip("Wenn true, wird nur ein Objekt mit dem angegebenen Tag akzeptiert. Wenn false, wird jedes Objekt akzeptiert.")]
    public bool requireTag = false;

    private GameObject textObject;
    private TextMesh textMesh;
    private bool hasShown = false;

    void Start()
    {
        CreateTextMeshIfNeeded();
        if (textObject != null)
            textObject.SetActive(false); // zunächst unsichtbar
    }

    void CreateTextMeshIfNeeded()
    {
        if (textObject != null) return;
        textObject = new GameObject("TableNumber");
        textObject.transform.SetParent(transform);
        textObject.transform.localPosition = textOffset;
        textObject.transform.localRotation = Quaternion.identity;

        textMesh = textObject.AddComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.characterSize = 0.12f;
        textMesh.fontSize = 64;
        textMesh.color = Color.white;
        textMesh.text = "1"; // wird angezeigt, wenn der Ball landet
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[table] OnCollisionEnter with '{collision.gameObject.name}' tag='{collision.gameObject.tag}'");
        HandleEnter(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[table] OnTriggerEnter with '{other.gameObject.name}' tag='{other.gameObject.tag}'");
        HandleEnter(other.gameObject);
    }

    void HandleEnter(GameObject go)
    {
        if (hasShown) {
            Debug.Log("[table] already shown, ignoring further contacts.");
            return; // nur einmal
        }
        if (go == null || go == gameObject) return;

        // Ignoriere reine Trigger-Collider von Objekten
        var col = go.GetComponent<Collider>();
        if (col != null && col.isTrigger) {
            Debug.Log($"[table] ignored '{go.name}' because its collider is a trigger.");
            return;
        }

        // Tag-Check je nach Einstellung
        if (requireTag)
        {
            if (string.IsNullOrEmpty(targetTag))
            {
                Debug.Log("[table] requireTag is true but targetTag is empty — ignoring.");
                return;
            }
            if (!go.CompareTag(targetTag))
            {
                Debug.Log($"[table] '{go.name}' has tag '{go.tag}' — does not match required tag '{targetTag}'.");
                return;
            }
        }
        else
        {
            Debug.Log($"[table] accepting '{go.name}' (tag='{go.tag}') because requireTag is false.");
        }

        // Optional: sicherstellen, dass das Objekt einen Rigidbody hat — nur als Hinweis
        var rb = go.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log($"[table] Hinweis: '{go.name}' hat keinen Rigidbody. Die Kollision/Trigger kann trotzdem funktionieren, hängt von der Szene ab.");
        }

        // Anzeige einmalig zeigen
        hasShown = true;
        if (textObject == null) CreateTextMeshIfNeeded();
        textMesh.text = "1";
        textObject.SetActive(true);
    }
}
