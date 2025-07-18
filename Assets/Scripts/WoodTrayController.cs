using System.Collections.Generic;
using UnityEngine;

public class WoodTrayController : MonoBehaviour
{
    public List<string> ingredients = new List<string>();

    public void AddIngredient(string ingredientName)
    {
        ingredients.Add(ingredientName);
    }

    public void ClearTray()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Ingredient");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
    
    public bool ContainsNori()
    {
        return ingredients.Contains("nori");
    }

    public string GetFishType()
    {
        foreach (var ing in ingredients)
        {
            if (!string.IsNullOrEmpty(ing))
            {
                return ing;
            }
        }

        return "salmon";
    }
}
