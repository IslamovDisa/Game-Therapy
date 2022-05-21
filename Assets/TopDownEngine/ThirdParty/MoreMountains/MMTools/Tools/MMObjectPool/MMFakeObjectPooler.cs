using UnityEngine;

namespace MoreMountains.Tools
{
    public class MMFakeObjectPooler : MMSimpleObjectPooler
    {
        public override GameObject GetPooledGameObject()
        {
            var newGameObject = (GameObject)Instantiate(GameObjectToPool);
            newGameObject.gameObject.SetActive(false);
            return newGameObject;
        }
    }
}
