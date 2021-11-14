using System;

public class Modal
{
	public string modalDisplay { get; set; }
	public string modalClass { get; set; }

	public Modal()
	{
		modalDisplay = "none;";
		modalClass = "";
	}
	public Modal(string modalDisplay, string modalClass)
	{
		this.modalDisplay = modalDisplay;
		this.modalClass = modalClass;
	}
}
