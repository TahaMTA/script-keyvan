using UnityEngine;

public class camera : MonoBehaviour
{
    private GameObject Target;
    [SerializeField] private float speed;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (Target == null)
            return;
        transform.position = Vector3.Lerp(transform.position, new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
