using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IPointerClickHandler
{
    private int index;
    private string value;
    private bool clickable;

    public bool Clickable { get => clickable; set => clickable = value; }

    public void OnPointerClick (PointerEventData eventData)
    {
        if(!Clickable) return;
        
        // Get value from input field and highlight
        InputField input = this.gameObject.GetComponent<InputField>();
        value = input.text;
        input.image.color = Color.green;

        // Notify GC
        GameObject.Find("GameCoordinator").GetComponent<GameCoordinator>().letterClicked(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.index = (int)char.GetNumericValue(this.gameObject.name[6]);
        this.Clickable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

