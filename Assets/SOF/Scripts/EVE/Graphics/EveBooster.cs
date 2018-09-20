using UnityEngine;

namespace EVE.Graphics
{
    /// <summary>
    /// Controlls a single booster object.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class EveBooster : MonoBehaviour
    {
        /// <summary>
        /// The intensity of the booster. 
        /// The expected range is 0 - 1, but nothing breaks if you go outside this range.
        /// </summary>
        public float boosterIntensity = 1.0f;
        /// <summary>
        /// The instensity of the booster flickering as a percentage of the boosterIntensity.
        /// </summary>
        public float flickerAmount = 0.1f;

        private LineRenderer _lineRenderer = null;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            float random = UnityEngine.Random.Range(-1.0f, 1.0f);
            var position = new Vector3[]
            {
            transform.position,
            transform.position + (transform.forward * (transform.lossyScale.z * -(boosterIntensity + random * boosterIntensity * flickerAmount)))
            };
            _lineRenderer.SetPositions(position);
            var width = transform.lossyScale.x + transform.lossyScale.y;
            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;
        }
    }
}
