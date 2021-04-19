using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    [SerializeField] int currentCharacter = 0;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCharacter(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCharacter(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeCharacter(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeCharacter(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeCharacter(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeCharacter(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeCharacter(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeCharacter(7);
        }


    }

    void ChangeCharacter(int character)
    {
        if (characters[character] != null)
        {
            characters[currentCharacter].SetActive(false);
            characters[character].SetActive(true);
            currentCharacter = character;
        }
    }
}
