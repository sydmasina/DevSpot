using Microsoft.AspNetCore.Mvc;
using DevSpot.ViewModels;

namespace DevSpot.Controllers
{
    public class SharedController : Controller
    {
        // Show confirmation modal before deleting
        public IActionResult ConfirmDelete(int id)
        {
            var model = new PopupModalViewModel
            {
                Title = "Confirm Delete",
                Message = "Are you sure you want to delete this entry?",
                ModalType = "danger", // Red theme for delete confirmation
                ItemId = id
            };

            return PartialView("_PopupModal", model);
        }

        // Show an info modal
        public IActionResult ShowInfoModal()
        {
            var model = new PopupModalViewModel
            {
                Title = "Information",
                Message = "This is an info modal.",
                ModalType = "info"
            };

            return PartialView("_PopupModal", model);
        }

        // Show a success modal
        public IActionResult ShowSuccessModal()
        {
            var model = new PopupModalViewModel
            {
                Title = "Success",
                Message = "Your action was completed successfully!",
                ModalType = "success"
            };

            return PartialView("_PopupModal", model);
        }

        // Show a warning modal
        public IActionResult ShowWarningModal()
        {
            var model = new PopupModalViewModel
            {
                Title = "Warning",
                Message = "Be careful! This action cannot be undone.",
                ModalType = "warning"
            };

            return PartialView("_PopupModal", model);
        }

    }
}
