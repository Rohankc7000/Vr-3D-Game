using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class XRButtonInteractable : XRSimpleInteractable
{
	[SerializeField] private Color[] buttonColors = new Color[4];
	[SerializeField] protected Image buttonImage;

	private Color normalColor;
	private Color highlightedColor;
	private Color pressedColor;
	private Color selectedColor;

	private bool isPressed;

	private void Start()
	{
		normalColor = buttonColors[0];
		highlightedColor = buttonColors[1];
		pressedColor = buttonColors[2];
		selectedColor = buttonColors[3];
		buttonImage = GetComponent<Image>();
		buttonImage.color = normalColor;
	}

	protected override void OnHoverEntered(HoverEnterEventArgs args)
	{
		base.OnHoverEntered(args);
		isPressed = false;
		buttonImage.color = highlightedColor;
	}

	protected override void OnHoverExited(HoverExitEventArgs args)
	{
		base.OnHoverExited(args);
		if (!isPressed)
		{
			buttonImage.color = normalColor;
		}
	}

	protected override void OnSelectEntered(SelectEnterEventArgs args)
	{
		base.OnSelectEntered(args);
		isPressed = true;
		buttonImage.color = pressedColor;
		Debug.Log("Selected...");
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);
		isPressed = false;
		buttonImage.color = selectedColor;
	}
}
