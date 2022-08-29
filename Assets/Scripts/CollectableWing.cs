using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CollectableWing : MonoBehaviour, IInteract
{

    private GameObject particle;
    private GameObject plus;
    private CameraController cameraController;
    private WingPool wingPool;

    private GameObject collectableWing;



    private void Start()
    {

        cameraController = CameraController.instance;
        wingPool = WingPool.instance;
      

        collectableWing = transform.GetChild(0).gameObject;
    }

    public void Interact(GameObject go)
    {


        collectableWing.SetActive(false);
      

        StartCoroutine(Sprinkles());
        if (go.GetComponent<PlayerController>())
        {

            StartCoroutine(plus3());
        }

        StartCoroutine(ComeBack());
    }

    IEnumerator ComeBack()
    {
        yield return new WaitForSeconds(0.2f);
        collectableWing.gameObject.SetActive(true);
    }



    private IEnumerator Sprinkles()
    {
        particle = wingPool.GetwingFromPool(1);
        ParticleSystem par = particle.GetComponentInChildren<ParticleSystem>();
        par.transform.position = transform.position;
        par.Play();
        yield return new WaitForSeconds(2);
        wingPool.ReturnWingToPool(particle, 1);
    }


    private IEnumerator plus3()
    {
        plus = wingPool.GetwingFromPool(2);
        TextMeshPro plus3 = plus.GetComponentInChildren<TextMeshPro>();
        plus3.text = "+3";
        plus3.transform.position = transform.position;
        plus3.transform.LookAt(2 * plus3.transform.position - cameraController.transform.position);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(plus3.rectTransform.DOMoveY(3, 1)).Join(plus3.DOFade(0, 0.5f)).Restart();

        yield return new WaitForSeconds(2);
        wingPool.ReturnWingToPool(plus, 2);
    }
}
