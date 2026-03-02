using UnityEngine;

public class FollowBackgroundCenter : MonoBehaviour
{
    public Transform background;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (background != null)
        {
            var pos = background.position;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
