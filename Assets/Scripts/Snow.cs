using UnityEngine;

public class Snow : MonoBehaviour
{
    public GameObject snow;

    private void Awake()
    {
        snow.SetActive(false);
    }

    public void StartSnow()
    {
        snow.SetActive(true);
        Animation animation = snow.GetComponent<Animation>();

        animation.Play("Snow");
    }

    public void EndSnow()
    {
        snow.SetActive(false);
    }
}