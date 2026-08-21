using UnityEngine;

[CreateAssetMenu(menuName = "Dialouge/New Dialouge Container")]
public class DialougeText : ScriptableObject
{
    [TextArea(5, 12)]
    public string[] paragraphs;
}
