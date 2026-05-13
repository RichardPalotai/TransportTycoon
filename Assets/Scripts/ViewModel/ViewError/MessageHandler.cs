using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageHandler : MonoBehaviour
{
    public static MessageHandler instance;
    
    #region Private variables
    [SerializeField]
    private Button Close_btn;
    [SerializeField]
    private TMP_Text MessageTag_txt;
    [SerializeField]
    private TMP_Text Message_txt;
    #endregion

    #region Properties
    public string MessageTag{
        get { return MessageTag_txt.text; }
        set
        {
            MessageTag_txt.text = value;
        }
    }
    public string MessageText{
        get { return Message_txt.text; }
        set
        {
            Message_txt.text = value;
        }
    }
    #endregion

    #region Unity calls
    void Awake()
    {
        instance = this;
        MessageTag = "Error";
        MessageText = "Some error message";
        MessageTag_txt.color = Color.red;
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
        MessageTag = "Error";
        MessageText = "Some error message";
        MessageTag_txt.color = Color.red;
        gameObject.SetActive(false);
    }
    #endregion

    #region Public methods
    public void DisplayError(string tag, string msg)
    {
        MessageTag = tag;
        MessageText = msg;
        MessageTag_txt.color = Color.red;
        gameObject.SetActive(true);
    }
    public void DisplaySuccessMessage(string tag, string msg)
    {
        MessageTag = tag;
        MessageText = msg;
        MessageTag_txt.color = Color.green;
        gameObject.SetActive(true);
    }

    public void DisplayMessage(string tag, string msg)
    {
        MessageTag = tag;
        MessageText = msg;
        MessageTag_txt.color = Color.blue;
        gameObject.SetActive(true);
    }
    #endregion
}
