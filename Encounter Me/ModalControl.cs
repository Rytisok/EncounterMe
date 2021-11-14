using System;

public static class ModalControl
{
	public static void OpenModal(Modal modal)
    {
        modal.modalDisplay = "block;";
        modal.modalClass = "show";
    }
    public static void CloseModal(Modal modal)
    {
        modal.modalDisplay = "";
        modal.modalClass = "hide";
    }
}
