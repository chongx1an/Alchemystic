using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionController : MonoBehaviour
{
    /*
     * Player related things
     */
    public static GameObject player;
    private static PlayerController playerController;
    private bool fireReady;
    private bool iceReady;
    private bool spaceReady;

    /*
     * Potion Prefab
     */
    public GameObject firePotionPrefab;
    public GameObject icePotionPrefab;
    public GameObject spacePotionPrefab;
    private GameObject potion;
    public float fireCD;
    public float iceCD;
    public float spaceCD;

    /*
     * Throwing parameters
     */
    private Vector2 mouseOriginPoint;
    private Vector2 mouseDiff;
    private Vector2 force;
    public Vector2 power;
    public float angle;
    private bool isAiming;
    public float maxForce = 10.0f;
    /*
     * Trajectory things
     */
    private List<GameObject> trajectoryPointsList;
    private const int NUM_OF_POINTS = 30;
    public GameObject pointPrefab;

    /*
     * CD Cover
     */
    public Image fireCD_Cover;
    public Image iceCD_Cover;
    public Image spaceCD_Cover;
    // Start is called before the first frame update
    void Start()
    {
        fireReady = true;
        iceReady = true;
        spaceReady = true;
        if (!player)
        {
            player = GameObject.Find("Player");
        }
        playerController = player.GetComponent<PlayerController>();
        isAiming = false;
        InitializeTrajectoryPoints();
    }

    // Update is called once per frame
    void Update()
    {
        CreatePotionWhenInput();
        AimPotion();
        ThrowPotionWhenRelease();
        
    }
    private void FixedUpdate()
    {
       
        if (!fireReady)
        {
            fireCD_Cover.enabled = true;
            float count = 0;
            if (fireCD_Cover.fillAmount > count)
            {
                fireCD_Cover.fillAmount -= Time.deltaTime / fireCD;
            }
            else
            {
                fireCD_Cover.fillAmount = 1;
            }
        }
        else
        {
            fireCD_Cover.enabled = false;
        }
        
        if (!iceReady)
        {
            iceCD_Cover.enabled = true;
            float count = 0;
            if (iceCD_Cover.fillAmount > count)
            {
                iceCD_Cover.fillAmount -= Time.deltaTime / iceCD;
            }
            else
            {
                iceCD_Cover.fillAmount = 1;
            }
        }
        else
        {
            iceCD_Cover.enabled = false;
        }
        
        if (!spaceReady)
        {
            spaceCD_Cover.enabled = true;
            float count = 0;
            if (spaceCD_Cover.fillAmount > count)
            {
                spaceCD_Cover.fillAmount -= Time.deltaTime / spaceCD;
            }
            else
            {
                spaceCD_Cover.fillAmount = 1;
            }
        }
        else
        {
            spaceCD_Cover.enabled = false;
        }
    }
    private void InitializeTrajectoryPoints()
    {
        trajectoryPointsList = new List<GameObject>();

        for (int i = 0; i < NUM_OF_POINTS; i++)
        {
            GameObject point = Instantiate(pointPrefab);
            point.SetActive(false);
            trajectoryPointsList.Add(point);
        }
    }
    private void CreatePotionWhenInput()
    {


        if (Input.GetMouseButtonDown(0))
        {
            if ((playerController.potionMode == PlayerController.PotionMode.Fire && fireReady)||
                (playerController.potionMode == PlayerController.PotionMode.Ice && iceReady)||
                (playerController.potionMode == PlayerController.PotionMode.Space && spaceReady))
            {
                mouseOriginPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                CreatePotion(playerController.potionMode);
                potion.GetComponent<Rigidbody2D>().gravityScale = 0;
                isAiming = true;
            }
            
        }
    }
    private void CreatePotion(PlayerController.PotionMode aPotionMode)
    {
        if (aPotionMode == PlayerController.PotionMode.Fire)
            potion = Instantiate(firePotionPrefab, transform.position, transform.rotation);
        else if (aPotionMode == PlayerController.PotionMode.Ice)
            potion = Instantiate(icePotionPrefab, transform.position, transform.rotation);
        else if (aPotionMode == PlayerController.PotionMode.Space)
            potion = Instantiate(spacePotionPrefab, transform.position, transform.rotation);
        potion.GetComponent<Rigidbody2D>().simulated = false;
    }
    private void ThrowPotionWhenRelease()
    {
        if (Input.GetMouseButtonUp(0) && potion)
        {
            Rigidbody2D potionRB2D = potion.GetComponent<Rigidbody2D>();
            potionRB2D.gravityScale = 2;
            potionRB2D.velocity = force;    //Throw potion
            potion.GetComponent<Rigidbody2D>().simulated = true;
            HideTrajectoryPath();
            isAiming = false;
            if (potion.tag == "Fire Potion")
            {
                StartCoroutine("ThrowFirePotion");
                

            }
            else if (potion.tag == "Ice Potion")
            {
                StartCoroutine("ThrowIcePotion");

            }
            else if (potion.tag == "Space Potion")
            {
                StartCoroutine("ThrowSpacePotion");
            }
        }
    }

    private IEnumerator ThrowFirePotion()
    {
        fireReady = false;
        yield return new WaitForSeconds(fireCD);
        fireReady = true;
    }
    private IEnumerator ThrowIcePotion()
    {
        iceReady = false;
        yield return new WaitForSeconds(iceCD);
        iceReady = true;
    }
    private IEnumerator ThrowSpacePotion()
    {
        spaceReady = false;
        yield return new WaitForSeconds(spaceCD);
        spaceReady = true;
    }
    private void HideTrajectoryPath()
    {
        for (int i = 0; i < NUM_OF_POINTS; i++)
        {
            trajectoryPointsList[i].SetActive(false);
        }
    }
    private void AimPotion()
    {
        if (isAiming)
        {
            playerController.isAiming = true;

            Vector2 mouseStretchPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            mouseDiff = mouseOriginPoint - mouseStretchPoint;

            if (mouseDiff.x > 0)
            {
                playerController.aimTo = PlayerController.AimTo.Right;
            }
            else
            {
                playerController.aimTo = PlayerController.AimTo.Left;
            }

            angle = Vector2.SignedAngle(Vector2.right, mouseDiff);

            potion.transform.position = transform.position;

            Vector2 velocity = power * mouseDiff.magnitude;


            velocity.x = RestrictForce(velocity.x);
            velocity.y = RestrictForce(velocity.y);

            float force_x = velocity.x * Mathf.Cos(angle * Mathf.Deg2Rad);
            float force_y = velocity.y * Mathf.Sin(angle * Mathf.Deg2Rad);

            force = new Vector2(force_x, force_y);

            DrawTrajectoryPath(angle, velocity);

            ChangePotionWhenModeChanged();
        }
        else
        {
            playerController.isAiming = false;
        }
    }
    private float RestrictForce(float force)
    {
        if(force > maxForce)
        {
            force = maxForce;
        }
        return force;
    }
    private void DrawTrajectoryPath(float angle, Vector2 velocity)
    {
        float time = 0;
        float x_coordinate;
        float y_coordinate;

        for (int i = 0; i < NUM_OF_POINTS; i++)
        {
            time = i * 0.1f;
            x_coordinate = transform.position.x + (velocity.x * Mathf.Cos(angle * Mathf.Deg2Rad) * time);
            y_coordinate = transform.position.y + (velocity.y * Mathf.Sin(angle * Mathf.Deg2Rad) * time) - (Physics2D.gravity.magnitude * time * time);

            Vector2 coordinate = new Vector2(x_coordinate, y_coordinate);

            trajectoryPointsList[i].transform.position = coordinate;
            trajectoryPointsList[i].SetActive(true);
        }
    }
    private void ChangePotionWhenModeChanged()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            Destroy(potion.gameObject);
            potion = null;
            CreatePotion(PlayerController.PotionMode.Fire);
        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(potion.gameObject);
            potion = null;
            CreatePotion(PlayerController.PotionMode.Ice);
        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            Destroy(potion.gameObject);
            potion = null;
            CreatePotion(PlayerController.PotionMode.Space);
        }
    }
}
