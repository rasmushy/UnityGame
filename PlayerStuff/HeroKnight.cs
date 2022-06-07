using System;
using UnityEngine;
using UnityEngine.SceneManagement;
// @author rasmushy
public class HeroKnight : MonoBehaviour, IShopCustomer {



    [SerializeField] UI_Inventory uiInventory;
    [SerializeField] HealthBar healthBar; // HealthBar for player
    [SerializeField] Transform attackPoint; // Attack (where player deals damage)
    //****************PREMADE STARTS***************************************************************************
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    [SerializeField] private Transform wallCheck;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] private Sensor_HeroKnight m_groundSensor;
    [SerializeField] private Sensor_HeroKnight m_wallSensorR1;
    [SerializeField] private Sensor_HeroKnight m_wallSensorR2;
    [SerializeField] private Sensor_HeroKnight m_wallSensorL1;
    [SerializeField] private Sensor_HeroKnight m_wallSensorL2;
    private float m_speed; // this was [SerializeField] 
    private float m_jumpForce; // this was [SerializeField] 
    private bool m_noBlood; // this was [SerializeField] 
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    //****************PREMADE ENDS*****************************************************************************

    // ScriptableObject = Data holder
    public Data_Heroknight playerData; // see Data_Heroknight script to see all playerData
    // Player related
    private HealthSystem playerHealth; // playerMaxHealth is set from Data_Heroknight. 
    private AttackDetails attackDetails; // details for attack, look: Codes/Structs/c
    private bool isDead;
    private bool youWin;
    private bool isBlocking;
    private bool colliderCooldownRestart = false;
    private float colliderCooldown = 0.0f;
    private int damageDirection;
    // -------------------------------------------------------------------------------------

    //Inventory    
    private static Inventory inventory;
    public static HeroKnight Instance { get; private set; }

    //Gold to use
    public event EventHandler OnGoldAmountChanged;

    //Coins?
    public int Coins;

    //Last Boss mechanic
    public GameObject BookSpell;
    private bool bookUsed = false;
    
    //Light for darker missions
    public GameObject Light;
    bool isLit;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        // set our character data from our ScriptableObject
        m_speed = playerData.movementSpeed;
        m_jumpForce = playerData.jumpForce;
        m_noBlood = playerData.noBlood;
        playerData.goldAmount = 0;
        playerData.curLevel = 0; // set our current level to 0 this is only for testing levels
        playerData.attackDamage = 15f; // we decided that 10 is the starting value

        isDead = false; // we are not dead when we start heroknight
        youWin = false; // checks did we win.
        isBlocking = false; // or blocking so we can set both false

        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);
        DontDestroyOnLoad(gameObject);
        //Players HealthSystem & HealthBar & AttackPoint
        playerHealth = new HealthSystem(playerData.playerMaxHealth); // construct healthsystem
        healthBar.Setup(playerHealth); // add healthbar into the healthsystem
        attackPoint = transform.Find("AttackPoint").GetComponent<Transform>(); //create attackpoint to actually deal dmg
        // ***********PREMADE CODE*************************************************************
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        // ***********PREMADE CODE ENDS********************************************************
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void Update()
    {
        if (isDead)
        {
            gameObject.layer = 10; //layer 10 = dead
            Invoke("EndScreen", 3f);
        }
        else if (youWin)
        {
            SceneManager.LoadScene("Ending");
            youWin = false;
        }

        //AuraLight
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isLit)
            {
                CloseLight();
            }
            else
            {
                LitLight();
            }
        }
        //Check if spell is used and if spell is used, hides the magic barrier in VillageNight level
        if(bookUsed)
        {
            
            BookSpell = GameObject.FindGameObjectWithTag("SpellBlock");
            if(BookSpell != null)
            {
                BookSpell.SetActive(false);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.U)) // this cheatcode
        {
            playerData.goldAmount++;
        }

        //******** PREMADE CODE*************************************************  
        // Premade code are basically animations, but there is some logic added here and there

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // -------- ATTACKPOINT FLIPPING -------------------------------------------------------
        // Swap direction of sprite depending on walk direction
        if (inputX > 0 && !isDead)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            attackPoint.transform.localPosition = playerData.attackPointPosition_R; // attack point position will change depending which way we are looking at
        }
        else if (inputX < 0 && !isDead)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            attackPoint.transform.localPosition = playerData.attackPointPosition_L; // attack point position changed when facing left
        }
        // -------------------------------------------------------------------------------------

        // Move
        if (!isDead)
        {
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) && CheckWall() || (m_wallSensorL1.State() && m_wallSensorL2.State() && CheckWall());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        //Attack // !m_rolling deleted from if
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !isDead && playerData.curLevel > 1)  
        {
            Attack();
        }

        // Block //  && !m_rolling deleted from if
        else if (Input.GetMouseButtonDown(1) && !isDead) 
        {
            isBlocking = true;
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
            m_speed = 0f; // we dont move when blocking :3
        }

        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
            m_speed = playerData.movementSpeed; // get our movement speed back to normal
            isBlocking = false;
        }

         
        //Jump  !m_rolling deleted from if
        else if (Input.GetKeyDown("space") && m_grounded  && !isDead)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.8f);

        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }

        if (colliderCooldownRestart) //this is to prevent picking up 1 item multiple times
        {
            colliderCooldown += Time.deltaTime;
            if (colliderCooldown > 0.25f)
                colliderCooldownRestart = false;
        }
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    public void LitLight()
    {
        Light.SetActive(true);

        isLit = true;
    }
    public void CloseLight()
    {
        Light.SetActive(false);

        isLit = false;
    }

    private void OnTriggerEnter2D(Collider2D collider) //Basically ItemPickUp() method use this to add items from ground to your inventory
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null && colliderCooldown > 0.25f)
        {
            Debug.Log("im touching this item" + itemWorld.GetItem());
            // Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
        colliderCooldownRestart = true;
        colliderCooldown = 0.0f;
    }

    public void DropEntityGold(int goldDropAmount, bool wasItFinalBoss)
    {
        playerData.goldAmount += goldDropAmount;
        this.youWin = wasItFinalBoss;
    }

    public int GetGoldAmount()
    {
        return playerData.goldAmount;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //When player buys an item
    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought item" + itemType);
        switch (itemType)
        {
            case Item.ItemType.HealthPotion: AddHealthPotion(); break;
            case Item.ItemType.ManaPotion: AddManaPotion(); break;
            case Item.ItemType.DmgB: AddDamageBoost(); break;
            case Item.ItemType.Book: AddBook(); break;
        }

    }

    public void AddHealthPotion()
    {
        ItemWorld.SpawnItemWorld(new Vector3(16, 3, -3), new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });   
    }

    public void AddManaPotion()
    {
        ItemWorld.SpawnItemWorld(new Vector3(16, 1, -3), new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
    }
    public void AddDamageBoost()
    {
        ItemWorld.SpawnItemWorld(new Vector3(16, 1, -3), new Item { itemType = Item.ItemType.DmgB, amount = 1 });
    }
    public void AddBook()
    {
        ItemWorld.SpawnItemWorld(new Vector3(16, 1, -3), new Item { itemType = Item.ItemType.Book, amount = 1 });
    }
    //Tries to spend gold
    public bool TrySpendGoldAmount(int spendGoldAmount)
    {
        
        if (GetGoldAmount() >= spendGoldAmount)
        {
            
            playerData.goldAmount -= spendGoldAmount;
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }

    //Use Item And Remove it From Inventory
    private void UseItem(Item item)
    {
       
        switch (item.itemType)
        {

            case Item.ItemType.HealthPotion:
                playerHealth.Heal(300);
                Debug.Log("Health Potion Used");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.ManaPotion:
            
                Debug.Log("Mana Potion Used");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
                
            case Item.ItemType.Coin:
                playerData.goldAmount++;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Coin, amount = 1 });
                break;
            case Item.ItemType.DmgB:
                // Damage increase, four-leafed clover
                playerData.attackDamage += 10f;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.DmgB, amount = 1 });
                break;
            case Item.ItemType.Book:
                bookUsed = true;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Book, amount = 1 });              
                break;
        }
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnLevelWasLoaded()
    {
        transform.position = GameObject.FindWithTag("StartPosition").transform.position; // returns error currently when you make new game
        playerData.curLevel++;
        GetGoldAmount();
    }

    // Player hurt method
    public void TakeDamage(AttackDetails enemyAttackDetails)
    {
        //lets first figure out where that damage comes from
        if (attackDetails.position.x > gameObject.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        // if we are blocking and the damage direction is same then we dont take damage, simple.
        if (isBlocking && m_facingDirection == damageDirection)
        {
            Debug.Log("You've blocked");
        }
        else if (!isBlocking)
        {
            playerHealth.Damage(enemyAttackDetails.damageAmount);
            // check are we dead
            if (playerHealth.GetHealthPercent() <= 0.0f)
            {
                isDead = true; // because of this, we only play death animation once because we are not in Player layer afterwards
                m_animator.SetBool("noBlood", m_noBlood);
                m_animator.SetTrigger("Death");
            }
            else
            {
                m_animator.SetTrigger("Hurt"); // if we are not, we are hurt
            }
        }
    }


    // Player attacks and deals damage function
    private void Attack()
    {
        //play animation
        m_currentAttack++;

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerData.attackRange, playerData.enemyLayers);

        // add playerData to attackDetails struct that will be sent to the enemy
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = playerData.stunDamageAmount;
        attackDetails.damageAmount = playerData.attackDamage;
        if (m_currentAttack > 3) // every third attack you crit and deal twice the damage.
            attackDetails.damageAmount *= 2;

        // Damage Enemies
        foreach (Collider2D enemy in hitEnemies)
            enemy.transform.GetComponentInParent<Entity>().Damage(attackDetails);

        //*********PREMADE CODE STARTS*****************************************
        if (m_currentAttack > 3) // Loop back to one after third attack
            m_currentAttack = 1;
        if (m_timeSinceAttack > 1.0f) // Reset Attack combo if time since last attack is too large
            m_currentAttack = 1;
        m_animator.SetTrigger("Attack" + m_currentAttack); // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        m_timeSinceAttack = 0.0f; // Reset timer
        //*********PREMADE CODE ENDS*******************************************
    }

    public bool CheckWall() // We dont want to hit walls.
    {
        return Physics2D.Raycast(wallCheck.position, transform.right * m_facingDirection, 1f,
            playerData.whatIsWall);
    }

    // Some targeting gizmos for setting up attackpoint & wallcheck
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position + (Vector3)(Vector2.right * playerData.attackRange), playerData.attackRadius);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * m_facingDirection * 1f));
    }

    public void EndScreen()
    {
        SceneManager.LoadScene("MainMenuScene");
        Destroy(gameObject);
    }
}

// ******** USELESS CODE FOR ROLLING mechanic
//private float m_rollForce; // this was [SerializeField] 
//private bool                m_rolling = false;
//private float               m_rollDuration = 8.0f / 14.0f;
//private float               m_rollCurrentTime;
//m_rollForce = playerData.rollForce;
//Increase timer that checks roll duration
//if (m_rolling)
//m_rollCurrentTime += Time.deltaTime;
// Disable rolling if timer extends duration
//if (m_rollCurrentTime > m_rollDuration)
//m_rolling = false;
//if (!m_rolling && !isDead)
//Roll
//else if (Input.GetKeyDown(KeyCode.LeftShift) && !m_rolling && !isDead)
//{
//m_rolling = true;
//m_animator.SetTrigger("Roll");
//m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
//}