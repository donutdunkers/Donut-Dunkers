using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public static class Ext
{ 
	public static bool InLayerMask(this int layer, LayerMask layermask)
	{ 
		return layermask == (layermask | 1 << layer);
	}

	public static LayerMask Exclude(this LayerMask layerMask, int layer)
	{ 
		return layerMask & ~(1 << layer);
	}

	public static LayerMask Exclude(this LayerMask layerMask, string layer)
	{ 
		return layerMask & ~(1 << LayerMask.NameToLayer(layer));
	}

	public static float GetJumpForce(float height, float gravity)
	{ 
		return Mathf.Sqrt(Mathf.Abs(gravity * height * 2f));
	}

	public static float GetJumpHeight(float force, float gravity)
	{ 
		return force * force / 2f / gravity;
	}

	public static Vector3 GetLowerPoint(this CapsuleCollider col, float offsetMultiplier = 1f)
	{ 
		Transform transform = col.transform;
		return col.transform.position + transform.TransformDirection(col.center) - transform.up * ((0.5f * col.height - col.radius) * offsetMultiplier);
	}

	public static Vector3 GetLowerPoint(this CapsuleCollider col, Rigidbody rb, float offsetMultiplier = 1f)
	{ 
		Transform transform = rb.transform;
		return rb.position + transform.TransformDirection(col.center) - transform.up * ((0.5f * col.height - col.radius) * offsetMultiplier);
	}

	public static Vector3 GetCapsuleLowerPoint(Vector3 p, Vector3 up, CapsuleCollider col, float offsetMultiplier = 1f)
	{ 
		return p + up * col.center.y - up * ((0.5f * col.height - col.radius) * offsetMultiplier);
	}

	public static Vector3 GetUpperPoint(this CapsuleCollider col, float offsetMultiplier = 1f)
	{ 
		Transform transform = col.transform;
		return col.transform.position + transform.TransformDirection(col.center) + transform.up * ((0.5f * col.height - col.radius) * offsetMultiplier);
	}

	public static Vector3 GetUpperPoint(this CapsuleCollider col, Rigidbody rb, float offsetMultiplier = 1f)
	{ 
		Transform transform = rb.transform;
		return rb.position + transform.TransformDirection(col.center) + transform.up * ((0.5f * col.height - col.radius) * offsetMultiplier);
	}

	public static Vector3 GetCapsuleUpperPoint(Vector3 p, Vector3 up, CapsuleCollider col, float offsetMultiplier = 1f)
	{ 
		return p + up * col.center.y + up * ((0.5f * col.height - col.radius) * offsetMultiplier);
	}

	public static float Flerp(float source, float target, float smoothing, float dt)
	{ 
		return Mathf.Lerp(source, target, 1f - Mathf.Pow(smoothing, dt));
	}

	public static float Flerp(float source, float target, float x)
	{ 
		return Mathf.Lerp(source, target, 1f - Mathf.Exp(-x * Time.deltaTime));
	}

	public static float FlerpAngle(float source, float target, float x)
	{ 
		return Mathf.LerpAngle(source, target, 1f - Mathf.Exp(-x * Time.deltaTime));
	}

	public static Vector3 Flerp(Vector3 source, Vector3 target, float smoothing, float dt)
	{ 
		return Vector3.Lerp(source, target, 1f - Mathf.Pow(smoothing, dt));
	}

	public static Vector3 Flerp(Vector3 source, Vector3 target, float x)
	{ 
		return Vector3.Lerp(source, target, 1f - Mathf.Exp(-x * Time.deltaTime));
	}

	public static Vector3 FSlerp(Vector3 source, Vector3 target, float smoothing, float dt)
	{ 
		return Vector3.Slerp(source, target, 1f - Mathf.Pow(smoothing, dt));
	}

	public static Vector3 FSlerp(Vector3 source, Vector3 target, float x)
	{ 
		return Vector3.Slerp(source, target, 1f - Mathf.Exp(-x * Time.deltaTime));
	}

	public static Vector3 RoundToUp(Vector3 source, Vector3 up, bool normalize = false)
	{ 
		if (normalize)
		{ 
			source.Normalize();
			up.Normalize();
		}
		float num = Vector3.Dot(source, up);
		if (num <= -0.707106769f)
		{ 
			return -up;
		}
		if (num >= 0.707106769f)
		{ 
			return up;
		}
		return Math3d.ProjectVectorOnPlaneNormalized(up, source);
	}

	public static Vector3 RoundToUp(Vector3 source, Vector3 up, out int dotSign, bool normalize = false)
	{ 
		if (normalize)
		{ 
			source.Normalize();
			up.Normalize();
		}
		float num = Vector3.Dot(source, up);
		if (num <= -0.707106769f)
			{ 
			dotSign = -1;
			return -up;
		}
		if (num >= 0.707106769f)
		{ 
			dotSign = 1;
			return up;
		}
		dotSign = 0;
		return Math3d.ProjectVectorOnPlaneNormalized(up, source);
	}

	public static Quaternion FSlerp(Quaternion source, Quaternion target, float smoothing, float dt)
	{ 
		return Quaternion.Slerp(source, target, 1f - Mathf.Pow(smoothing, dt));
	}

	public static Quaternion FSlerp(Quaternion source, Quaternion target, float x)
	{ 
		return Quaternion.Slerp(source, target, 1f - Mathf.Exp(-x * Time.deltaTime));
	}

	public static void SetEmission(this ParticleSystem ps, bool emit, bool kill = false)
	{ 
		if (emit && !ps.isEmitting)
		{ 
			ps.Play();
			return;
		}
		if (!emit && ps.isEmitting)
		{ 
			ps.Stop(false, kill ? ParticleSystemStopBehavior.StopEmittingAndClear : 	ParticleSystemStopBehavior.StopEmitting);
		}
	}

	public static string GetTransformPath(Transform transform, Transform upTo)
	{ 
		StringBuilder stringBuilder = new StringBuilder();
		Transform transform2 = transform;
		int num = 0;
		while (transform2 != upTo)
		{ 
			if (num > 0)
			{ 
				stringBuilder.Insert(0, transform2.name + "\\");
			}
			else
			{ 
				stringBuilder.Insert(0, transform2.name);
			}
			transform2 = transform2.parent;
			num++;
		}
		return stringBuilder.ToString();
	}

	public static List<Transform> GetAllTransforms(Transform parent)
	{ 
		List<Transform> expr_05 = new List<Transform>();
		Ext.BuildTransformList(expr_05, parent);
		return expr_05;
	}

	public static void SetCustomSimulationSpace(this ParticleSystem ps, Transform transform)
	{ 
		ParticleSystem.MainModule main = ps.main;
		main.simulationSpace = ParticleSystemSimulationSpace.Custom;
		main.customSimulationSpace = transform;
	}

	public static void EmitConditioned(this ParticleSystem ps, bool emitCondition, ParticleSystemStopBehavior stopBehavior = ParticleSystemStopBehavior.StopEmitting)
	{ 
		if (ps.isPlaying)
		{ 
			if (!emitCondition)
			{ 
				ps.Stop(true, stopBehavior);
			}
			return;
		}
		if (emitCondition)
		{ 
		ps.Play();
		}
	}

	public static void EmitConditioned(this ParticleSystem ps, Func<bool> emitCondition, ParticleSystemStopBehavior stopBehavior = ParticleSystemStopBehavior.StopEmitting)
	{ 
		if (ps.isPlaying)
		{ 
			if (!emitCondition())
			{ 
				ps.Stop(true, stopBehavior);
			}
			return;
		}
		if (emitCondition())
		{ 
			ps.Play();
		}
	}

	private static void BuildTransformList(ICollection<Transform> transforms, Transform parent)
	{ 
		if (parent == null)
		{ 
			return;
		}
		foreach (Transform transform in parent)
		{ 
			transforms.Add(transform);
			Ext.BuildTransformList(transforms, transform);
		}
	}
}