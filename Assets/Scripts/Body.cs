using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] skinBodies;

    private int skinIndex;

    private void Start()
    {
        skinIndex = PlayerPrefs.GetInt("chosenSkin", skinIndex);
        Instantiate(skinBodies[skinIndex], parent);
    }
}
