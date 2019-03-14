using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Attack : MonoBehaviour {

    Player1Movement playerMove;

    public Transform basicShotSpawn;
    public GameObject basicAttack;
    public SpriteRenderer shieldRender;

    #region  Variables
    private float fireRate;
    private float nextFire = 0.0f;
    private bool isShieldActive = false;

    private int shieldHealth = 20;
    private const int maxShieldHealth = 20;
    #endregion

    // Use this for initialization
    void Start() {
        playerMove = GetComponent<Player1Movement>();

        fireRate = 0.3f ;
    }

    // Update is called once per frame
    void Update() {
        GetInput();
    }

    void GetInput()
    {
        if (
            Input.GetKey(KeyCode.V) &&
            !playerMove.GetIsMoving() &&
            Time.time > nextFire 
            )
        {
            Debug.Log("Shooting");
            nextFire += Time.deltaTime + fireRate;
            BasicAttack();
        }

        if (Input.GetKeyDown(KeyCode.B))
            SkillAttack();

        if (Input.GetKeyDown(KeyCode.N))
            UltimateAttack();

        if (Input.GetKey(KeyCode.M) && (shieldHealth > 0))
        {
            Debug.Log("Activando Protocolo Ruedines.");
            isShieldActive = true;
            shieldRender.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.M) || (shieldHealth < 0))
        {
            Debug.Log("Descactivando Protocolo Ruedines.");
            UnableShield();
        }
    }

    void BasicAttack()
    {
        Instantiate(basicAttack, basicShotSpawn.position, basicShotSpawn.rotation);
    }

    void SkillAttack()
    { }

    void UltimateAttack()
    { }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isShieldActive)
        {
            shieldHealth -= 5;
        }
    }

    void UnableShield()
    {
        isShieldActive = false;
        shieldRender.enabled = false;
        // Animaciones de rotura de escudo o desactivacion.
    }
}
