using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagTester : MonoBehaviour
{
    [SerializeField]
    private List<TagInit> _tags;
    public List<TagInit> All => _tags;

    public bool HasTag(TagInit t)
    {
        return _tags.Contains(t);
    }

    public bool HasTag(string TagName){
        return _tags.Exists(t => t.Name.Equals(TagName, System.StringComparison.InvariantCultureIgnoreCase));
    }

}
