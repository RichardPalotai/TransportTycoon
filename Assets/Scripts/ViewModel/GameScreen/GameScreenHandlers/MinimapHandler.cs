using UnityEngine;

public class MinimapHandler : MonoBehaviour
{
    [SerializeField]
    private Canvas PlayerLocationIcon_cnv;

    void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AdjustPlayerPositionIcon();
    }

    private void AdjustPlayerPositionIcon()
    {
        Quaternion rotation = Quaternion.Euler(90, Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z); ;
        PlayerLocationIcon_cnv.transform.LookAt(PlayerLocationIcon_cnv.transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}
