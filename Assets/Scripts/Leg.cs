using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] legSkinPrefabs;

    private int skinIndex;

    private void Start()
    {
        skinIndex = PlayerPrefs.GetInt("chosenSkin", skinIndex);
        Instantiate(legSkinPrefabs[skinIndex], parent);
    }
}
