using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DiceDisplayer : MonoBehaviour
{
	[SerializeField] private DiceSelection _diceDropDown;
	[SerializeField] private List<GameObject> _dicePrefabs;
	[SerializeField] private ObjectLauncher _launcher;
	private List<GameObject> setOfDiceToThrow;

	public void Start()
	{
		_launcher = GetComponent<ObjectLauncher>();
		setOfDiceToThrow = new List<GameObject>();
	}

	public void AddDice()
	{
		GameObject diceToAdd = GetDicePrefab(_diceDropDown.GetCurrentSelectedDice());

		diceToAdd.GetComponent<Rigidbody>().isKinematic = true;

		setOfDiceToThrow.Add(diceToAdd);
	}

	public void RemoveDice()
	{
		setOfDiceToThrow.RemoveAt(0);
	}

	public void RemoveDice(GameObject dice)
	{
		setOfDiceToThrow.Remove(dice);
	}

	public void ThrowDice()
	{
		GameObject diceToThrow = setOfDiceToThrow.First();

		_launcher.ThrowObject(setOfDiceToThrow.First());
		RemoveDice();
	}

	private GameObject GetDicePrefab(Enumerators.DiceTypes dice)
	{
		return Instantiate(_dicePrefabs[(int)dice], transform);
	}
}
