namespace CefDotnet.CefApi.Types;

///
/// Supported file dialog modes.
///
public enum cef_file_dialog_mode_t
{
    ///
    /// Requires that the file exists before allowing the user to pick it.
    ///
    FILE_DIALOG_OPEN,

    ///
    /// Like Open, but allows picking multiple files to open.
    ///
    FILE_DIALOG_OPEN_MULTIPLE,

    ///
    /// Like Open, but selects a folder to open.
    ///
    FILE_DIALOG_OPEN_FOLDER,

    ///
    /// Allows picking a nonexistent file, and prompts to overwrite if the file
    /// already exists.
    ///
    FILE_DIALOG_SAVE,

    FILE_DIALOG_NUM_VALUES,
}