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
    private bool clickable;
    private int playerIdx;

    public bool Clickable { get => clickable; set => clickable = value; }
    public string Value { get => value; set => this.value = value; }
    public int PlayerIdx { get => playerIdx; set => playerIdx = value; }

    public void OnPointerClick (PointerEventData eventData)
    {
        if(!Clickable) return;

        GameObject.Find("GameCoordinator").GetComponent<GameCoordinator>().tileClicked(0, row, column);
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
