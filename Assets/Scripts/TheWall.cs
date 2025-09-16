using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TheWall : MonoBehaviour
{
	[SerializeField] private XRSocketInteractor wallSocket;
	[SerializeField] private List<GameObject> wallCubes;

	private void Start()
	{
		if (wallSocket != null)
		{
			wallSocket.selectEntered.AddListener(OnSocketEnter);
			wallSocket.selectExited.AddListener(OnSocketExited);
		}
	}

	private void OnSocketExited(SelectExitEventArgs arg0)
	{

	}

	private void OnSocketEnter(SelectEnterEventArgs arg0)
	{
		foreach (GameObject wall in wallCubes)
		{
			if (wall != null)
			{
				Rigidbody rb = wall.GetComponent<Rigidbody>();
				rb.isKinematic = false;
			}
		}
	}
}
