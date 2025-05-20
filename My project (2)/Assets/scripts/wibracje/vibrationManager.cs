using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Vibrate(float left = 0.5f, float right = 0.5f, float duration = 0.2f)
    {
        var pad = Gamepad.current;
        if (pad != null)
        {
            pad.SetMotorSpeeds(left, right);
            CancelInvoke(nameof(StopVibration));
            Invoke(nameof(StopVibration), duration);
        }
    }

    private void StopVibration()
    {
        Gamepad.current?.SetMotorSpeeds(0f, 0f);
    }
}
