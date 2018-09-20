using UnityEditor;

namespace EVE.SOF
{
    [CustomPropertyDrawer(typeof(HullStore))]
    public class HullStorePropertyDrawer : SerializableDictionaryPropertyDrawer { }

    [CustomPropertyDrawer(typeof(FactionStore))]
    public class FactionStorePropertyDrawer : SerializableDictionaryPropertyDrawer { }

    [CustomPropertyDrawer(typeof(RaceStore))]
    public class RaceStorePropertyDrawer : SerializableDictionaryPropertyDrawer { }

    [CustomPropertyDrawer(typeof(MaterialStore))]
    public class MaterialStorePropertyDrawer : SerializableDictionaryPropertyDrawer { }
}