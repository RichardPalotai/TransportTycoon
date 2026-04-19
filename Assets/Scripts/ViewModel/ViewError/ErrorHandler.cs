using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorHandler : MonoBehaviour
{
    public static ErrorHandler instance;
    
    #region Private variables
    [SerializeField]
    private Button Close_btn;
    [SerializeField]
    private TMP_Text ErrorTag_txt;
    [SerializeField]
    private TMP_Text Error_txt;
    #endregion

    #region Properties
    public string ErrorTag{
        get { return ErrorTag_txt.text; }
        set
        {
            ErrorTag_txt.text = value;
        }
    }
    public string ErrorText{
        get { return Error_txt.text; }
        set
        {
            Error_txt.text = value;
        }
    }
    #endregion

    #region Unity calls
    void Awake()
    {
        instance = this;
        ErrorTag = "Error";
        ErrorText = "Some error message";
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
    #endregion

    #region Private methods
    private void OnCloseClicked()
    {
        ErrorTag = "Error";
        ErrorText = "Some error message";
        gameObject.SetActive(false);
    }
    #endregion

    #region Public methods
    public void DisplayError(string tag, string msg)
    {
        ErrorTag = tag;
        ErrorText = msg;
        gameObject.SetActive(true);
    }
    #endregion
}
