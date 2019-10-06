using DG.Tweening;
using System.Collections;
using UnityEngine;

public enum State { Healthy, Decaying, Dying }

public class Leaf : MonoBehaviour {

    public float life;

    public SpriteRenderer sprite;

    public Branch branch;

    public State state = State.Healthy;

    public bool active = true;

    public Color healthyColor;
    public Color decayingColor;
    public Color dyingColor;

    public Rigidbody2D body;

    private void Start() {
        life = Tree.Instance.leafLife;
        transform.DOBlendableScaleBy(new Vector3(1f, 1f), 0.2f).OnComplete(() => { transform.DOPunchScale(new Vector3(0.3f, 0.3f), 0.5f); });
        if (branch == null) {
            branch = transform.parent.GetComponent<Branch>();
        }
    }

    private readonly float timeStep = 0.1f;
    private float time = 0;

    private void Update() {
        if (active) {
            time += Time.deltaTime;
            if (time > timeStep) {
                time = 0f;
                if (branch.water > 0f) {
                    branch.Grow();
                    life++;
                    branch.water--;
                    if (state != State.Healthy) {
                        sprite.DOColor(healthyColor, 0.5f);
                        state = State.Healthy;
                    }
                } else if (life <= 0f) {
                    branch.DeathOfABeutifulLeaf(gameObject);
                    StartCoroutine(BreakLeaf());
                    branch.water += Tree.Instance.waterPoints;
                } else if (life < 50f && state == State.Decaying) {
                    sprite.DOColor(dyingColor, 0.5f);
                    state = State.Dying;
                } else if (life < 100f && state == State.Healthy) {
                    sprite.DOColor(decayingColor, 0.5f);
                    state = State.Decaying;
                } else if (life > 100f && state != State.Healthy) {
                    sprite.DOColor(healthyColor, 0.5f);
                } else {
                    life--;
                }

            }
        }

    }

    public IEnumerator BreakLeaf() {
        active = false;
        if(body != null) {
            body.isKinematic = false;
            body.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
        }
  

        transform.DOScale(0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }
}
