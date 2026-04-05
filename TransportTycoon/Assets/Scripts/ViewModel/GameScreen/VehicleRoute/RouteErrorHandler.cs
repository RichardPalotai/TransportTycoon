using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouteErrorHandler : MonoBehaviour
{
    public static RouteErrorHandler instance;

    [SerializeField]
    private Button Close_btn;
    [SerializeField]
    private TMP_Text Error_txt;

    public string ErrorText{
        get { return Error_txt.text; }
        set
        {
            Error_txt.text = value;
        }
    }

    void Awake()
    {
        instance = this;

        gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Close_btn.onClick.AddListener(OnCloseClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCloseClicked()
    {
        ErrorText = "Some error message";
        gameObject.SetActive(false);
    }

    public void DisplayError(string msg)
    {
        ErrorText = msg;
        gameObject.SetActive(true);
    }
}
