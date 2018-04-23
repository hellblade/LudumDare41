using UnityEngine;
using System.Collections;

public class CoinPickup : Pickup
{
    RunnerManager manager;
    GeneratorManager genManager;
    AudioSource audio;

    static ObjectPool<CoinPickup> Pool = new ObjectPool<CoinPickup>();

    public static CoinPickup GetObject(CoinPickup source)
    {
        CoinPickup result = null;

        if (!Pool.TryGet(ref result))
        {
            result = Instantiate(source);
        }

        return result;
    }

    protected override System.Type RequiredComponent
    {
        get
        {
            return typeof(InventoryManager);
        }
    }

    public override GameObject Spawn(float x, float y)
    {
        throw new System.NotImplementedException();
    }

    protected override bool OnPickup(Component target)
    {
        var inventory = target as InventoryManager;

        if (!inventory)
            return false;

        AudioSource.PlayClipAtPoint(audio.clip, transform.position);

        inventory.AddCoin();
        this.gameObject.SetActive(false);
        Pool.Free(this);

        return true;
    }

    private void Awake()
    {
        manager = FindObjectOfType<RunnerManager>();
        genManager = FindObjectOfType<GeneratorManager>();

        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        transform.position -= manager.CurrentMoveSpeed * Time.deltaTime;

        if (!manager.IsGameRunning || transform.position.x < -genManager.ScreenAmountX - 2)
        {
            Remove();
        }
    }

    public void Remove()
    {
        gameObject.SetActive(false);
        Pool.Free(this);
    }
}