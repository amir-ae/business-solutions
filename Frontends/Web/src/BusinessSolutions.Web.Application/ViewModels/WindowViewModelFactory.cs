namespace BusinessSolutions.Web.Application.ViewModels
{
    public static class WindowViewModelFactory
    {
        public static WindowViewModel<T> Create<T>(T data)
        {
            return new WindowViewModel<T>
            {
                ModelData = data,
                Action = "Create",
                Theme = "primary",
                ShowId = false
            };
        }

        public static WindowViewModel<T> View<T>(T data)
        {
            return new WindowViewModel<T>
            {
                ModelData = data,
                Action = "View",
                ReadOnly = true,
                Theme = "info",
                ShowAction = false,
                CancelLabel = "Close"
            };
        }

        public static WindowViewModel<T> Edit<T>(T data)
        {
            return new WindowViewModel<T>
            {
                ModelData = data,
                Action = "Edit",
                Theme = "warning"
            };
        }

        public static WindowViewModel<T> Delete<T>(T data)
        {
            return new WindowViewModel<T>
            {
                ModelData = data,
                Action = "Delete",
                ReadOnly = true,
                Theme = "danger"
            };
        }
    }

    public class WindowViewModel<T>
    {
        public T? ModelData { get; set; }
        public string Action { get; set; } = "Create";
        public bool ReadOnly { get; set; } = false;
        public string? Theme { get; set; } = "primary";
        public bool ShowId { get; set; } = true;
        public bool ShowAction { get; set; } = true;
        public string CancelLabel { get; set; } = "Cancel";
    }
}