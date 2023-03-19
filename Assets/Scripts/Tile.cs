using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    private int row;
    private int column;
    private string value;

    public void OnPointerClick (PointerEventData eventData)
    {
        value = this.gameObject.GetComponent<InputField>().text;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.row = (int)char.GetNumericValue(this.gameObject.name[4]);
        this.column = (int)char.GetNumericValue(this.gameObject.name[5]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
