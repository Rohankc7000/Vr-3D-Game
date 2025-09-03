using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputTest : MonoBehaviour
{
	public InputActionAsset inputActions;
	private InputActionMap testActionMap;
	private InputAction testValue;
	private InputAction testPassThrough;

	private void OnEnable()
	{
		inputActions.Enable();
		testActionMap = inputActions.FindActionMap("Test");
		testValue = testActionMap.FindAction("TestValue");
		testPassThrough = testActionMap.FindAction("TestPassThrough");

		testValue.performed += OnTestValueActionStart;
		testValue.canceled += OnTestValueActionStop;
		testPassThrough.performed += OnTestPassThroughActionStart;
		testPassThrough.canceled += OnTestPassThroughActionStop;
	}

	private void OnDisable()
	{
		inputActions.Disable();

		testValue.performed -= OnTestValueActionStart;
		testValue.canceled -= OnTestValueActionStop;
		testPassThrough.performed -= OnTestPassThroughActionStart;
		testPassThrough.canceled -= OnTestPassThroughActionStop;
	}

	private void OnTestValueActionStart(InputAction.CallbackContext value)
	{
		float obtainValue = value.ReadValue<float>();
		Debug.Log("Start Test Value: " + obtainValue);
	}

	private void OnTestValueActionStop(InputAction.CallbackContext value)
	{
		float obtainValue = value.ReadValue<float>();
		Debug.Log("Stop Test Value: " + obtainValue);
	}

	private void OnTestPassThroughActionStart(InputAction.CallbackContext value)
	{
		float obtainValue = value.ReadValue<float>();
		Debug.Log("Start Test PassThrough: " + obtainValue);
	}

	private void OnTestPassThroughActionStop(InputAction.CallbackContext value)
	{
		float obtainValue = value.ReadValue<float>();
		Debug.Log("Stop Test PassThrough: " + obtainValue);
	}
}