using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkinChanger : MonoBehaviour
{
    public Skin[] Info;
    private bool[] StockCheck;

    public Button BuyButton;
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI CarrotsText;
    public Transform Player;
    public int Index;

    public int Carrots;

    public bool IsShopActive = false;
    public AudioSource AudioSource;

    private void Awake()
    {
        Carrots = PlayerPrefs.GetInt("TotalCarrots");
        Index = PlayerPrefs.GetInt("chosenSkin");
        CarrotsText.text = Carrots.ToString();

        StockCheck = new bool[15];
        if (PlayerPrefs.HasKey("StockArray"))
            StockCheck = PlayerPrefsX.GetBoolArray("StockArray");

        else
            StockCheck[0] = true;

        Info[Index].isChosen = true;

        for (int i = 0; i < Info.Length; i++)
        {
            Info[i].inStock = StockCheck[i];
            if (i == Index)
                Player.GetChild(i).gameObject.SetActive(true);
            else
                Player.GetChild(i).gameObject.SetActive(false);
        }

        PriceText.text = "CHOSEN";
        BuyButton.interactable = false;

        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            IsShopActive = true;
            
        }
    }

    public void Save()
    {
        PlayerPrefsX.SetBoolArray("StockArray", StockCheck);
    }

    public void ScrollRight()
    {
        AudioSource.Play();
        if (Index < Player.childCount - 1)
        {
            Index++;

            if (Info[Index].inStock && Info[Index].isChosen)
            {
                PriceText.text = "CHOSEN";
                BuyButton.interactable = false;
            }
            else if (!Info[Index].inStock)
            {
                PriceText.text = Info[Index].cost.ToString();
                BuyButton.interactable = true;
            }
            else if (Info[Index].inStock && !Info[Index].isChosen)
            {
                PriceText.text = "CHOOSE";
                BuyButton.interactable = true;
            }

            for (int i = 0; i < Player.childCount; i++)
                Player.GetChild(i).gameObject.SetActive(false);
            //player.GetChild(index-1).gameObject.SetActive(false);

            Player.GetChild(Index).gameObject.SetActive(true);
        }
    }

    public void ScrollLeft()
    {
        AudioSource.Play();
        if (Index > 0)
        {
            Index--;

            if (Info[Index].inStock && Info[Index].isChosen)
            {
                PriceText.text = "CHOSEN";
                BuyButton.interactable = false;
            }
            else if (!Info[Index].inStock)
            {
                PriceText.text = Info[Index].cost.ToString();
                BuyButton.interactable = true;
            }
            else if (Info[Index].inStock && !Info[Index].isChosen)
            {
                PriceText.text = "CHOOSE";
                BuyButton.interactable = true;
            }

            for (int i = 0; i < Player.childCount; i++)
                Player.GetChild(i).gameObject.SetActive(false);

            Player.GetChild(Index).gameObject.SetActive(true);
        }
    }

    public void BuyButtonAction()
    {
        if (BuyButton.interactable && !Info[Index].inStock)
        {
            if (Carrots > int.Parse(PriceText.text))
            {
                AudioSource.Play();
                Carrots -= int.Parse(PriceText.text);
                CarrotsText.text = Carrots.ToString();
                PlayerPrefs.SetInt("TotalCarrots", Carrots);
                StockCheck[Index] = true;
                Info[Index].inStock = true;
                PriceText.text = "CHOOSE";
                Save();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (BuyButton.interactable && !Info[Index].isChosen && Info[Index].inStock)
        {
            AudioSource.Play();
            PlayerPrefs.SetInt("chosenSkin", Index);
            BuyButton.interactable = false;
            PriceText.text = "CHOSEN";

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}


[System.Serializable]
public class Skin
{
    public int cost;
    public bool inStock;
    public bool isChosen;
}