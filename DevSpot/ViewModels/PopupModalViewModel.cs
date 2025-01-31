namespace DevSpot.ViewModels
{
    public class PopupModalViewModel
    {
        public string Title { get; set; } // Modal title (e.g., "Confirm Delete", "Success")
        public string Message { get; set; } // Message to display in the modal
        public string ModalType { get; set; } // Can be "info", "success", "warning", "danger"
        public int? ItemId { get; set; } // Optional, only for delete confirmation
    }
}
