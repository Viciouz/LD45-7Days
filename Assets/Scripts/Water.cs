using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Water : MonoBehaviour
{

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start() {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other) {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Leaf leaf = other.GetComponent<Leaf>();
        int i = 0;

        while (i < numCollisionEvents) {
            if (leaf) {
                leaf.branch.water += Tree.Instance.waterPoints;
                leaf.transform.DOShakeRotation(0.3f);
            }
            i++;
        }
    }
}
