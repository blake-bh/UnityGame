using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class Chest : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        [ExposeProperty]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                animator.SetBool("IsOpened", isOpened);
            }
        }
        [SerializeField, HideInInspector]
        private bool isOpened;

        public void Open()
        {
            IsOpened = true;
        }

        public void Close()
        {
            IsOpened = false;
        }
    }
}
