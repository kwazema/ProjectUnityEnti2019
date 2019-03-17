using System;
using UnityEngine;

//public class IAMovement : MonoBehaviour
//{
//    public float health;
//    public float damage;

//	public Rigidbody2D rigidbody2D;
//	public Animator animator;
//    public Vector2 v2;

//    private BoxCollider2D playerCol;

//    public float speedRun;
//    public float direction;
//    public float timeToNextAttack = 0;
//    float timeToChangeColor = 0;
//    float timeToDead = 0;

//    //private RaycastHit2D rightGround;
//    //private RaycastHit2D leftGround;
//    //private RaycastHit2D rightBody;
//    //private RaycastHit2D leftBody;

//    public Vector3 v2SizeEnemy;

//    public LayerMask layerGround;
//    public LayerMask layerEnemy;

//    enum States
//	{
//		Idle,
//		MoveCloser,
//		Attack,
//		Dead
//	}
//	States state;

//	States State
//	{
//		get
//		{
//			return state;
//		}
//		set
//		{
//			if (state != States.Dead)
//			{
//				state = value;
//				int intState = (int)state;
//				state = (States)Mathf.Clamp(intState, 0, 3);
//				//Debug.Log("Changed to state" + state);
//				animator.SetInteger("State", intState);
//			}
//		}
//	}

//    //States GetState()
//    //{
//    //    return state;
//    //}
//    //void SetState(States value)
//    //{
//    //    if (state != States.Dead)
//    //    {
//    //        state = value;
//    //        int intState = (int)state;
//    //        state = (States)Mathf.Clamp(intState, 0, 3);
//    //        Debug.Log("Changed to state" + state);
//    //        animator.SetInteger("State", intState);
//    //    }
//    //}

//    private void Awake()
//    {
//        playerCol = GameObject.Find("ControlerCharacter").GetComponent<BoxCollider2D>();
//    }

//    void Start()
//	{
//		state = States.Idle;
//        speedRun = 2;
//        direction = 1;
//        damage = 5;
//        health = 100;
//        timeToDead = 2;

//        v2SizeEnemy = new Vector3(1f, .5f);
//    }

//    private void Update()
//    {
//        FlipPlayer();

//        AutoChangeColor();
//        AutoDead();
//    }

//    void FixedUpdate()
//	{
//        switch (State)
//		{
//			case States.Idle:
//				Idle();
//				break;
//			case States.MoveCloser:
//				MoveCloser();
//				break;
//			case States.Attack:
//				Attack();
//				break;
//			case States.Dead:
//				Dead();
//				break;
//		}

//        RayCastBody();
//        RayCastGround();


//        v2.y = rigidbody2D.velocity.y; // Vertical
//        v2.x = direction * speedRun; // Horizontal

//        rigidbody2D.velocity = v2; // Aplicamos al RG
//    }

//	void Idle()
//	{
//        speedRun = 1.5f;
//    }

//    void MoveCloser()
//	{
//        FollowPlayer();
//        speedRun = 3;
//    }

//    void Attack()
//	{
//        Player player = GameObject.Find("ControlerCharacter").GetComponent<Player>();
//        speedRun = 0;

//        if (Time.time > timeToNextAttack)
//        {
//            timeToNextAttack = Time.time;
//            timeToNextAttack += 1f;


//            player.SetDamage(damage);
//        }
//    }

//    void Dead()
//	{
//        animator.SetTrigger("Die");
//        speedRun = 0;
//    }

//    void OnTriggerEnter2D(Collider2D collider)
//	{
//		Player player = collider.GetComponent<Player>();
//        if (player)
//			State++;
//	}

//	void OnTriggerExit2D(Collider2D collider)
//	{
//		Player player = collider.GetComponent<Player>();
//		if (player)
//			State--;
//	}

//	public void ReceiveDamage(float damage)
//	{
//        if (damage >= health)
//        {
//            health = 0;
//            state = States.Dead;

//            Physics2D.IgnoreCollision(playerCol, GetComponent<CapsuleCollider2D>());
//            //gameObject.layer = 10;

//            timeToDead = Time.time;
//            timeToDead += 2;
//        }
//        else
//        {
//            health -= damage;
//        }

//        SetColor(Color.red);
//        timeToChangeColor = Time.time;
//        timeToChangeColor += .2f;

//        Debug.Log("Enemy Recive Damage");
//	}

//    void AutoChangeColor()
//    {
//        if (GetComponent<SpriteRenderer>().color == Color.red)
//        {
//            if (Time.time > timeToChangeColor)
//            {
//                SetColor(Color.white);
//            }
//        }
//    }

//    void AutoDead()
//    {
//        if (health <= 0)
//        {
//            if (Time.time > timeToDead)
//            {
//                Destroy(gameObject);
//            }
//        }
//    }

//    public void SetColor(Color color)
//    {
//        GetComponent<SpriteRenderer>().color = color;
//    }

//    void FollowPlayer()
//    {
//        Player player = GameObject.Find("ControlerCharacter").GetComponent<Player>();

//        //Debug.Log("V2 Player: " + player.transform.position.x);
//       //Debug.Log("V2 Enemy: " + transform.position.x);

//        float distanceX = player.transform.position.x - transform.position.x;
//        float distanceY = player.transform.position.y - transform.position.y;


//        if (distanceX < 0)
//        {
//            direction = -1f;
//        }
//        else
//        {
//            direction = 1f;
//        }

//        if (Mathf.Abs(distanceX) > 0f && Mathf.Abs(distanceX) < .5f)
//        {
//            if (Mathf.Abs(distanceY) > .5f)
//            {
//                direction = 0;
//            }
//        }
//    }

//    void FlipPlayer()
//    {
//        if (rigidbody2D.velocity.x > 0.1f)
//        {
//            transform.eulerAngles = new Vector3(0, 180, 0);
//        }
//        else if (rigidbody2D.velocity.x < -0.1f)
//        {
//            transform.eulerAngles = new Vector3(0, 0, 0);
//        }
//    }

//    void RayCastGround()
//    {
//        bool rightGround = Physics2D.Raycast(new Vector2(transform.position.x + v2SizeEnemy.x, transform.position.y - v2SizeEnemy.y), Vector2.down, .15f, layerGround); 
//        Debug.DrawRay(new Vector2(transform.position.x + v2SizeEnemy.x, transform.position.y - v2SizeEnemy.y), Vector2.down * .15f, Color.blue, .05f);

//        if (!rightGround)
//        {
//            direction = -1;

//            if (state == States.MoveCloser)
//            {
//                direction = 0;
//            }
//        }

//        //-------------------------------------------//

//        bool leftGround = Physics2D.Raycast(new Vector2(transform.position.x - v2SizeEnemy.x, transform.position.y - v2SizeEnemy.y), Vector2.down, .15f, layerGround);
//        Debug.DrawRay(new Vector2(transform.position.x - v2SizeEnemy.x, transform.position.y - v2SizeEnemy.y), Vector2.down * .15f, Color.yellow, .05f);

//        if (!leftGround)
//        {
//            direction = 1;

//            if (state == States.MoveCloser)
//            {
//                direction = 0;
//            }
//        }
//    }

//    void RayCastBody()
//    {
//        if (state == States.Idle)
//        {
//            bool rightBody = Physics2D.Raycast(new Vector2(transform.position.x + v2SizeEnemy.x, transform.position.y), Vector2.right, 0.3f, layerEnemy);
//            Debug.DrawRay(new Vector2(transform.position.x + v2SizeEnemy.x, transform.position.y), Vector2.right * 0.3f, Color.green, .05f);

//            if (rightBody)
//            {
//                direction = -1;
//            }

//            //-------------------------------------------//

//            bool leftBody = Physics2D.Raycast(new Vector2(transform.position.x - v2SizeEnemy.x, transform.position.y), Vector2.left, 0.3f, layerEnemy);
//            Debug.DrawRay(new Vector2(transform.position.x - v2SizeEnemy.x, transform.position.y), Vector2.left * 0.3f, Color.red, .05f);


//            if (leftBody)
//            {
//                //Enemy enemyLeft = leftBody.collider.GetComponent<Enemy>();
//                direction = 1;
//            }
//        }
//    }
//}