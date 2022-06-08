using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class LevelObject : MonoBehaviour
{
    public Transform spawnStickerLocation;
    public P3dPaintableTexture paintable;
    public P3dPaintableTexture paintable2;
    public GameObject stencil;
    public Material overlayMat;
    public bool overlayAdded = false;
    public GameObject endCharacter;
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public GameObject moneyRoll;

    void Start()
    {
        moneyRoll.SetActive(true);
        paintable.gameObject.SetActive(false);
        Timer.Delay(1.3f, () =>
        {
            moneyRoll.transform.GetChild(0).GetComponent<Animator>().Play("MoneyRoll");
            Timer.Delay(1.2f, () =>
            {
                moneyRoll.SetActive(false);
                paintable.gameObject.SetActive(true);
            });

        });
    }
}
