using System;

public static class ModalControl
{
	public static void OpenModal(Modal modal)
    {
        modal.ModalDisplay = "block;";
        modal.ModalClass = "show";
    }
    public static void CloseModal(Modal modal)
    {
        modal.ModalDisplay = "";
        modal.ModalClass = "hide";
    }
}
