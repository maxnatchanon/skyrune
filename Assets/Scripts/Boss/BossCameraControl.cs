using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossCameraControl : MonoBehaviour
{
	float amp = 2f;
	float currentDuration = 1f;

	public float duration = 0.5f;
	public CinemachineVirtualCamera cam;
	CinemachineBasicMultiChannelPerlin noise;

	void Start()
	{
		currentDuration = duration;
		noise = cam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
	}

	void Update()
	{
		noise.m_AmplitudeGain = (currentDuration <= duration) ? amp : 0f;
		currentDuration += Time.deltaTime;
	}

	public void ShakeCamera()
	{
		currentDuration = 0f;
	}
}
