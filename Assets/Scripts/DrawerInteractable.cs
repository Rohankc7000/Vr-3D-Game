using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DrawerInteractable : XRGrabInteractable
{
	[SerializeField] private XRSocketInteractor keySocket;
	[SerializeField] private bool isLocked;
	[SerializeField] private Transform drawerTransform;
	[SerializeField] private GameObject KeyPointLight;

	private Transform parentTransform;
	private const string DEFAULT_LAYER = "Default";
	protected const string GRAB_LAYER = "Grab";

	[SerializeField] private bool isGrabbed;
	private Vector3 limitPosition;
	[SerializeField] protected Vector3 limitDistances = new Vector3(.1f, .1f, .1f);
	[SerializeField] float drawerLimitZ = 0.8f;
	private void Start()
	{
		if (keySocket != null)
		{
			keySocket.selectEntered.AddListener(OnDrawerUnlocked);
			keySocket.selectExited.AddListener(OnDrawerExited);
		}
		parentTransform = transform.parent;
		limitPosition = drawerTransform.localPosition;
	}

	private void Update()
	{
		if (isGrabbed && drawerTransform != null)
		{
			drawerTransform.localPosition = new Vector3(drawerTransform.localPosition.x, drawerTransform.localPosition.y, transform.localPosition.z);
			CheckLimtis();
		}
	}

	private void CheckLimtis()
	{
		if ((transform.localPosition.x >= limitPosition.x + limitDistances.x) ||
			(transform.localPosition.x <= limitPosition.x - limitDistances.x) ||
			(transform.localPosition.y >= limitPosition.y + limitDistances.y) ||
			(transform.localPosition.y <= limitPosition.y - limitDistances.y))
		{
			isGrabbed = false;
			ChangeLayerMask(DEFAULT_LAYER);
		}
		else if (drawerTransform.localPosition.z <= limitPosition.z - limitDistances.z)
		{
			isGrabbed = false;
			drawerTransform.localPosition = limitPosition;
			ChangeLayerMask(DEFAULT_LAYER);
		}
		else if (drawerTransform.localPosition.z >= drawerLimitZ + limitDistances.z)
		{
			isGrabbed = false;
			drawerTransform.localPosition = new Vector3(
				drawerTransform.localPosition.x,
				drawerTransform.localPosition.y,
				drawerLimitZ
			);
			ChangeLayerMask(DEFAULT_LAYER);
		}
	}

	private void ChangeLayerMask(string mask)
	{
		base.interactionLayers = InteractionLayerMask.GetMask(mask);
	}

	protected override void OnSelectEntered(SelectEnterEventArgs args)
	{
		base.OnSelectEntered(args);
		if (!isLocked)
		{
			transform.SetParent(parentTransform);
			isGrabbed = true;
		}
		else
		{
			ChangeLayerMask(DEFAULT_LAYER);
		}
		Debug.Log("*** Grabbedd. ***");
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);
		ChangeLayerMask(GRAB_LAYER);
		isGrabbed = false;
		transform.localPosition = drawerTransform.localPosition;
		Debug.Log("*** Released. ***");
	}

	private void OnDrawerExited(SelectExitEventArgs arg0)
	{
		isLocked = true;
		KeyPointLight.SetActive(true);
		Debug.Log("*** DRAWER LOCKED ***");
	}

	private void OnDrawerUnlocked(SelectEnterEventArgs arg0)
	{
		isLocked = false;
		KeyPointLight.SetActive(false);
		Debug.Log("*** DRAWER UNLOCKED ***");
	}
}
