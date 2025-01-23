using UnityEngine;

public class PlayerPartsRandomiser : MonoBehaviour
{
    [SerializeField] private Sprite[] randomHead;
    [SerializeField] private Sprite[] randomBody;
    [SerializeField] private Sprite[] randomLegs;
    [SerializeField] private Sprite[] randomArms;

    [SerializeField] private SpriteRenderer head;
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer leftLeg;
    [SerializeField] private SpriteRenderer rightLeg;
    [SerializeField] private SpriteRenderer leftArm;
    [SerializeField] private SpriteRenderer rightArm;

    public void RandomizeParts()
    {
        int headID = Random.Range(0, randomHead.Length);
        int bodyID = Random.Range(0, randomBody.Length);
        int legsID = Random.Range(0, randomLegs.Length);
        int armsID = Random.Range(0, randomArms.Length);

        head.sprite = randomHead[headID];
        body.sprite = randomBody[bodyID];
        leftLeg.sprite = randomLegs[legsID];
        rightLeg.sprite = randomLegs[legsID];
        leftArm.sprite = randomArms[armsID];
        rightArm.sprite = randomArms[armsID];
    }
}
