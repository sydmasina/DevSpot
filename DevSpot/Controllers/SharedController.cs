using Microsoft.AspNetCore.Mvc;

namespace DevSpot.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult GetModalData(string modalType, int? id, string actionControllerName)
        {
            string title = "";
            string message = "";
            string actionUrl = "";
            string buttonText = "";
            string buttonClass = "";
            string controllerName = actionControllerName;

            // Determine the action type and set the modal content dynamically
            if (modalType == "delete")
            {
                title = "Confirm Deletion";
                message = "Are you sure you want to delete this item?";
                buttonText = "Delete";
                buttonClass = "btn-danger";  // Danger class for delete
                actionUrl = Url.Action("Delete", controllerName, new { id = id });
            }
            else if (modalType == "info")
            {
                title = "Information";
                message = "Here is some important information.";
                buttonText = "OK";
                buttonClass = "btn-info";  // Info class for info
                controllerName = "Info";  // Controller for info action
                actionUrl = Url.Action("Info", controllerName, new { id = id });
            }
            else
            {
                title = "Unknown Action";
                message = "No action specified.";
                buttonText = "Close";
                buttonClass = "btn-secondary";
                actionUrl = "#"; // Default action
            }

            // Return the modal data as JSON including the dynamic action URL and controller
            return Json(new { title, message, buttonText, actionUrl, buttonClass });
        }

    }
}
