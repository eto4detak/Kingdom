using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProviderControlePoint : MonoBehaviour
{
    public GameObject prefabControlePoint;
    public Unit owner;
    private float time;
    private float newPointTime = 0.3f;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > newPointTime)
        {
            time = 0;
            Collider[] radiusColliders = Physics.OverlapSphere(transform.position, 3f);
            for (int i = 0; i < radiusColliders.Length; i++)
            {
                if(radiusColliders[i].gameObject.layer == 0)
                {
                    ControlePoint temp = radiusColliders[i].GetComponent<ControlePoint>();
                }
            }
        }
    }

    public void SetPoint()
    {
        Vector3 pointPosition = transform.position;
        pointPosition.y = 0;
        Instantiate(prefabControlePoint, pointPosition, Quaternion.identity);
    }

}
