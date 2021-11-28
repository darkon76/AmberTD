using DG.Tweening;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private bool _valid;
    public bool Valid
    {
        get => _valid;
        set
        {
            if (_valid == value)
            {
                return;
            }

            _valid = value;
            SetMaterials();
        }
    }

    public Material ValidMaterial;
    public Material InvalidMaterial;
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        SetMaterials();
    }

    private void Awake()
    {
        SetMaterials();
    }

    private void SetMaterials()
    {
        var material = Valid ? ValidMaterial : InvalidMaterial;
        var materials = new Material[_meshRenderer.materials.Length];
        

        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            materials[i] = material;
        }

        _meshRenderer.materials = materials;
    }
}
