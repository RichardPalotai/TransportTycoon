using UnityEngine;
using UnityEngine.UI;

public class VehicleRouteHandler : MonoBehaviour
{
    [SerializeField]
    private Button Cancel_btn;
    [SerializeField]
    private Button Reset_btn;
    [SerializeField]
    private Button Ok_btn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cancel_btn.onClick.AddListener(OnCancelClicked);
        Reset_btn.onClick.AddListener(OnResetClicked);
        Ok_btn.onClick.AddListener(OnOkClicked);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCancelClicked()
    {
        GameViewModel.instance.SetRouteDisplayActive(false);
    }

    private void OnResetClicked()
    {
        
    }

    private void OnOkClicked()
    {
        
    }
}
