using UnityEngine;
using UnityEngine.InputSystem;


public class NavigationMarkerController : MonoBehaviour
{
    public GameObject MyArrow;
    public Material InactiveMaterial;
    public Material GazedAtMaterial;

    private Vector3 arrowInitialPosition;
    public float amplitude;
    public float speed;

    private Renderer childRenderer;
    private bool isGazed = false;
    private TeleportationManager teleportationManager;


    private void Awake()
    {
        arrowInitialPosition = MyArrow.transform.position;
    }


    private void Start()
    {
        childRenderer = MyArrow.GetComponent<Renderer>();
        teleportationManager = FindObjectOfType<TeleportationManager>();
    }

    public void Update()
    {
        // Handle Controller inputs
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        // Enable the oscillation when looking at a marker
        if (isGazed)
        {
            ArrowOscillation();
        }
        else
        {
            ResetArrowPosition();
        }

        // Handle input
        if (isGazed && (gamepad.rightShoulder.isPressed || Input.GetKeyDown(KeyCode.F)))
        {
            teleportationManager.TeleportToMarker(gameObject);
        }
    }

    public void OnPointerEnter()
    {
        isGazed = true;
        SetMaterial(true);
    }

    public void OnPointerExit()
    {
        isGazed = false;
        SetMaterial(false);
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            childRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }

    private void ArrowOscillation()
    {
        float newY = arrowInitialPosition.y + amplitude * Mathf.Sin(speed * Time.time);
        MyArrow.transform.position = new Vector3(MyArrow.transform.position.x, newY, MyArrow.transform.position.z);
    }

    private void ResetArrowPosition()
    {
        MyArrow.transform.position = arrowInitialPosition;
    }
}
