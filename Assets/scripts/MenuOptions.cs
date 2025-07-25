using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public enum OptionType { Restart, Quit }
    [SerializeField] private OptionType option;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{name} was touched by {other.name} (tag = {other.tag})");

        fruitSpawner.Instance.ClearMenuOptions();

        if (!other.CompareTag("Knife") && !other.transform.root.CompareTag("Knife"))
        {
            return;
        }

        if (option == OptionType.Restart)
        {
            fruitSpawner.Instance.BeginRound();
        }
            
        else
        {
            fruitSpawner.Instance.QuitGame();
        }
            
    }
}
