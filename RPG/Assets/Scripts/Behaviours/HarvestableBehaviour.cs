using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HarvestableBehaviour : InteractableBehaviour
{
    public AudioClip harvestSound;
    public int health = 3;

    public override void Interact(IAnimatable iAnimatable)
    {
        iAnimatable.DoAttack();

        Invoke("PlayAudio", 0.2f);

        DropLoot(iAnimatable);
    }

    private void PlayAudio()
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    private void DropLoot(IAnimatable iAnimatable)
    {
        if (iAnimatable is WorkerBehaviour)
        {
            ((WorkerBehaviour)iAnimatable).items.Add(GetComponent<Loot>().GetLoot());
        }
        else
        {
            InventoryController.instance.AddItems(GetComponents<Loot>()
                .Select(loot => loot.GetLoot())
                .Where(temp => temp != null)
                .ToList()
            );
        }
    }
}
