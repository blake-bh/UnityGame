using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class Switch : MonoBehaviour
    {
        public Door target;
        [Space]
        public SpriteRenderer spriteRenderer;
        public Sprite spriteOpened;
        public Sprite spriteClosed;

        private Animator Animator
        {
            get
            {
                if (animator == null) animator = GetComponent<Animator>();
                return animator;
            }
        }
        private Animator animator;

        private void Start()
        {
            IsOn = isOn;
            Animator.SetBool("IsOn", isOn);
        }

        [ExposeProperty]
        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;
                if (target) target.IsOpened = isOn;

                if (Application.isPlaying == false) return;
                Animator.SetBool("IsOn", isOn);
            }
        }
        [SerializeField, HideInInspector]
        private bool isOn;

        public void TurnOn()
        {
            IsOn = true;
        }

        public void TurnOff()
        {
            IsOn = false;
        }
    }
}
