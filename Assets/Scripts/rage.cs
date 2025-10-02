using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class rage : MonoBehaviour
{
	[Header("Config")]
	public float duration = 5f;          // quanto tempo o rage dura
	public float interval = 10f;         // tempo de espera até o próximo rage
	public float speedMultiplier = 2f;   // multiplicador de velocidade
	public float cooldown = 0f;          // se quiser tempo extra depois do intervalo

	private bool isRaging = false;

	// COR
	private SpriteRenderer spriteRenderer;
	private Color originalColor;

	private class ModifiedEntry
	{
		public object target;
		public FieldInfo field;
		public PropertyInfo prop;
		public object originalValue;
		public bool isProperty;
	}
	private List<ModifiedEntry> modified = new List<ModifiedEntry>();

	private string[] speedKeys = new string[] { "speed", "move", "vel", "velocity", "walk", "run", "maxspeed" };
	private string[] fireRateKeys = new string[] { "firerate" };

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
			originalColor = spriteRenderer.color;
	}

	void Start()
	{
		StartCoroutine(RageLoop());
	}

	private IEnumerator RageLoop()
	{
		while (true)
		{
			yield return new WaitForSeconds(interval); // espera interval segundos

			yield return StartCoroutine(RageCoroutine());
		}
	}

	private IEnumerator RageCoroutine()
	{
		isRaging = true;

		// muda cor pra vermelho
		if (spriteRenderer != null)
			spriteRenderer.color = Color.red;

		FindAndModify();

		yield return new WaitForSeconds(duration);

		Revert();

		// volta a cor original
		if (spriteRenderer != null)
			spriteRenderer.color = originalColor;

		isRaging = false;

		if (cooldown > 0f)
			yield return new WaitForSeconds(cooldown);
	}

	private void FindAndModify()
	{
		modified.Clear();
		MonoBehaviour[] comps = GetComponentsInChildren<MonoBehaviour>(true);

		foreach (var comp in comps)
		{
			if (comp == null || comp == this) continue;

			Type t = comp.GetType();

			var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (var f in fields)
			{
				if (!IsNumericField(f.FieldType)) continue;

				string name = f.Name.ToLower();
				float mult = GetMultiplierForName(name);
				if (mult == 1f) continue;

				object orig = f.GetValue(comp);
				float origF = ToFloat(orig);
				float newVal = origF * mult;
				object valToSet = FromFloat(newVal, f.FieldType);
				f.SetValue(comp, valToSet);

				modified.Add(new ModifiedEntry { target = comp, field = f, originalValue = orig, isProperty = false });
			}

			var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (var p in props)
			{
				if (!p.CanRead || !p.CanWrite) continue;
				if (!IsNumericType(p.PropertyType)) continue;

				string name = p.Name.ToLower();
				float mult = GetMultiplierForName(name);
				if (mult == 1f) continue;

				object orig = p.GetValue(comp, null);
				float origF = ToFloat(orig);
				float newVal = origF * mult;
				object valToSet = FromFloat(newVal, p.PropertyType);
				p.SetValue(comp, valToSet, null);

				modified.Add(new ModifiedEntry { target = comp, prop = p, originalValue = orig, isProperty = true });
			}
		}
	}

	private void Revert()
	{
		foreach (var m in modified)
		{
			if (!m.isProperty)
				m.field.SetValue(m.target, m.originalValue);
			else
				m.prop.SetValue(m.target, m.originalValue, null);
		}
		modified.Clear();
	}

	// helpers
	private bool IsNumericField(Type t)
	{
		return IsNumericType(t);
	}

	private bool IsNumericType(Type t)
	{
		return t == typeof(float) || t == typeof(double) || t == typeof(int);
	}

	private float GetMultiplierForName(string lowerName)
	{
		foreach (var k in speedKeys) if (lowerName.Contains(k)) return speedMultiplier;
		foreach (var k in fireRateKeys) if (lowerName.Contains(k)) return 1f / speedMultiplier;
		return 1f;
	}

	private float ToFloat(object o)
	{
		if (o is int) return (float)(int)o;
		if (o is float) return (float)o;
		if (o is double) return (float)(double)o;
		return 0f;
	}

	private object FromFloat(float v, Type t)
	{
		if (t == typeof(int)) return (int)Mathf.Round(v);
		if (t == typeof(float)) return v;
		if (t == typeof(double)) return (double)v;
		return v;
	}
}
