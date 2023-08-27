using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myCharacter;
    private CharacterState myCharacterState;
    public Slider uiHealth;
    private int maxHealth;
    private int currentHealth;
    private Slider characterHealth;
    public TMP_Text maxHealthText;
    public TMP_Text currentHealthText;

    public GameObject Boss;
    public Slider uiBossHealth;
    public Slider bossHealth;
    private int bossMaxHealth;
    private int bossCurrentHealth;
    public TMP_Text bossMaxHealthText;
    public TMP_Text bossCurrentHealthText;
    private bool bossConnected;
    public CharacterState bossState;

    public GameObject BossStateUI;
    public void setUp()
    {
        myCharacterState = myCharacter.GetComponentInChildren<CharacterState>(); 
        characterHealth = myCharacterState.health;
        uiHealth.maxValue = characterHealth.maxValue;

        
        StartCoroutine(update_health());
    }

    public void BossSetUp()
    {
        Boss = GameObject.Find("Enemy Group").transform.GetChild(0).gameObject;
        bossState = Boss.GetComponentInChildren<CharacterState>();
        bossHealth = bossState.health;
        uiBossHealth.maxValue = bossHealth.maxValue;
        bossConnected = true;
        BossStateUI.SetActive(true);
    }
    
    IEnumerator update_health()
    {
        while (true)
        {
            uiHealth.value = characterHealth.value;
            currentHealth = (int)characterHealth.value;
            maxHealth = (int)characterHealth.maxValue;
            maxHealthText.text = maxHealth.ToString();
            currentHealthText.text = currentHealth.ToString();

            if (bossConnected)
            {
                uiBossHealth.value = bossHealth.value;
                bossCurrentHealth = (int)bossHealth.value;
                bossMaxHealth = (int)bossHealth.maxValue;
                bossMaxHealthText.text = bossMaxHealth.ToString();
                bossCurrentHealthText.text = bossCurrentHealth.ToString();
                
            }
            yield return null;


        }
    }
}

