using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    public GameObject shopUI;
    public TextMeshProUGUI totalApples;
    public TextMeshProUGUI currentAccessoriesName;
    public AccessoriesLogic accessoriesLogic;

    public GameObject accessoriesPrefab;
    public Transform accessoriesContainer;
    public Accessories[] accessories;
    private bool isInit = false;

    
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);
        accessories = Resources.LoadAll<Accessories>("Accessories/");
        shopUI.SetActive(true);

        if (!isInit)
        {
            totalApples.text = SaveManager.Instance.save.Apples.ToString("0000");
            currentAccessoriesName.text = accessories[SaveManager.Instance.save.CurrentAccessoriesIndex].ItemName;
            PopulateTheShop();
            isInit = true;
        }

        
    }

    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    private void PopulateTheShop()
    {
        
        for(int i = 0; i < accessories.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(accessoriesPrefab, accessoriesContainer) as GameObject;

            go.GetComponent<Button>().onClick.AddListener(() => OnAccessoriesClick(index));

            go.transform.GetChild(0).GetComponent<Image>().sprite = accessories[index].Thumbnail;

            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = accessories[index].ItemName;

            if (SaveManager.Instance.save.UnlockAccessoriesFlag[i] == 0)
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = accessories[index].ItemPrice.ToString();
            else
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    private void OnAccessoriesClick(int i)
    {
        if(SaveManager.Instance.save.UnlockAccessoriesFlag[i] ==1)
        {
            SaveManager.Instance.save.CurrentAccessoriesIndex = i;
            currentAccessoriesName.text = accessories[i].ItemName;
            accessoriesLogic.SelectAccessories(i);
            SaveManager.Instance.Save();
        }

        else if(accessories[i].ItemPrice <= SaveManager.Instance.save.Apples)
        {
            SaveManager.Instance.save.Apples -= accessories[i].ItemPrice;
            SaveManager.Instance.save.UnlockAccessoriesFlag[i] = 1;
            SaveManager.Instance.save.CurrentAccessoriesIndex = i;
            currentAccessoriesName.text = accessories[i].ItemName;
            accessoriesLogic.SelectAccessories(i);
            totalApples.text = SaveManager.Instance.save.Apples.ToString("0000");
            SaveManager.Instance.Save();
            accessoriesContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            Debug.Log("not enough apples");
        }
        
    }

    public void OnHomepClick(int i)
    {
        Debug.Log("clicked");

    }
}
