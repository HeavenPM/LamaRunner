using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsBonuses : MonoBehaviour
{
    [SerializeField] Player player;

    private int skinIndex;

    public float ShieldSkinBoost = 1;
    public float DoubleCarrotSkinBoost = 1;
    public float JetpackSkinBoost = 1;
    public float JumpForceBoost = 7;

    public int CarrotFactor = 1;

    public bool IsExtraLive = false;

    private void Awake()
    {
        skinIndex = PlayerPrefs.GetInt("chosenSkin", skinIndex);

        //Alchemist
        if (skinIndex == 8)
        {
            CarrotFactor = 2; 
        }
        //Viking
        if (skinIndex == 11)
        {
            CarrotFactor = 2;
        }
        //Don
        if (skinIndex == 14)
        {
            CarrotFactor = 3;
        }

    }

    private void Start()
    {
        //Default
        if (skinIndex == 0)
        {
            JumpForceBoost = 7;
        }

        //Pilot
        if (skinIndex == 1)
        {
            JetpackSkinBoost = 1.5f;
            JumpForceBoost = 7;
        }

        //Knight
        else if (skinIndex == 2)
        {
            ShieldSkinBoost = 1.5f;
            JumpForceBoost = 7;
        }

        //Farmer
        else if (skinIndex == 3)
        {
            DoubleCarrotSkinBoost = 1.5f;
            JumpForceBoost = 7;
        }

        //Cool
        else if (skinIndex == 4)
        {
            ShieldSkinBoost = 1.3f;
            DoubleCarrotSkinBoost = 1.3f;
            JetpackSkinBoost = 1.3f;
            JumpForceBoost = 7;
        }

        //Pegasus
        else if (skinIndex == 5)
        {
            JumpForceBoost = 8;
            JetpackSkinBoost = 2;
        }

        //Astronaut
        else if (skinIndex == 6)
        {
            ShieldSkinBoost = 2;
            JetpackSkinBoost = 2;
            JumpForceBoost = 7;
        }

        //Santa
        else if (skinIndex == 7)
        {
            DoubleCarrotSkinBoost = 2;
            JetpackSkinBoost = 2;
            JumpForceBoost = 7;
        }

        //Alchemist
        else if (skinIndex == 8)
        {
            CarrotFactor = 2;
            JumpForceBoost = 7;
        }

        //King
        else if (skinIndex == 9)
        {
            ShieldSkinBoost = 2;
            DoubleCarrotSkinBoost = 2;
            JumpForceBoost = 7;
        }

        //Ninja
        else if (skinIndex == 10)
        {
            JumpForceBoost = 8;
            ShieldSkinBoost = 2;
            DoubleCarrotSkinBoost = 2;
            JetpackSkinBoost = 2;
        }

        //Viking
        else if (skinIndex == 11)
        {
            CarrotFactor = 2;
            ShieldSkinBoost = 2.5f;
            JumpForceBoost = 7;
        }

        //Caesar
        else if (skinIndex == 12)
        {
            ShieldSkinBoost = 2.5f;
            DoubleCarrotSkinBoost = 2.5f;
            JetpackSkinBoost = 2.5f;
            JumpForceBoost = 7;
        }

        //Unicorn
        else if (skinIndex == 13)
        {
            IsExtraLive = true;
            JumpForceBoost = 7;
        }

        //Don
        else if (skinIndex == 14)
        {
            CarrotFactor = 3;
            DoubleCarrotSkinBoost = 3;
            JumpForceBoost = 7;
        }
    }
}
