using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IPointerClickHandler
{
    private string value;
    private bool clickable;

    public bool Clickable { get => clickable; set => clickable = value; }

    public void OnPointerClick (PointerEventData eventData)
    {
        if(!Clickable) return;
        
        // Highlight
        InputField input = this.gameObject.GetComponent<InputField>();
        input.image.color = Color.green;

        // Notify GC
        GameObject.Find("GameCoordinator").GetComponent<GameCoordinator>().letterClicked(value);
    }

    private void OnKeyEvent()
    {
        if(!Clickable) return;
        
        // Highlight
        InputField input = this.gameObject.GetComponent<InputField>();
        input.image.color = Color.green;

        // Notify GC
        GameObject.Find("GameCoordinator").GetComponent<GameCoordinator>().letterClicked(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.Clickable = false;
    }

    // Update is called once per frame
    void Update()
    {
        InputField input = this.gameObject.GetComponent<InputField>();
        value = input.text;

        if(value != "" && value != "?")
        {
            KeyCode kc;

            if(value == "Å")
            {
                kc = KeyCode.RightBracket;
            }
            else if(value == "Ä")
            {
                kc = KeyCode.Quote;
            }
            else if(value == "Ö")
            {
                kc = KeyCode.BackQuote;
            }
            else
            {
                kc = (KeyCode)System.Enum.Parse(typeof(KeyCode), value);
            }

            if(Input.GetKeyDown(kc))
            {
                OnKeyEvent();
            }
        }
    }
}

