using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowManager : MonoBehaviour
{
    [SerializeField] GameObject nokori;
    [SerializeField] GameObject time;
    [SerializeField] GameObject clear;
    [SerializeField] GameObject clear2;

    private int quantity = 0;
    private float DownTime = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString();
       
    }

    // Update is called once per frame
    void Update()
    {
        DownTime -= Time.deltaTime;
        time.GetComponent<TextMeshProUGUI>().text = DownTime.ToString();
    }

    public void Count()
    {
        if (quantity < 10)
        {
            quantity = quantity + 1;
            nokori.GetComponent<TextMeshProUGUI>().text = quantity.ToString();

            if (quantity == 9)
            {
                clear.SetActive(true);
                clear2.SetActive(true);
            }
        }
    }
}
