using System;
using TMPro;
using UnityEngine;

public class DiceSelection : MonoBehaviour
{
	private TMP_Dropdown _dropDown;

	public Enumerators.DiceTypes GetCurrentSelectedDice() => Util.ParseEnum<Enumerators.DiceTypes>(_dropDown.options[_dropDown.value].text);

	private void Start()
	{
		_dropDown = gameObject.GetComponent<TMP_Dropdown>();

		FillChoices();
	}

	private void FillChoices()
	{
		foreach (Enumerators.DiceTypes diceType in Enum.GetValues(typeof(Enumerators.DiceTypes)))
		{
			_dropDown.options.Add(new TMP_Dropdown.OptionData(diceType.ToString()));
		}
	}
}