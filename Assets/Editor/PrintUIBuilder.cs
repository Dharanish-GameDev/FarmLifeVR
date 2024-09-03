using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using FARMLIFEVR.CROPS.MAIZE;

public class PrintUIBuilder : EditorWindow
{
	#region Private Variables

	private ObjectField maizeFieldStateMachine;
	private Button printButton;

	#endregion

	#region Properties



	#endregion

	#region LifeCycle Methods

	private void Awake()
	{

	}
	private void Start()
	{

	}
	private void Update()
	{

	}

	#endregion

	#region Private Methods


	#endregion

	#region Public Methods

	[MenuItem("Tools/PrintEditor")]
	public static void OpenEditorWindow()
	{
		PrintUIBuilder builder = GetWindow<PrintUIBuilder>();
		builder.titleContent = new GUIContent("PrintEditor");
		builder.minSize = new Vector2(300, 300);
    }

    private void CreateGUI()
    {
		VisualElement root = rootVisualElement;
		VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI_Builder/NewUXMLTemplate.uxml");
		VisualElement tree = visualTree.Instantiate();
		root.Add(tree);

		printButton = root.Q<Button>("Print");
		maizeFieldStateMachine = root.Q<ObjectField>("MaizeField");

		printButton.clicked += OnPrintButtonClicked;
    }

	private void OnPrintButtonClicked()
	{
		MaizeFeildStateMachine maizeStateMachine = maizeFieldStateMachine.value as MaizeFeildStateMachine;
		if (maizeStateMachine != null)
		{
			
		}
		else
		{
			Debug.Log("MaizeField Ref is Null");
		}
	}

    #endregion
}
