using ArenaGame.UI.BaseUI;
using UnityEngine;
using UnityEngine.UI;

namespace Sprites
{
    public class SpritesLoader : MonoBehaviour
    {
        [SerializeField]
        private string spriteName;

        private void Awake()
        {
            var spriteAtlas = GetComponentInParent<IBaseUI>().SpriteAtlas;
            GetComponent<Image>().sprite = spriteAtlas.GetSprite(spriteName);
        }
    }
}