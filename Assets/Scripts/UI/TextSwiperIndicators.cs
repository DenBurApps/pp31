using System;
using UnityEngine;
using UnityEngine.UI;

namespace Olimpik.Subscribe.UI
{
    internal class TextSwiperIndicators : MonoBehaviour
    {
        [SerializeField] private TextSwiper textSwiper;
        [SerializeField] private Color enabledColor;
        [SerializeField] private Color disabledColor;
        void Awake()
        {
            textSwiper.OnPageChange += OnPageChange;
            OnPageChange(1);
        }
        private void OnPageChange(int pageNumber)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                var image = child.GetComponent<Image>();

                if(i == pageNumber - 1)
                {
                    image.color = enabledColor;
                }
                else
                {
                    image.color = disabledColor;
                }
            }
        }
    }
}
