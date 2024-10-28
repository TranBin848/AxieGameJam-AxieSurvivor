using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    public AudioSource levelUpSound;
    //Current stats
    
    public float currentHealth;    
    float currentRecovery;    
    float currentMoveSpeed;    
    float currentMight;    
    float currentProjectileSpeed;   
    float currentMagnet;
    public AudioSource collectSound;

    #region Current Stats Properties

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if(currentHealth != value)
            {
                currentHealth = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
                }
            }
        }

    }

    public float CurrentRecovery 
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value) 
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }

        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if(currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
                
             
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }

            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if(currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }

    #endregion 




    //Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;
    

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrese;
    }

    //I-Frames
    [Header("I_Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvicible;


    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMP_Text levelText;

    
    public GameObject firstPassiveItemtest, secondPassiveItemtest;
    

   
    void Awake()
    {
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        

        inventory = GetComponent<InventoryManager>();

        SpawnWeapon(characterData.StartingWeapon);
        
       
        
        
        
        

    }


    void Start () 
    {
        experienceCap = levelRanges[0].experienceCapIncrese;

        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "MoveSpeed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "ProjectileSpeed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
        GameManager.instance.AssignChosenCharacterUI(characterData);
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();

    }

    void Update ()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvicible)
        {
            isInvicible = false;
        }
        Recover();
    }
    public void IncreaseExperience(int amount)
    {
        collectSound.Play();
        experience += amount;
        LevelUpChecker();

        UpdateExpBar();
    }
    void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            levelUpSound.Play();

            int experienceCapIncrease = 0; 
            foreach(LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrese;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;

            UpdateLevelText();

            GameManager.instance.StartLevelUp();
        }

    }

    void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;

    }

    void UpdateLevelText()
    {
        levelText.text = "LV" + level.ToString();
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvicible)
        {
            CurrentHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvicible = true;

            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    public void Kill()
    {
        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.GameOver();
        }
    }

    public void RestoreHealth(float amount)
    {
        if(CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;

            if(CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }

    }
    void Recover()
    {
        if(CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            if(CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }    
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if(weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
        
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;

    }





}
