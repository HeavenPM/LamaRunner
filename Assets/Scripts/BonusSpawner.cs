using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Bonus[] bonusTemplates;
    

    private int bonusGeneratePercent;

    private void Start()
    {
        bonusGeneratePercent = Random.Range(0, 9);
        if(bonusGeneratePercent <= 1)
        {
            Instantiate(bonusTemplates[Random.Range(0, bonusTemplates.Length)], transform);
        }
    }
}
