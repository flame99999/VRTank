using UnityEngine;
using System.Collections;
using System;

[System.Serializable] 
public struct MyItemImage
{
	public UnityEngine.UI.Image panel;
	public UnityEngine.UI.Image image;
	public UnityEngine.RectTransform rect;
}

public class CameraPointCheck : MonoBehaviour {


	public Transform m_Camera;
	public Transform m_EyePoint;
	public AudioSource m_audioSource;
	public GameObject m_explosion;

	[SerializeField] public MyItemImage[] m_ItemImage = new MyItemImage[6];

	public UnityEngine.UI.Text m_debugText;

	private float m_RayLength = 10000f;
	private float m_rectWidth = 0;
	private int m_currentIndex = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < 6; ++i) {
			m_ItemImage [i].image.color = Color.white;
			m_ItemImage [i].panel.color = Color.white;

			if (m_ItemImage [i].rect != null) {
				m_ItemImage [i].rect.sizeDelta = new Vector2(0, 200);
			}

		} 

		Ray ray = new Ray(m_Camera.position, m_Camera.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, m_RayLength)) {
			m_EyePoint.position = hit.point;

			m_debugText.text = hit.collider.gameObject.name;

			int len = hit.collider.gameObject.name.Length;
			if (len==1) {
				
				int index = int.Parse (hit.collider.gameObject.name);
				if (m_currentIndex != index) {
					m_rectWidth = 0.0f;
				}

				m_currentIndex = index;
				if (index == 5) {
					m_ItemImage [index].image.color = Color.red;
				} else {
					m_ItemImage [index].image.color = Color.black;
				}

				m_ItemImage [index].panel.color = Color.white;

				if (m_ItemImage [index].rect != null) {
					m_rectWidth += 1f;

					if (m_rectWidth == 199.0f) {
						m_audioSource.Play ();
					}

					if (m_rectWidth >= 200.0f) {
						m_rectWidth = 200.0f;

						if(m_audioSource.isPlaying && m_explosion.activeSelf==false){
							if (m_audioSource.time >= 3.0f) {
								m_explosion.transform.position = hit.point;
								m_explosion.SetActive (true);
							}
							//m_debugText.text = m_audioSource.time.ToString ();
						}


						if(m_audioSource.isPlaying==false){
							switch (index) {
							case 2:
								UIEvent.StartScene ("Demo Scene");
								break;
							case 4:
								break;
							case 5:
								Application.Quit ();
								break;
							}
						}
					}

					m_ItemImage [index].rect.sizeDelta = new Vector2(m_rectWidth, 200);
				}

				//Vector3 v = m_ItemImage [index].rect.right;
				//int x = 0;
				//x++;
			}else{

				m_rectWidth = 0;
			}

		} else {
			float d = Vector3.Distance (m_EyePoint.position, m_Camera.position);
			m_EyePoint.position = m_Camera.position + m_Camera.forward * d;
		}


	}
}
