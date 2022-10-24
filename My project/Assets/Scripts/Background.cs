using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace auttr
{
    public class Background : MonoBehaviour
    {
        Material materialGround;
        [SerializeField] float offsety;
        [SerializeField] float scrollSpeed = 5;


        private void Awake()
        {
            materialGround = GameObject.Find("Quad_�a�O").GetComponent<Renderer>().material;
        }

        public void MoveNext()
        {
            offsety += (Time.deltaTime * scrollSpeed) / 10f;
            materialGround.SetTextureOffset("_MainTex", new Vector2(0, offsety));
        }
        private void Update()
        {
            MoveNext();
        }

    }

}
