using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timer;
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
             timer -= Time.deltaTime;
        }
        else if (timer <= 0)
        {
            timer = 0;
            
            timerText.color = Color.red;
        }
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
