using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLog : MonoBehaviour
{
    public TMP_Text[] rows;
    private int currentIdx;

    public void write(string msg)
    {
        System.DateTime theTime = System.DateTime.Now;
        //string time = theTime.Hour + ":" + theTime.Minute + ":" + theTime.Second;
        string time = theTime.ToString("HH:mm:ss");

        for(int i  = this.rows.Length - 1; i > 0; i--)
        {
            this.rows[i].text = this.rows[i - 1].text;
        }

        this.rows[0].text = $"[LOG] {time}: {msg}";
    }

    // Start is called before the first frame update
    void Start()
    {
        if(this.rows.Length == 0)
        {
            print("No logs were found.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
