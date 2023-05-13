using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] Transform targetPos;
    [SerializeField] Transform lookPos;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookPos);
    }
    private void FixedUpdate()
    {
        Vector3 pos = targetPos.position;
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
