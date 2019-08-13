using UnityEngine;

public class FollowingObject : MonoBehaviour
{
    private Vector3 offset;   
    public Transform follow;   //the position of the player

    // Start is called before the first frame update
    void Start()
    {
        offset = follow.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position - offset;
    }
}
