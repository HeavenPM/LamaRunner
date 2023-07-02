using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Description : MonoBehaviour
{
    [SerializeField] private SkinChanger skinChanger;

    private Vector3 shopPos;

    private void Awake()
    {
        shopPos = transform.position;
    }
    private void Update()
    {
        if (skinChanger.IsShopActive == true)
        {
            transform.position = shopPos;
        }
        else if (skinChanger.IsShopActive == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
