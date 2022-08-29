using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;


public class PhotoCapture : MonoBehaviour
{
    [SerializeField] private Image photoDisplay;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    private GameEvents gameEvents;
    private Texture2D screenCapture;

    private void Start()
    {
        gameEvents = GameEvents.instance;
        gameEvents.OnFinish += TakeAScreenShot;
        gameEvents.OnEnd += () => photoFrame.SetActive(false);

        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }


    private void TakeAScreenShot()
    {
        StartCoroutine(CapturePhoto());
    }



    IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }


    private void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100f);
        photoDisplay.sprite = photoSprite;

        photoFrame.SetActive(true);
        StartCoroutine(flash());
        StartCoroutine(MovePhotoCorner());
    }


    IEnumerator flash()
    {
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    IEnumerator MovePhotoCorner()
    {
        yield return new WaitForSeconds(1);
        MovePhoto();
    }
    private void MovePhoto()
    {
        Sequence photoSeq = DOTween.Sequence();
        photoSeq.Append(photoFrame.transform.DORotate(new Vector3(0, 0, 30), 1, RotateMode.Fast)).
            Join(photoFrame.transform.DOScale(new Vector3(0.5f, 0.5f, 1), 1)).Join(photoFrame.transform.DOMove(new Vector3(-1.5f, 3.7f, 50), 1));
    }
}
