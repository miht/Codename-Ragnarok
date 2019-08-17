using Modules.UI;

public class Equippable : Item
{
    public enum EquippableTypes {
        Armor,
        Weapon
    }

    protected EquippableTypes _equippableType;

    void Start() {
        base._itemType = ItemTypes.Equippable;
    }
}
