//----------------------------------------------
//          Realistic Tank Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RTCTankController)), CanEditMultipleObjects]
public class RCCEditor : Editor {
	
	RTCTankController tankController;

	static bool firstInit = false;
	
	Texture2D wheelIcon;
	Texture2D configIcon;
	Texture2D soundIcon;
	Texture2D smokeIcon;
	
	bool WheelSettings;
	bool Configurations;
	bool SoundSettings;
	bool SmokeSettings;
	
	Color defBackgroundColor;
	
	[MenuItem("Tools/BoneCracker Games/Realistic Tank Controller/Add Main Controller To Tank")]
	static void CreateBehavior(){
		
		if(!Selection.activeGameObject.GetComponent<RTCTankController>()){
			
			Selection.activeGameObject.AddComponent<RTCTankController>();
			
			Selection.activeGameObject.GetComponent<Rigidbody>().mass = 10000f;
			Selection.activeGameObject.GetComponent<Rigidbody>().angularDrag = .5f;
			Selection.activeGameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
			firstInit = true;
			
		}else{
			
			EditorUtility.DisplayDialog("Your Gameobject Already Has Tank Controller", "Your Gameobject Already Has Tank Controller", "Ok");
			
		}
		
	}

	[MenuItem("Tools/BoneCracker Games/Realistic Tank Controller/Add Gun Controller To Main Gun")]
	static void CreateGunBehavior(){
		
		if(!Selection.activeGameObject.GetComponent<RTCTankGunController>()){
			
			Selection.activeGameObject.AddComponent<RTCTankGunController>();
			
			Selection.activeGameObject.GetComponent<Rigidbody>().mass = 1f;
			Selection.activeGameObject.GetComponent<Rigidbody>().angularDrag = 10f;
			Selection.activeGameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

			Selection.activeGameObject.GetComponent<HingeJoint>().axis = Vector3.up;
			Selection.activeGameObject.GetComponent<HingeJoint>().useSpring = true;
			JointSpring js = new JointSpring();
			js.damper = 50;
			Selection.activeGameObject.GetComponent<HingeJoint>().spring = js;
			
		}else{
			
			EditorUtility.DisplayDialog("Your Main Gun Already Has Gun Controller", "Your Main Gun Already Has Gun Controller", "Ok");
			
		}
		
	}
	
	void Awake(){
		
		wheelIcon = Resources.Load("WheelIcon", typeof(Texture2D)) as Texture2D;
		configIcon = Resources.Load("ConfigIcon", typeof(Texture2D)) as Texture2D;
		soundIcon = Resources.Load("SoundIcon", typeof(Texture2D)) as Texture2D;
		smokeIcon = Resources.Load("SmokeIcon", typeof(Texture2D)) as Texture2D;
		
	}
	
	public override void OnInspectorGUI () {
		
		serializedObject.Update();
		
		tankController = (RTCTankController)target;
		defBackgroundColor = GUI.backgroundColor;
		
		if(firstInit){
			SetDefaultSettings();
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		
		if(WheelSettings)
			GUI.backgroundColor = Color.gray;
		else GUI.backgroundColor = defBackgroundColor;
		
		if(GUILayout.Button(wheelIcon))
			if(!WheelSettings)	WheelSettings = true;
		else WheelSettings = false;
		
		if(Configurations)
			GUI.backgroundColor = Color.gray;
		else GUI.backgroundColor = defBackgroundColor;
		
		if(GUILayout.Button(configIcon))
			if(!Configurations)	Configurations = true;
		else Configurations = false;
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		
		if(SoundSettings)
			GUI.backgroundColor = Color.gray;
		else GUI.backgroundColor = defBackgroundColor;
		
		if(GUILayout.Button(soundIcon))
			if(!SoundSettings)	SoundSettings = true;
		else SoundSettings = false;
		
		if(SmokeSettings)
			GUI.backgroundColor = Color.gray;
		else GUI.backgroundColor = defBackgroundColor;
		
		if(GUILayout.Button(smokeIcon))
			if(!SmokeSettings)	SmokeSettings = true;
		else SmokeSettings = false;
		
		GUI.backgroundColor = defBackgroundColor;
		EditorGUILayout.EndHorizontal();
		
		if(WheelSettings){
			
			EditorGUILayout.Space();
			GUI.color = Color.cyan;
			EditorGUILayout.HelpBox("Wheel Settings", MessageType.None);
			GUI.color = defBackgroundColor;
			EditorGUILayout.Space();


			
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelTransform_L"), new GUIContent("Wheels Left", "Select all left wheels of your tank."), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelTransform_R"), new GUIContent("Wheels Right", "Select all right wheels of your tank."), true);
			EditorGUILayout.Space();

			if(GUILayout.Button("Create Wheel Colliders")){
				
				WheelCollider[] wheelColliders = tankController.gameObject.GetComponentsInChildren<WheelCollider>();
				
				if(wheelColliders.Length >= 1){
					foreach(WheelCollider wc in wheelColliders)
						Destroy(wc);
					tankController.CreateWheelColliders();
				}else{
					tankController.CreateWheelColliders();
				}
				
			}

			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("trackBoneTransform_L"), new GUIContent("Track Bones Left", "Select all left bones of your track."), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("trackBoneTransform_R"), new GUIContent("Track Bones Right", "Select all right bones of your track."), true);
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("uselessGearTransform_L"), new GUIContent("Useless Gears Left", "Select all left useless gears of your tank."), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("uselessGearTransform_R"), new GUIContent("Useless Gears Right", "Select all right useless gears of your tank."), true);
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelColliders_L"), new GUIContent("Left Wheel Colliders", "Select all left wheel colliders of your tank."), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelColliders_R"), new GUIContent("Right Wheel Colliders", "Select all right wheel colliders of your tank."), true);
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("leftTrackMesh"), new GUIContent("Track Left", "Select left track of your tank."), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("rightTrackMesh"), new GUIContent("Track Right", "Select right track of your tank."), false);
			EditorGUILayout.Space();

		}
		
		if(Configurations){
			
			EditorGUILayout.Space();
			GUI.color = Color.cyan;
			EditorGUILayout.HelpBox("Configurations", MessageType.None);
			GUI.color = defBackgroundColor;
			EditorGUILayout.Space();
			
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("canControl"), new GUIContent("Can Be Controllable Now", "Enables/Disables controlling the vehicle."));
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("runEngineAtAwake"), new GUIContent("Engine Is Running At Awake", "Engine is running defaultly at start of the game."));
			EditorGUILayout.LabelField("Engine Is Running Now", tankController.engineRunning.ToString());
			EditorGUILayout.PropertyField(serializedObject.FindProperty("engineRunning"), new GUIContent("Engine Running Now", "Engine is running currently."));
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("COM"), new GUIContent("Center Of Mass (''COM'')", "Center of Mass of the vehicle. Usually, COM is below around front driver seat."));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("engineTorque"), new GUIContent("Maximum Engine Torque"), false);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("steerTorque"), new GUIContent("Steering Torque"), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("minEngineRPM"), new GUIContent("Lowest Engine RPM (While Running Engine)"), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxEngineRPM"), new GUIContent("Highest Engine RPM"), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("brakeTorque"), new GUIContent("Maximum Brake Torque"), false);
			EditorGUILayout.Space();
			//EditorGUILayout.PropertyField(serializedObject.FindProperty("engineTorqueCurve"), true);
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("trackOffset"), new GUIContent("Track Offset", "Height offset of your tracks."), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("trackScrollSpeedMultiplier"), new GUIContent("Track Scroll Speed", "Track scroll speed multiplier."), false);
			EditorGUILayout.Space();

		}
		
		if(SoundSettings){
			
			EditorGUILayout.Space();
			GUI.color = Color.cyan;
			EditorGUILayout.HelpBox("Sound Settings", MessageType.None);
			GUI.color = defBackgroundColor;
			
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("engineStartUpAudioClip"), new GUIContent("Starting Engine Sound"), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("engineIdleAudioClip"), new GUIContent("Idle Engine Sound"), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("engineRunningAudioClip"), new GUIContent("Heavy Engine Sound"), false);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("brakeClip"), new GUIContent("Brake Sound"), false);
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Slider(serializedObject.FindProperty("minEngineSoundPitch"), .25f, 1f);
			EditorGUILayout.Slider(serializedObject.FindProperty("maxEngineSoundPitch"), 1f, 2f);
			EditorGUILayout.Slider(serializedObject.FindProperty("minEngineSoundVolume"), 0f, 1f);
			EditorGUILayout.Slider(serializedObject.FindProperty("maxEngineSoundVolume"), 0f, 1f);
			EditorGUILayout.Slider(serializedObject.FindProperty("maxBrakeSoundVolume"), 0f, 1f);
			EditorGUILayout.Space();
			
		}
		
		if(SmokeSettings){
			
			EditorGUILayout.Space();
			GUI.color = Color.cyan;
			EditorGUILayout.HelpBox("Smoke Settings", MessageType.None);
			GUI.color = defBackgroundColor;
			EditorGUILayout.Space();
			
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelSlip"), new GUIContent("Wheel Smoke"), false);
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("exhaustSmoke"), new GUIContent("Exhaust Smoke"), false);
			EditorGUILayout.Space();

		}

		if(tankController.GetComponentInChildren<RTCTankGunController>())
			tankController.GetComponentInChildren<RTCTankGunController>().GetComponent<HingeJoint>().connectedBody = tankController.GetComponent<Rigidbody>();
		
		serializedObject.ApplyModifiedProperties();
		
		if(GUI.changed && !EditorApplication.isPlaying){
			EngineCurveInit();
		}
		
	}
	
	void EngineCurveInit (){

		tankController.engineTorqueCurve = new AnimationCurve(new Keyframe(0, 1));
		tankController.engineTorqueCurve.AddKey(new Keyframe(tankController.maxSpeed, 0));
		tankController.engineTorqueCurve.postWrapMode = WrapMode.Clamp;
		
	}
	
	void SetDefaultSettings(){

		GameObject COM = new GameObject("COM");
		COM.transform.parent = tankController.transform;
		COM.transform.localPosition = Vector3.zero;
		COM.transform.localScale = Vector3.one;
		COM.transform.rotation = tankController.transform.rotation;
		tankController.COM = COM.transform;

		firstInit = false;
		
	}
	
}
