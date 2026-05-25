using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int FPS = 60;
    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    
}
