using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AccessoriesLogic : MonoBehaviour
{
    [SerializeField] private Transform accessoriesContainer;
    private List<GameObject>accessoriesModels = new List<GameObject>();
    private Accessories[] accessories;

    private void Start()
    {
        accessories = Resources.LoadAll<Accessories>("Accessories");
        Spawnaccessories();
        SelectAccessories(SaveManager.Instance.save.CurrentAccessoriesIndex);
    }

    private void Spawnaccessories()
    {
        for (int i = 0; i < accessories.Length; i++)
        {
            int index = i;
            accessoriesModels.Add(Instantiate(accessories[index].Model, accessoriesContainer));
        }
        DisableAllAccessories();

    }

    public void DisableAllAccessories()
    {
        for (int i = 0; i < accessoriesModels.Count; i++)
        {
            accessoriesModels[i].SetActive(false);
        }
    }

    public void SelectAccessories(int index)
    {
        DisableAllAccessories();
        accessoriesModels[index].SetActive(true);
    }
} 
