using UnityEngine;

namespace EVE.Graphics
{
    /// <summary>
    /// Controlls a single sprite object.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshRenderer))]
    public class EveSprite : MonoBehaviour
    {
        /// <summary>
        /// The blink rate - if not positive value, sprite doesn't blink
        /// </summary>
        public float blinkRate = 1.0f;
        /// <summary>
        /// Offset phase for the blinking.
        /// </summary>
        public float blinkPhase = 0.0f;
        /// <summary>
        /// Minimum size.
        /// </summary>
        public float minScale = 1.0f;
        /// <summary>
        /// Maximum size.
        /// </summary>
        public float maxScale = 4.0f;
        /// <summary>
        /// Camera based scaling.
        /// </summary>
        public float falloff = 0.0f;
        /// <summary>
        /// Color
        /// </summary>
        public Color color = Color.white;

        private AnimationCurve curve;

        [SerializeField]
        private MaterialPropertyBlock _materialProperties;

        private float _timer = 0.0f;

        private void Start()
        {
            _materialProperties = new MaterialPropertyBlock();
            _timer = -blinkPhase;
            _UpdateCurve();
        }

        private void _UpdateCurve()
        {
            if (blinkRate > 0)
            {
                var blinkTime = 1 / blinkRate;
                var keys = new Keyframe[]
                {
                    new Keyframe(0.0f, minScale),
                    new Keyframe(blinkTime * 0.9f, minScale),
                    new Keyframe(blinkTime * 0.95f, maxScale),
                    new Keyframe(blinkTime, minScale)
                };
                curve = new AnimationCurve(keys);
            }
        }

        private void _UpdateScale()
        {
            var blinkTime = 1.0f / blinkRate;
            if (blinkRate > 0 && _timer > 0)
            {
                float s = curve.Evaluate(_timer);
                transform.localScale = new Vector3(s, s, s);
                if (_timer > blinkTime)
                {
                    _timer -= blinkTime;
                }
            }
            else
            {
                transform.localScale = new Vector3(minScale, minScale, minScale);
            }
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Application.isEditor)
                _UpdateCurve();
#endif
            _timer += Time.deltaTime;
            _UpdateScale();

            if (_materialProperties == null)
                _materialProperties = new MaterialPropertyBlock();

            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.GetPropertyBlock(_materialProperties);

            _materialProperties.SetColor("_TintColor", color);
            _materialProperties.SetFloat("_ScaleX", transform.lossyScale.x * 2);
            _materialProperties.SetFloat("_ScaleY", transform.lossyScale.y * 2);

            meshRenderer.SetPropertyBlock(_materialProperties);
        }
    }
}
