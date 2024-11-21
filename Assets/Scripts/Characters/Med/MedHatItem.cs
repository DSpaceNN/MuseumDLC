using UnityEngine;

public class MedHatItem : ItemOnCharacterMb
{
    [SerializeField] private GameObject _hairsGo;

    public override void OnEquip()
    {
        SkinnedMeshRenderer renderer = _hairsGo.GetComponent<SkinnedMeshRenderer>();
        renderer.SetBlendShapeWeight(0, 100f);
    }
}
