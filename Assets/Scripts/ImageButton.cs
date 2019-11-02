using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Image))]
    public class ImageButton : MonoBehaviour
    {
        [Tooltip("Command id")]
        public string id;

        [Tooltip("button features")]
        public Sprite unselected;
        public Sprite selected;
        public Sprite hovered;

        private Image _image;
        private Sprite _oldSprite;

        void Start()
        {
            _image = GetComponent<Image>();
            _image.sprite = unselected;
        }

        public void OnHover()
        {
            _oldSprite = _image.sprite;
            _image.sprite = hovered;
        }

        public void OnNormal()
        {
            _image.sprite = _oldSprite;
        }

        public void OnSelect()
        {
            _image.sprite = selected;
            _oldSprite = selected;
        }

        public void OnUnselect()
        {
            _image.sprite = unselected;
            _oldSprite = unselected;
        }
    }
}
