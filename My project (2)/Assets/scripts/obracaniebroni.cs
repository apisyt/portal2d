using UnityEngine;

public class RotateGunTowardsMouse : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Jeœli grafika patrzy w prawo, nie trzeba dodawaæ offsetu
        // Jeœli patrzy np. w górê, dodaj +90f do angle
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
