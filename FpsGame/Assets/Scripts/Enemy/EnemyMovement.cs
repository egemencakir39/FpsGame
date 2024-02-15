using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private Transform target;

    [Header("Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float DetectionRange = 10f;
    [SerializeField] private float StopDistance = 3f;
    [SerializeField] private float RotationSpeed = 3f;

    private void FixedUpdate()
    {
        movement();
    }
    void movement() // hareket
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position); // enemy ile hedef mesafesini hesaplama
        if (distanceToPlayer <= DetectionRange && distanceToPlayer > StopDistance) // mesafe koþullarý
        {
            Vector3 targetPos = new Vector3(target.position.x, 0f, target.position.z); // sadece x ve z de takip edip y de 0 yapma
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime); // enemy hedefine doðru hareket
           // enemy hedefine yüzünü dönmesi
            Vector3 direction = targetPos - transform.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, RotationSpeed * Time.deltaTime);
        }

    }
}
