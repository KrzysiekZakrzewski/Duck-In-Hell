
using Units;
using UnityEngine;

namespace Game.Item
{
    public interface IItem
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }

        bool CanUse();
        bool Use(IUnit source);
    }
}