using Terminal.Gui;
using PygmalionUI.Services;

namespace PygmalionUI.Views;

public class FilesView : BaseView
{
    private readonly NavigationService _navigationService;
    private ListView _filesList = null!;
    private List<string> _files;

    public FilesView(NavigationService navigationService) : base("Files")
    {
        _navigationService = navigationService;
        _files = new List<string>
        {
            "document1.txt",
            "presentation.pptx",
            "spreadsheet.xlsx",
            "image.png",
            "notes.md"
        };

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        var label = new Label("File List (Press ESC to return to main menu)")
        {
            X = 1,
            Y = 1
        };
        Add(label);

        _filesList = new ListView(_files)
        {
            X = 1,
            Y = 3,
            Width = Dim.Fill() - 2,
            Height = Dim.Fill() - 8
        };
        Add(_filesList);

        var addButton = new Button("Add File")
        {
            X = 1,
            Y = Pos.Bottom(_filesList) + 1
        };
        addButton.Clicked += OnAddFile;
        Add(addButton);

        var removeButton = new Button("Remove Selected")
        {
            X = Pos.Right(addButton) + 2,
            Y = Pos.Bottom(_filesList) + 1
        };
        removeButton.Clicked += OnRemoveFile;
        Add(removeButton);

        var backButton = new Button("Back")
        {
            X = Pos.Right(removeButton) + 2,
            Y = Pos.Bottom(_filesList) + 1
        };
        backButton.Clicked += () => _navigationService.GoBack();
        Add(backButton);
    }

    private void OnAddFile()
    {
        var fileNameLabel = new Label("File Name:")
        {
            X = 1,
            Y = 1
        };

        var fileNameField = new TextField("")
        {
            X = Pos.Right(fileNameLabel) + 1,
            Y = 1,
            Width = 30
        };

        var dialog = new Dialog("Add New File", 50, 8);
        dialog.Add(fileNameLabel, fileNameField);

        var okButton = new Button("OK");
        okButton.Clicked += () =>
        {
            if (!string.IsNullOrWhiteSpace(fileNameField.Text.ToString()))
            {
                _files.Add(fileNameField.Text.ToString()!);
                _filesList.SetSource(_files);
                Application.RequestStop();
            }
        };

        var cancelButton = new Button("Cancel");
        cancelButton.Clicked += () => Application.RequestStop();

        dialog.AddButton(okButton);
        dialog.AddButton(cancelButton);

        Application.Run(dialog);
    }

    private void OnRemoveFile()
    {
        if (_filesList.SelectedItem >= 0 && _filesList.SelectedItem < _files.Count)
        {
            _files.RemoveAt(_filesList.SelectedItem);
            _filesList.SetSource(_files);
        }
    }
}
