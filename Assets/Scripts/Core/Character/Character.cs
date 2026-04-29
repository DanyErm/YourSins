using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            if (value < 0)
                hp = 0;
            else
                hp = value;
        }
    }

    public float moveSpeed;


    public Vector3 GetPos()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}