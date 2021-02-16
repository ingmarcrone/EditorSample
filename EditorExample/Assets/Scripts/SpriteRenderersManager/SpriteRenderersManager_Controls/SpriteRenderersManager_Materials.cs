using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteRenderersManager_Materials : SpriteRenderersManager_Property_Base
{
    [SerializeField] private Material[] Materials;

    public void GetGebruikteMaterials()
    {
        List<string> huidigGebruikteMateriaalNamen = new List<string>();
        foreach (SpriteRenderer spriteRenderer in SpriteRenderers())
            if (!huidigGebruikteMateriaalNamen.Contains(HelperSpriteRenderer.CleanName(spriteRenderer.sharedMaterial.name)))
                huidigGebruikteMateriaalNamen.Add(HelperSpriteRenderer.CleanName(spriteRenderer.sharedMaterial.name));

        Material[] beschikbareMaterials = Resources.LoadAll<Material>("Materials");

        List<Material> materials = new List<Material>();
        foreach (string huidigGebruikteMateriaalNaam in huidigGebruikteMateriaalNamen.OrderBy(x => x))
            foreach (var beschikbaarMaterial in beschikbareMaterials)
                if (beschikbaarMaterial.name.Contains(huidigGebruikteMateriaalNaam))
                    materials.Add(beschikbaarMaterial);

        Materials = materials.ToArray();
    }

    public void SetMaterials()
    {
        if (Materials == null || Materials.Length == 0)
            return;

        if (Materials.Length == 1)
            foreach (SpriteRenderer spriteRenderer in SpriteRenderers())
                spriteRenderer.material = Materials[0];
        else
            foreach (SpriteRenderer spriteRenderer in SpriteRenderers())
                spriteRenderer.material = MaterialFromIndex(Materials, SetMaterialIndexes(Materials.Length));

    }

    private static Material MaterialFromIndex(Material[] materials, Dictionary<int, float> materialIndexes)
    {
        float random = Random.Range(0f, 1f);
        int index = materialIndexes.First(x => random >= x.Value).Key;
        Material material = materials[index];
        return material;
    }

    private Dictionary<int, float> SetMaterialIndexes(int aantalMaterials)
    {
        Dictionary<int, float> materialIndexes = new Dictionary<int, float>();
        float optelWaarde = 1f / aantalMaterials;
        for (int i = 0; i < aantalMaterials; i++)
            materialIndexes.Add(i, (aantalMaterials - 1 - i) * optelWaarde);
        return materialIndexes;
    }

}
