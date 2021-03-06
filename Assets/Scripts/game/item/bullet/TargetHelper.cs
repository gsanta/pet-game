using System.Collections.Generic;
using game.character;
using UnityEngine;

namespace game.item.bullet
{
    public static class TargetHelper
    {
        public static ICharacter GetClosest(List<ICharacter> characters, Vector3 position, float maxRange)
        {
            ICharacter closest = null;

            foreach (var target in characters)
            {
                var targetPos = target.GetPosition();
                if (!(Vector3.Distance(position, targetPos) <= maxRange)) continue;
                if (closest == null)
                {
                    closest = target;
                }
                else
                {
                    if (Vector3.Distance(position, targetPos) <=
                        Vector3.Distance(position, closest.GetPosition()))
                    {
                        closest = target;
                    }
                }
            }

            return closest;
        }
    }
}