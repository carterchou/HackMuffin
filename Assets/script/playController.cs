using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playController : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Rigidbody2D rig2d;
	public GameObject prefab;
	public rotate rotateOBJ;
	bool isInit;
	int moveType = 0;
	public bool isRoot = false;
	public float canMoreCoolDown = 0.5f;
	public float canHitCoolDown = 0.5f;
	public float canMoveCoolDown = 2; //分裂時先停在原地

	public AudioClip[] se; //分裂 聚合
	public AudioSource sePlayer;

	public float scaleMax = 0.8f;
	public int hp;

    public void init(int hp, bool isMore = false)
	{
		this.hp = hp;
		if(GameManager.GetInstance().MainGameController.slimes.Contains(this) == false)
        {
			GameManager.GetInstance().MainGameController.slimes.Add(this);
		}
		
		canMoreCoolDown = 2;
		canMoveCoolDown = isMore ? 3 : 0;
		isInit = true;
	}

	public void SetRoot(bool isRoot)
    {
		this.isRoot = isRoot;
        if (GetComponent<BoxCollider2D>().isTrigger && isRoot)
        {
			GetComponent<BoxCollider2D>().isTrigger = false;
		}
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
	}

	void coolDown()
    {
		if (canMoveCoolDown > 0)
		{
			canMoveCoolDown -= Time.deltaTime;
			return;
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
		if (other.collider.tag == "enemy" && canHitCoolDown <= 0)
		{
			animator.SetTrigger("doTouch");
			canHitCoolDown = 0.5f;
			Debug.Log("Get hit");
			becomeMore(other.collider);
		}
	}

    private void OnCollisionExit2D(Collision2D other)
    {
		if (other.collider.tag == "Player" && GetComponent<BoxCollider2D>().isTrigger)
		{
			GetComponent<BoxCollider2D>().isTrigger = false;
		}
	}

    private void OnCollisionStay2D(Collision2D other)
    {
		if (other.collider.tag == "enemy" && canHitCoolDown <= 0)
		{
			animator.SetTrigger("doTouch");
			canHitCoolDown = 0.5f;
			Debug.Log("Get hit");
			becomeMore(other.collider);
		}else if (other.collider.tag == "Player" && isRoot && canMoreCoolDown <= 0)
		{
			combine(other.collider);
		}
	}


    void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.tag == "enemy" && canHitCoolDown <= 0)
		{
			animator.SetTrigger("doTouch");
			canHitCoolDown = 0.5f;
			Debug.Log("Get hit");
			becomeMore(other);
		}
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
				sePlayer.PlayOneShot(se[0]);
			}

			force3 = (transform.position - other.transform.position).normalized;
			force = new Vector2(force3.x, force3.y) * Random.Range(1, 2);
			rig2d.velocity = force;
		}
        else
        {
			GameManager.GetInstance().MainGameController.slimes.Remove(this);
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
		GameManager.GetInstance().MainGameController.slimes.Remove(otherSlime);
		sePlayer.PlayOneShot(se[1]);
		Destroy(otherSlime.gameObject);

	}
}
