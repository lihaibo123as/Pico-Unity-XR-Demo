using System.Collections;
using UnityEngine;

public class PrimaryReactor : MonoBehaviour
{
    public PrimaryButtonWatcher watcher;
    public bool IsPressed = false; // 用于在 Unity Inspector 窗口中显示按钮状态
    public Vector3 rotationAngle = new Vector3(45, 45, 45);
    public float rotationDuration = 0.25f; // 秒
    private Quaternion offRotation;
    private Quaternion onRotation;
    private Coroutine rotator;

    void Start()
    {
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
        offRotation = this.transform.rotation;
        onRotation = Quaternion.Euler(rotationAngle) * offRotation;
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        IsPressed = pressed;
        if (rotator != null)
            StopCoroutine(rotator);
        if (pressed)
            rotator = StartCoroutine(AnimateRotation(this.transform.rotation, onRotation));
        else
            rotator = StartCoroutine(AnimateRotation(this.transform.rotation, offRotation));
    }

    private IEnumerator AnimateRotation(Quaternion fromRotation, Quaternion toRotation)
    {
        float t = 0;
        while (t < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, t / rotationDuration);
            t += Time.deltaTime;
            yield return null;
        }
    }
}