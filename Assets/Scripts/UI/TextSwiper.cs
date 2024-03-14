using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Olimpik.Subscribe.UI
{

    [RequireComponent(typeof(RectTransform))]
    public class TextSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;

        public event Action<int> OnPageChange;

        [SerializeField] private float percentThreshold = 0.2f;
        [SerializeField] private float easing = 0.5f;

        public int CurrentPage
        {
            get => _CurrentPage;
            private set
            {
                _CurrentPage = value;
                OnPageChange?.Invoke(_CurrentPage);
            }
        }
        private int _CurrentPage = 1;

        private float panelLocation;
        private float width;
        private int totalPages;

        private Coroutine autoMove;
        private Coroutine smoothMove;

        async void Awake()
        {
            rectTransform = this.GetComponent<RectTransform>(); 
            await Task.Yield();
            await Task.Yield();
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            width = new Rect(corners[0], new Vector2(
                rectTransform.lossyScale.x * rectTransform.rect.size.x,
                rectTransform.lossyScale.y * rectTransform.rect.size.y)).width;

            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    totalPages++;
                }
            }

            panelLocation = transform.position.x;
        }

        private sbyte _wegwergrwgre;
        public void OnDrag(PointerEventData dataOD)
        {
              
               
            var differenceOD = dataOD.pressPosition.x - dataOD.position.x;
                
            transform.position = new Vector3(panelLocation - differenceOD, transform.position.y, transform.position.z);
        }
        public void OnEndDrag(PointerEventData data)
        {
            var percentage = (data.pressPosition.x - data.position.x) / width;
            if (Mathf.Abs(percentage) >= percentThreshold)
            {
                  
                var newLocation = panelLocation;
                if (percentage > 0 && CurrentPage < totalPages)
                {
                      
                    CurrentPage++;
                    newLocation -= width;
                }
                else if (percentage < 0 && CurrentPage > 1)
                {
                    CurrentPage--;
                    newLocation += width;
                }
                smoothMove = StartCoroutine(SmoothMove(newLocation));
                panelLocation = newLocation;
            }
            else
            {
                smoothMove = StartCoroutine(SmoothMove(panelLocation));
            }
        }
        private IEnumerator SmoothMove(float endposSM)
        {
               
            var startSM = transform.position;
            var endSM = new Vector3(endposSM, transform.position.y, transform.position.z);
               
               
               
               
            float percentSM = 0f;
              
              
            while (percentSM <= 1.0)
            {
                percentSM += Time.deltaTime / easing;
                transform.position = Vector3.Lerp(startSM, endSM, Mathf.SmoothStep(0f, 1f, percentSM));
                yield return null;
            }
            smoothMove = null;
        }
    }
}
