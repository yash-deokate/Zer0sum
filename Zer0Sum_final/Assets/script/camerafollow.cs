using UnityEngine;

public class camerafollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform target;
    [SerializeField] Vector3  offset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       follow();
    }
    void follow(){
        Vector3 targetpos=target.position+offset;
        Vector3 smoothpos=Vector3.Lerp(transform.position,targetpos,3f);
        transform.position=smoothpos;
    }
}
