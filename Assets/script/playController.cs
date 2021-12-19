using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playController : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Rigidbody2D rig2d;
	public GameObject prefab;
	public GameObject fire;
	public rotate rotateOBJ;
	public aim aimOBJ;
	bool isInit;
	int moveType = 0;
	public bool isRoot = false;
	public float canMoreCoolDown = 0.5f;
	public float canHitCoolDown = 0.5f;
	public float canMoveCoolDown = 2; //分裂時先停在原地

	public float scaleMax = 0.8f;
	public int hp;

    private void OnEnable()
    {
		if (isRoot && isInit == false)
		{
			init(GameManager.GetInstance().playerMaxHP);
		}
	}

    public void init(int hp, bool isMore = false)
	{
		this.hp = hp;
		if(GameManager.GetInstance().slimes.Contains(this) == false)
        {
			GameManager.GetInstance().slimes.Add(this);
		}
		
		canMoreCoolDown = 2;
		canMoveCoolDown = isMore ? 3 : 0;
		isInit = true;
	}

	public void SetRoot(bool isRoot)
    {
		this.isRoot = isRoot;
	}

	public bool CheckRoot()
	{
		return isRoot;
	}

	void Update()
	{
		if(isInit == false)
        {
			return;
        }

		//size
		changeSize();

		//coolDown
		coolDown();

		move();

		attack();

	}

	void move()
    {
		float Horizontal = 0;
		float Vertical = 0;
		bool run = false;
		if (canMoveCoolDown <= 0)
		{
			Horizontal = Input.GetAxisRaw("Horizontal");
			Vertical = Input.GetAxisRaw("Vertical");
			run = Input.GetKey(KeyCode.LeftShift);
		}

		if (Horizontal == 0 && Vertical == 0)
		{
			rig2d.velocity = Vector2.Lerp(rig2d.velocity, Vector2.zero, 0.1f);
			animator.SetBool("isWalk", false);
		}
		else
		{
			Vector2 velocity = moveCircle(new Vector2(Horizontal, Vertical));
			rig2d.velocity = velocity * (run ? GameManager.GetInstance().speed * 2 : GameManager.GetInstance().speed);

			if(Horizontal >= 0.01f)
            {
				spriteRenderer.flipX = false;
            }
            else if(Horizontal <= -0.01f)
            {
				spriteRenderer.flipX = true;
			}
			animator.SetBool("isWalk", true);
		}
	}

	void attack()
    {
		if (Input.GetMouseButton(0)){
			rotateOBJ.isRotate = true;
		}else{
			rotateOBJ.isRotate = false;
		}
		fireball();
	}

	
	void fireball()
    {
        if (Input.GetMouseButton(0)){
			aimOBJ.click = true;
		}
		if (Input.GetMouseButtonDown(1)){
			aimOBJ.click = true;
			fireball fireball = Instantiate(fire, transform.position, transform.rotation).GetComponent<fireball>();
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			fireball.targetPos = new Vector3(pos.x, pos.y,0);
			fireball.isInit = true;
			//fire.rigidbody.AddForce(aimOBJ.position);
		}
        else
        {
			aimOBJ.click = false;
		}
	}


	void coolDown()
    {
		if (canMoveCoolDown > 0)
		{
			canMoveCoolDown -= Time.deltaTime;
			GetComponent<BoxCollider2D>().isTrigger = true;
			return;
        }
        else
        {
			GetComponent<BoxCollider2D>().isTrigger = false;
        }

		if (canMoreCoolDown > 0)
        {
			canMoreCoolDown -= Time.deltaTime;

		}

		if(canHitCoolDown > 0)
        {
			canHitCoolDown -= Time.deltaTime;

		}
	}

	void changeSize()
    {
		float size = hp / (float)GameManager.GetInstance().playerMaxHP;
		transform.localScale = new Vector3(size, size, size) * scaleMax;

        if (isRoot)
        {
			spriteRenderer.sortingOrder = 2;
        }
        else
        {
			spriteRenderer.sortingOrder = 2;
		}
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
		if (other.collider.tag == "enemy" && canHitCoolDown <= 0)
		{
			canHitCoolDown = 2;
			Debug.Log("Get hit");
			becomeMore(other.collider);
		}
	}

    private void OnCollisionStay2D(Collision2D other)
    {
		if (other.collider.tag == "Player" && isRoot && canMoreCoolDown <= 0)
		{
			combine(other.collider);
		}
	}

	



    void OnTriggerEnter2D(Collider2D other)
	{

		//animator.SetTrigger("hasDamage");
	}

	Vector2 moveCircle(Vector2 raw)
    {
		Vector2 newV = new Vector2();
		newV.x = raw.x * Mathf.Sqrt(1 - (raw.y * raw.y) / 2);
		newV.y = raw.y * Mathf.Sqrt(1 - (raw.x * raw.x) / 2);
		return newV;
	}

	void becomeMore(Collider2D other)
    {
		int damage = 2;

		hp = Mathf.Clamp(hp - damage, 0, GameManager.GetInstance().playerMaxHP);
		if(hp > 0)
        {
			Vector3 force3;
			Vector2 force;
			if (prefab)
			{
				Vector3 newPosition = transform.position;
				playController more = Instantiate(prefab, newPosition, transform.rotation).GetComponent<playController>();
				more.init(damage, true);
				more.SetRoot(false);
				force3 = (transform.position - other.transform.position).normalized ;
				force = new Vector2(force3.x, force3.y) * Random.Range(4,8);
				more.GetComponent<Rigidbody2D>().velocity = force;
			}

			force3 = (transform.position - other.transform.position).normalized;
			force = new Vector2(force3.x, force3.y) * Random.Range(1, 2);
			rig2d.velocity = force;
		}
        else
        {
			GameManager.GetInstance().slimes.Remove(this);
			Destroy(gameObject);
		}

    }

	void combine(Collider2D other)
    {
		playController otherSlime = other.GetComponent<playController>();
		if(otherSlime.canMoreCoolDown > 0)
        {
			return;
        }
		canMoreCoolDown = 0.5f;
		hp = Mathf.Clamp(otherSlime.hp + hp, 0, GameManager.GetInstance().playerMaxHP);
		GameManager.GetInstance().slimes.Remove(otherSlime);
		Destroy(otherSlime.gameObject);

	}
}
