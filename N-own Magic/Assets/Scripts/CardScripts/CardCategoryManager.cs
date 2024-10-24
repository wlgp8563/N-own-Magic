using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Category", menuName = "TCG/CardCategory")]
public class CardCategoryManager : ScriptableObject
{
    public string categoryName;
    public List<string> cardNames;
    public List<Sprite> cardImages;
    public List<string> descriptions;
    public List<int> lightEnergies;
}
