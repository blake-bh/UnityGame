using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class Door : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite spriteOpened;
        public Sprite spriteClosed;

        public GameObject player;

        public Transform t;
        


        private Animator Animator
        {
            get
            {
                if (animator == null ) animator = GetComponent<Animator>();
                return animator;
            }
        }
        private Animator animator;

        [ExposeProperty]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;

                if (Application.isPlaying)
                {
                    Animator.SetBool("IsOpened", isOpened);
                }
                else
                {
                    if(spriteRenderer) spriteRenderer.sprite = isOpened ? spriteOpened : spriteClosed;
                }
            }
        }
        [SerializeField,HideInInspector]
        private bool isOpened;

        private void Start()
        {
            Animator.Play(isOpened ? "Opened" : "Closed");
            IsOpened = isOpened;
           t = player.GetComponent<Transform>();
        }


        public void Open()
        {
            IsOpened = true;
        }

        public void Close()
        {
            IsOpened = false;
        }
        public void Update()
        {
            if(Input.GetButtonDown("OpenDoor") && t.position.x > -5 && t.position.x < -3.5)
            OpenDoor();
        }

        private void OpenDoor(){
            Debug.Log("Button Pressed");
            IsOpened = true;
            Debug.Log(IsOpened);
            StartCoroutine(closeDoor());
        }
        private IEnumerator closeDoor(){
            yield return new WaitForSeconds(2f);
            IsOpened = false;
        }
    }
}
