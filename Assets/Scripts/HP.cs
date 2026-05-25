using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour,IDamageable
{
    public float maxHP = 100f;
    public float currentHP;

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider ghostSlider;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;



    public float ghostSliderDuration = 1f;
    float timeTotal;
    float t;
    bool easeNow;

    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        timeTotal = 0;

        currentHP = maxHP;
        hpSlider.maxValue = maxHP;
        ghostSlider.maxValue = maxHP;

        hpSlider.value = maxHP;
        ghostSlider.value = maxHP;
    }

    void Update()
    {
        if(currentHP <= 0)
        {
            currentHP = 0;
        }
        
        hpSlider.value = currentHP;


        EaseHP();



    }


    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        easeNow = true;
        timeTotal = 0;

        Death();
        
    }

    void EaseHP()
    {
        
        if(easeNow)
        {
            t = timeTotal / ghostSliderDuration;

            if(t>1)
            {
                easeNow = false;
                return;
            }

            ghostSlider.value = Mathf.Lerp(ghostSlider.value, currentHP, easeOutCirc(t));

            timeTotal += Time.deltaTime;

        }
        
    }

    void Death()
    {
        if (currentHP <= 0)
        {
            Debug.Log("Player is Dead");
            loseScreen.SetActive(true);
        }
    }

    float easeOutCirc(float t)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
    }   

}

public interface IDamageable
{
    void TakeDamage(int damage);
}
