using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;
using UnityEngine.UI;

public class ClearPanel : Panel
{
    [SerializeField]
    RectTransform textContainer = null;

    [SerializeField]
    Text textOriginal = null;

    [SerializeField]
    float scrollSpeed = 4.0f;

    [SerializeField]
    Animator bookAnimator = null;

    [SerializeField]
    GameObject backPanel = null;

    [SerializeField]
    Transform cameraTransform = null;

    [SerializeField]
    Transform targetCameraTransform = null;

    [SerializeField]
    GameObject namingContainer = null;

    [SerializeField]
    InputField input = null;

    [SerializeField]
    Button okButton = null;

    [SerializeField]
    TextMesh textmesh = null;

    [SerializeField]
    GameObject pageObj = null;

    [SerializeField]
    GameObject gameClearMessage = null;

    [SerializeField]
    ParticleSystem[] particles = null;

    float currentSpeed;

    public void Init(string[] story)
    {
        textContainer.gameObject.SetActive(false);
        textContainer.sizeDelta = new Vector2(1500.0f, textOriginal.fontSize * story.Length * 2);
        textContainer.anchoredPosition = Vector2.down * 750.0f;

        for(int i = 0 ; i < story.Length ; i++)
        {
            var text = Instantiate(textOriginal, textContainer);
            text.text = story[i];
            text.gameObject.SetActive(true);
        }

        textContainer.gameObject.SetActive(true);
    }

    public override void Activate()
    {
        base.Activate();

        StartCoroutine(ScrollAnimation());
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            currentSpeed += Time.deltaTime * 1.0f;
        }
        else
        {
            currentSpeed -= Time.deltaTime * 1.0f;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, scrollSpeed, currentSpeed * 3.0f);
    }

    IEnumerator ScrollAnimation()
    {
        var startDelayTime = 0.5f;

        yield return new WaitForSeconds(startDelayTime);

        currentSpeed = scrollSpeed;
        while(true)
        {
            yield return null;
            textContainer.anchoredPosition += Vector2.up * currentSpeed;

            if(textContainer.anchoredPosition.y > textContainer.sizeDelta.y - 500.0f) break;
        }

        StartCoroutine(CloseBookAnimation());
    }

    IEnumerator CloseBookAnimation()
    {
        backPanel.SetActive(false);
        textContainer.gameObject.SetActive(false);
        pageObj.SetActive(false);

        bookAnimator.Play("CloseBook");

        yield return new WaitForSeconds(1.5f);

        var startPos = cameraTransform.position;
        var startRot = cameraTransform.rotation;

        yield return KKUtilities.FloatLerp(1.0f, (t) =>
        {
            var temp = Easing.InQuad(t);
            cameraTransform.SetPositionAndRotation(
                Vector3.Lerp(startPos, targetCameraTransform.position, temp),
                Quaternion.Lerp(startRot, targetCameraTransform.rotation, temp));
        });

        namingContainer.SetActive(true);
        while(true)
        {
            yield return KKUtilities.WaitAction(okButton.onClick);

            if(!string.IsNullOrEmpty(input.text)) break;
        }

        namingContainer.SetActive(false);
        textmesh.text = input.text;

        //1回転
        var c = Camera.main.transform;

        yield return KKUtilities.FloatLerp(0.5f, (t) =>
        {
            c.AddLocalPositoinZ(-1.0f * Time.deltaTime);
        });

        gameClearMessage.SetActive(true);
        foreach(var p in particles) p.Play(true);
        while(true)
        {


            yield return KKUtilities.FloatLerp(3.0f, (t) =>
            {
                bookAnimator.transform.parent.SetRotationZ(Mathf.Lerp(0.0f, 360.0f, Easing.Linear(t)));
            });

            yield return new WaitForSeconds(3.0f);
        }
    }


}
