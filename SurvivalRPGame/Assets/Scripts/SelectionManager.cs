using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public bool onTarget;
    public GameObject interaction_Info_UI;
    Text interaction_text;

    // Singleton-Instanz
    private static SelectionManager _instance;

    // Privater Konstruktor
    private SelectionManager() { }

    // Öffentliche statische Methode, um auf die Singleton-Instanz zuzugreifen
    public static SelectionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SelectionManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SelectionManager");
                    _instance = go.AddComponent<SelectionManager>();
                }
            }
            return _instance;
        }
    }

    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject ourInteractabe = (selectionTransform.GetComponent<InteractableObject>());

            if (ourInteractabe && ourInteractabe.playerInRange)
            {
                onTarget = true;
                interaction_text.text = ourInteractabe.GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else // if there is a hit, but with an Interactable Script
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
            }
        }
        else // if there is no hit at all
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }
}
