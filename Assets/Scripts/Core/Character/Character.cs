using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3 GetPos()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}