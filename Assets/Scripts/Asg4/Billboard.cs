using UnityEngine;

/// <summary>
/// This points this transform too the main camera
/// </summary>
public class Billboard : MonoBehaviour
{
    public bool yBillboard = false;
    public bool invert = false;
    void LateUpdate()
    {
        Vector3 delta = transform.position - Camera.main.transform.position;
        if (yBillboard) delta.y = 0;
        if (invert) delta *= -1;
        delta.Normalize();
        transform.rotation.SetLookRotation(delta);
    }
}
