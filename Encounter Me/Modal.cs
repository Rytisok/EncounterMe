using System;

public class Modal
{
	public string ModalDisplay { get; set; }
	public string ModalClass { get; set; }

	public Modal()
	{
		ModalDisplay = "none;";
		ModalClass = "";
	}
	public Modal(string modalDisplay, string modalClass)
	{
		this.ModalDisplay = modalDisplay;
		this.ModalClass = modalClass;
	}
}
