using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SpriteRenderersManager_Property_Base : SpriteRenderersManager_Base
{
    [Tooltip("Als 'this' of een parent, een naam heeft waar een van deze strings in voorkomt, wordt deze genegeerd bij het uitvoeren van de opdracht.(Sowieso worden ALLE methodes Alleen uitgevoerd op GameObject's met een SpriteRenderer))")]
    [SerializeField]
    private List<string> NameIgnoreList = new List<string>();

    [SerializeField]
    private List<string> NameExclusiveList = new List<string>();

    [SerializeField]
    private List<string> TagIgnoreList = new List<string>();

    [SerializeField]
    private List<string> TagExclusiveList = new List<string>();

    public string Title = "DEAULT TITLE";


    public bool IsIncluissiefInActieve = true;

    public SpriteRenderer[] SpriteRenderers()
    {
        List<SpriteRenderer> spriteRenderersToReturn = new List<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in GetSpriteRenderers(IsIncluissiefInActieve))
            if (IsSpriteRendererGoedGekeurd(spriteRenderer))
                spriteRenderersToReturn.Add(spriteRenderer);

        return spriteRenderersToReturn.ToArray();
    }

    private bool IsSpriteRendererGoedGekeurd(SpriteRenderer spriteRenderer)
    {
        List<string> tags = GetTags(spriteRenderer);

        if (!Is_TagExclusiveList_Goedgekeurd(tags))
            return false;

        if (!Is_NameExclusiveList_Goedgekeurd(spriteRenderer))
            return false;

        if (!Is_TagIgnoreList_Goedgekeurd(tags))
            return false;

        if (!Is_NameIgnoreList_Goedgekeurd(spriteRenderer))
            return false;

        return true;
    }

    private bool Is_NameIgnoreList_Goedgekeurd(SpriteRenderer spriteRenderer)
    {
        bool isNameIgnoreList = false;

        if (NameIgnoreList == null || NameIgnoreList.Count == 0)
            isNameIgnoreList = true;
        else
            isNameIgnoreList = !NameIgnoreList.Any(x => spriteRenderer.name.ToLower().Trim().Contains(x.ToLower().Trim()));
        return isNameIgnoreList;
    }

    private bool Is_TagIgnoreList_Goedgekeurd(List<string> tags)
    {
        bool isTagIgnoreList = false;

        if (TagIgnoreList == null || TagIgnoreList.Count == 0)
            isTagIgnoreList = true;
        else
            isTagIgnoreList = !tags.Any(t => TagIgnoreList.Any(tel => t == tel));
        return isTagIgnoreList;
    }

    /// <summary>
    /// Goedgekeurd betekend: er zijn geen gewenste naam onderdelen opgegeven, of, een van de gewenste naam onderdelen is gevonden in de SpriteRenderer naam.
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <returns></returns>
    private bool Is_NameExclusiveList_Goedgekeurd(SpriteRenderer spriteRenderer)
    {
        bool isNameExclusiveList = false;

        if (NameExclusiveList == null || NameExclusiveList.Count == 0)
            isNameExclusiveList = true;
        else
            isNameExclusiveList = NameExclusiveList.Any(x => spriteRenderer.name.ToLower().Trim().Contains(x.ToLower().Trim()));
        return isNameExclusiveList;
    }

    /// <summary>
    /// Goedgekeurd betekend: er zijn geen gewenste tags opgegeven, of, een van de gewenste tags is gevonden in de SpriteRenderer tags.
    /// </summary>
    /// <param name="tags"></param>
    /// <returns></returns>
    private bool Is_TagExclusiveList_Goedgekeurd(List<string> tags)
    {
        bool isTagExclusiveList = false;

        if (TagExclusiveList == null || TagExclusiveList.Count == 0)
            isTagExclusiveList = true;
        else
            isTagExclusiveList = tags.Any(t => TagExclusiveList.Any(tel => t == tel));

        return isTagExclusiveList;
    }

    private static List<string> GetTags(SpriteRenderer spriteRenderer)
    {
        List<string> tags = new List<string>();

        CustomTag customTag = spriteRenderer.GetComponent<CustomTag>();
        if (customTag != null && customTag.GetTags().Count() > 0)
            tags.AddRange(customTag.GetTags());

        if (spriteRenderer.tag != null || spriteRenderer.tag != "Untagged")
            tags.Add(spriteRenderer.tag);
        return tags;
    }

    public string[] NamesOfObject()
    {
        SpriteRenderer[] spriteRenderers = GetSpriteRenderers();

        List<string> namen = new List<string>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (!namen.Contains(spriteRenderer.name))
                namen.Add(spriteRenderer.name);

            string[] splits = Regex.Split(spriteRenderer.name, @"(?<!^)(?=[A-Z])");

            foreach (string split in splits)
                if (!namen.Contains(split))
                    namen.Add(split);

            string[] splits2 = Regex.Split(Regex.Replace(spriteRenderer.name, @"[\d-]", string.Empty), @"(?<!^)(?=[A-Z])");
            foreach (string split in splits2)
                if (!namen.Contains(split))
                    namen.Add(split);

        }

        return namen.OrderBy(x => x).ToArray();
    }

}
