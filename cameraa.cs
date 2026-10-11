using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject Target;
    [SerializeField] private float speed = 5f;
    private Vector3 offset; // فاصله دوربین از بازیکن

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        if (Target != null)
        {
            // محاسبه offset اولیه
            offset = transform.position - Target.transform.position;
        }
    }

    void Update()
    {
       
    }
    void LateUpdate()
    {
        if (Target == null) return;

        Vector3 targetPosition = Target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}