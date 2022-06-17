using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesePastaPlateManager : MonoBehaviour
{
    public ParticleSystem particles;
    public Animator anim;
    bool held = false;
    bool toSetInput = true;
    bool checkLevelEnd = true;
    public Transform pastaPlate;
    public Transform lerpPos;
    public float currentCheeseAmount = 0;
    public float maxCheeseAmount = 8f;
    [SerializeField] bool canPlay;
    bool CanPlay
    {
        get
        {
            return canPlay;
        }

        set
        {
            canPlay = value;
            objectFollowMouse.enabled = value;
        }
    }

    public ObjectFollowMouse objectFollowMouse;

    void Start()
    {
        CanPlay = false;
        Timer.Delay(2.0f, () =>
        {
            CanPlay = true;
        });
    }
    void Update()
    {
        SetInput();
        ParticleState();
        LevelEnd();
    }

    void SetInput()
    {
        if (!toSetInput) return;
        if (Input.GetMouseButtonDown(0))
        {
            held = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            held = false;
        }
    }

    void ParticleState()
    {

        if (held)
        {
            if (!CanPlay) return;

            if (!particles.isPlaying)
            {
                particles.Play();
                anim.Play("Season");
            }
        }
        else
        {
            if (particles.isPlaying)
            {
                particles.Stop();
                anim.Play("Idle");
            }
        }
    }

    void LevelEnd()
    {
        if (!checkLevelEnd) return;

        currentCheeseAmount += Time.deltaTime;

        if (currentCheeseAmount >= maxCheeseAmount)
        {
            checkLevelEnd = false;
            StepComplete();
        }
    }

    void StepComplete()
    {
        CameraController.instance.SetCurrentCamera("PlatingFinishPasta", 1);
        objectFollowMouse.transform.GetChild(0).gameObject.SetActive(false);
        toSetInput = false;
        held = false;
        Timer.Delay(1.0f, () =>
        {
            Timer.Delay(0.5f, () =>
            {
                CameraSpaceEffects.instance.OnEfx(CameraSpaceEffects.instance.confetti);

            });
            UIElements.instance.gameObject.GetComponent<RespondMessage>().ShowCorrectResponse(UIElements.instance.gameObject.GetComponent<RespondMessage>().correctList2);

            CameraSpaceEffects.instance.OnEfx(CameraSpaceEffects.instance.twinkle);
        });
        // Timer.Delay(2.5f, () =>
        // {
        //     LerpObjectPosition.instance.LerpObject(pastaPlate, lerpPos.transform.position, 1f, () =>
        //     {
        //         LerpObjectLocalRotation.instance.LerpObject(pastaPlate, lerpPos.transform.rotation, 1f, () =>
        //         {
        //             pastaPlate.GetComponent<ObjectRotate>().enabled = true;
        //             Timer.Delay(2.0f, () =>
        //             {
        //                 CameraSpaceEffects.instance.SpawnEFX(CameraSpaceEffects.instance.confetti);
        //                 Timer.Delay(2.0f, () =>
        //                 {
        //                     GameManager.Instance.NextStep();
        //                 });

        //             });
        //         });
        //     });
        // });
    }
}
