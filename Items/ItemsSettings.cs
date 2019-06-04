using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Settings")]
    public class ItemsSettings : ScriptableObject
    {
        public List<CountryItem> countries = new List<CountryItem>();
    }
}