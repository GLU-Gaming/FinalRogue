using UnityEngine;
using UnityEngine.InputSystem;

public class CursorAim : MonoBehaviour
{
    public Transform attackPoint;         // The attack point that will rotate
    [SerializeField] private float attackPointRadius = 1f;  // Radius of the circle for the attack point
    private Camera mainCamera;            // Reference to the main camera
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private bool facingRight = true;      // Tracks if the character is facing right

    private void Start()
    {
        mainCamera = Camera.main; // Get the main camera
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer on the same GameObject

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found! Please ensure your Player GameObject has a SpriteRenderer component.");
        }
    }

    private void Update()
    {
        // Get the mouse position in world space
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPosition.z = 0f; // Set Z to 0 since we're in 2D

        // Calculate the direction from the player to the mouse
        Vector3 direction = (mouseWorldPosition - transform.position).normalized;

        // Constrain the direction to the attack point circle
        Vector3 offset = direction * attackPointRadius;
        attackPoint.position = transform.position + offset;

        // Calculate the angle for rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackPoint.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip the character based on mouse position relative to the screen center
        FlipCharacter(mouseWorldPosition);
    }

    // Handles flipping the character based on mouse position
    private void FlipCharacter(Vector3 mouseWorldPosition)
    {
        // Flip the sprite using SpriteRenderer.flipX
        if (mouseWorldPosition.x < transform.position.x && facingRight)
        {
            facingRight = false;
            spriteRenderer.flipX = true;
        }
        else if (mouseWorldPosition.x > transform.position.x && !facingRight)
        {
            facingRight = true;
            spriteRenderer.flipX = false;
        }
    }

    // Draw a visualization of the attack point's circular path in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackPointRadius);
    }
}
