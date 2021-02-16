using System.Collections.Generic;
using UnityEngine;

public class CustomTag : MonoBehaviour
{
    [SerializeField]
    private List<string> tags = new List<string>();

    public bool HasTag(string tag) => tags.Contains(tag);

    public IEnumerable<string> GetTags() => tags;

    public void Rename(int index, string tagName) => tags[index] = tagName;

    public string GetAtIndex(int index) => tags[index];

    public int Count
    {
        get => tags.Count;
    }

}
