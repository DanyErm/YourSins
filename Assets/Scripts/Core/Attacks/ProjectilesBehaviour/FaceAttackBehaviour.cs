using System.Collections;
using UnityEngine;

public class FaceAttackBehaviour : MonoBehaviour
{
    [SerializeField] private float harmlessPartTime = 1.5f;
    [SerializeField] private float harmfulPartTime = 1.5f;

    [SerializeField] private Animator faceAnimator;
    [SerializeField] private CircleCollider2D circleCollider2D;


    private IEnumerator AfterAppearingAnimCourutine()
    {
        yield return new WaitForSeconds(harmlessPartTime);
        faceAnimator.Play("ShowingTeethAnimation");
    }

    private IEnumerator AfterShowingTeethAnimCourutine()
    {
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(harmfulPartTime);
        circleCollider2D.enabled = false;
        faceAnimator.Play("DisappearingAnimation");
    }


    public void AfterAppearingAnimEvent()
    {
        StartCoroutine(AfterAppearingAnimCourutine());
    }

    public void AfterShowingTeethAnimEvent()
    {
        StartCoroutine(AfterShowingTeethAnimCourutine());
    }

    public void AfterDisappearingAnimEvent()
    {
        Destroy(gameObject);
    }
}