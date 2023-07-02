using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Carrot : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Destroy(gameObject);
        }

    }

}
