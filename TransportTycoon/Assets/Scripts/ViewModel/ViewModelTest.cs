using UnityEngine;

public class ViewModelTest : MonoBehaviour
{
    void Awake()
    {
        Game g = new Game();
        Game.instance = g;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Game.instance.NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        Game.instance.UpdateGame(Time.deltaTime);
    }
}
