
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed;
    private float direction;
    private float lifetime;
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anim;

    // Awake is called before the first frame update
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0); //To move an object -> use transform.translate

        lifetime += Time.deltaTime;

        if (lifetime > 5) {
            Deactivate();
        }
    }

    //When the projectile hits an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        // Check if we hit the enemy
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    //To move the fireball
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction) //Flip the projectile
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
